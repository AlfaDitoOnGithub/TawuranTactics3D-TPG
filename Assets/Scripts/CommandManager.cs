using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CommandType {
    MoveTo,
    Attack
}

public class Command {
    public Character character;
    public Vector2Int selectedGrid;
    public CommandType commandType;

    public Command(Character character, Vector2Int selectedGrid, CommandType commandType)
    {
        this.character = character;
        this.selectedGrid = selectedGrid;
        this.commandType = commandType;
    }

    public List<PathNode> path;
    public GridObject target;
}

public class CommandManager : MonoBehaviour
{
    Command currentCommand;
    CommandInput commandInput;
    ClearUtility clearUtility;

    private void Awake()
    {
        clearUtility = GetComponent<ClearUtility>();
    }

    private void Start()
    {
        commandInput = GetComponent<CommandInput>();
    }

    private void Update()
    {
        if(currentCommand != null)
        {
            ExecuteCommand();
        }
    }
    public void AddAttackCommand(Character character, Vector2Int selectedGrid, GridObject target){
        currentCommand = new Command(character, selectedGrid, CommandType.Attack);
        currentCommand.target = target;
    }

    public void AddMoveCommand(Character character, Vector2Int selectedGrid, List<PathNode> path)
    {
        currentCommand = new Command(character, selectedGrid, CommandType.MoveTo);
        currentCommand.path = path;
    }

    public void ExecuteCommand()
    {
        switch (currentCommand.commandType)
        {
            case CommandType.MoveTo:
                MovementCommandExecute();
                break;
            case CommandType.Attack:
                AttackCommandExecute();
                break;
            default:
                break;
        }
    }

    private void AttackCommandExecute()
    {
        Character receiver = currentCommand.character;
        receiver.GetComponent<Attack>().AttackPosition(currentCommand.target);
        receiver.GetComponent<CharacterTurn>().canAct =false;
        currentCommand = null;
        clearUtility.ClearPF();
        clearUtility.ClearMoveHighlight();
        
    }

    private void MovementCommandExecute()
    {
        Character reciever = currentCommand.character;
        reciever.GetComponent<Movement>().Move(currentCommand.path);
        reciever.GetComponent<CharacterTurn>().canWalk =false;
        currentCommand = null;
        clearUtility.ClearAttackHighlight();
    }
}

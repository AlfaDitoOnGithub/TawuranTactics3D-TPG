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
}

public class CommandManager : MonoBehaviour
{
    Command currentCommand;
    CommandInput commandInput;

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
    public void AddAttackCommand(Character character, Vector2Int selectedGrid){
        currentCommand = new Command(character, selectedGrid, CommandType.Attack);
    }

    public void AddMoveCommand(Character character, Vector2Int selectedGrid, List<PathNode> path)
    {
        currentCommand = new Command(character, selectedGrid, CommandType.MoveTo);
        currentCommand.path = path;
    }

    public void ExecuteCommand(){
        Character reciever = currentCommand.character;
        reciever.GetComponent<Movement>().Move(currentCommand.path);
        currentCommand = null;
        commandInput.HighlightWalkableTerrain();
    }
}

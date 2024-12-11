using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandInput : MonoBehaviour
{
    CommandManager commandManager;
    MouseInput mouseInput;
    MoveCharacter moveCharacter;
    CharacterAttack characterAttack;
    SelectCharacter selectCharacter;
    ClearUtility clearUtility;
    [SerializeField] CommandType currentCommand;
    bool isInputCommand;

    private void Awake()
    {
        commandManager = GetComponent<CommandManager>();
        mouseInput = GetComponent<MouseInput>();
        moveCharacter = GetComponent<MoveCharacter>();
        characterAttack = GetComponent<CharacterAttack>();
        selectCharacter = GetComponent<SelectCharacter>();
        clearUtility = GetComponent<ClearUtility>();

    }

    public void SetCommandType(CommandType commandType){
        currentCommand = commandType;
    }
    
    public void InitCommand(){
        isInputCommand = true;
        switch(currentCommand){
            default: break;
            case CommandType.MoveTo:
                HighlightWalkableTerrain();
                break;
            case CommandType.Attack:
                characterAttack.CalculateAttackArea(selectCharacter.selected.GetComponent<GridObject>().positionOnGrid, selectCharacter.selected.attackRange);
                break;
        }
    }
    

    private void Start()
    {
        // HighlightWalkableTerrain();
        // characterAttack.CalculateAttackArea(selectedCharacter.GetComponent<GridObject>().positionOnGrid, selectedCharacter.attackRange);
    }
    private void Update() {
        if(isInputCommand == false) {
            return;
        }
        switch (currentCommand)
        {
            case CommandType.MoveTo:
                MoveCommandInput();
                break;
            case CommandType.Attack:
                AttackCommandInput();
                break;
            default: break;
        }
        // MoveCommandInput();
        // AttackCommandInput();
    }

    private void AttackCommandInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(characterAttack.Check(mouseInput.positionOnGrid) == true){
                GridObject gridObject = characterAttack.GetAttackTarget(mouseInput.positionOnGrid);
                if (gridObject == null)
                {
                    return;
                }
                commandManager.AddAttackCommand(selectCharacter.selected, mouseInput.positionOnGrid, gridObject);
                StopCommandInput();
            }
        }
         if(Input.GetMouseButtonDown(1)){
            // selectCharacter.selected.GetComponent<Movement>().SkipAnimation();
            StopCommandInput();
            clearUtility.ClearAttackHighlight();
        }
    }

    

    private void MoveCommandInput()
    {
        if(Input.GetMouseButtonDown(0)){

            List<PathNode> path = moveCharacter.GetPath(mouseInput.positionOnGrid);
            if (path == null)
            {
                return;
            }
            if (path.Count == 0)
            {
                return;
            }
            commandManager.AddMoveCommand(selectCharacter.selected, mouseInput.positionOnGrid, path);
            StopCommandInput();
        }
        if(Input.GetMouseButtonDown(1)){
            // selectCharacter.selected.GetComponent<Movement>().SkipAnimation();
            StopCommandInput();
            clearUtility.ClearMoveHighlight();
            clearUtility.ClearPF();
        }
    }
    private void StopCommandInput()
    {
        selectCharacter.Deselect();
        selectCharacter.enabled = true;
        isInputCommand = false;
    }
    public void HighlightWalkableTerrain()
    {
        moveCharacter.CheckWalkableTerrain(selectCharacter.selected);
    }
}

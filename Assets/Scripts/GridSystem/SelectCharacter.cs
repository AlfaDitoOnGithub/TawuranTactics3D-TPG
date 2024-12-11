using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacter : MonoBehaviour
{
    MouseInput mouseInput;
    CommandMenu commandMenu;

    private void Awake()
    {
        mouseInput = GetComponent<MouseInput>();
        commandMenu = GetComponent<CommandMenu>();
    }

    public Character selected;
    public Character hoverOverCharacter;
    Vector2Int positionOnGrid = new Vector2Int(-1,-1);
    [SerializeField] GridMap targetGrid;
    GridObject hoverOverGridObject;

    private void Update()
    {
        HoverOverObject();

        SelectInput();
        DeselectInput();

    }

    private void HoverOverObject()
    {
        if (positionOnGrid != mouseInput.positionOnGrid)
        {
            positionOnGrid = mouseInput.positionOnGrid;
            hoverOverGridObject = targetGrid.GetPlaceObject(positionOnGrid);
            if (hoverOverGridObject != null)
            {
                hoverOverCharacter = hoverOverGridObject.GetComponent<Character>();
            }
            else
            {
                hoverOverCharacter = null;
            }
        }
    }

    private void DeselectInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            selected = null;
            UpdatePanel();
        }
    }

    private void UpdatePanel()
    {
        if (selected != null)
        {
            commandMenu.OpenPanel();
        }
        else{
            commandMenu.ClosePanel();
        }
    }

    private void SelectInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (hoverOverCharacter != null && selected == null)
            {
                selected = hoverOverCharacter;
            }
            UpdatePanel();
        }
    }

    public void Deselect()
    {
        selected =null;
    }
}

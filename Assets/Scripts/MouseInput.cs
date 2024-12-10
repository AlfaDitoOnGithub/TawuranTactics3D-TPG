using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] GridMap targetGrid;
    [SerializeField] LayerMask terrainLayer;

    public Vector2Int positionOnGrid;
    public bool active;
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit; 
        if (Physics.Raycast(ray,out hit, float.MaxValue, terrainLayer))
        {
            Vector2Int hitPosition = targetGrid.GetGridPosition(hit.point);
            if(hitPosition != positionOnGrid){
                positionOnGrid = hitPosition;
            }
        }
        else{
            active = false;
        }
    }
}

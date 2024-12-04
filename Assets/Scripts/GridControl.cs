using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridControl : MonoBehaviour
{
    [SerializeField] GridMap targetGrid;

    [SerializeField] LayerMask terrainLayer;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if(Physics.Raycast(ray,out hit, float.MaxValue, terrainLayer)){
                Vector2Int gridPosition = targetGrid.GetGridPosition(hit.point);
                Debug.Log(gridPosition);
                GridObject gridObject = targetGrid.GetPlaceObject(gridPosition);
                if(gridObject == null){
                    Debug.Log("x= " +gridPosition.x + "y= "+gridPosition.y + "is empty");
                }
                else{
                    Debug.Log("There is something" + gridObject.GetComponent<Character>().charName);
                }
            }
        }
    }
}

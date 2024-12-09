using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridControl : MonoBehaviour
{
    [SerializeField] GridMap targetGrid;

    [SerializeField] LayerMask terrainLayer;

    Pathfinding pathfinding;
    Vector2Int currentPosition = new Vector2Int();
    List<PathNode> path;

    private void Start()
    {
        pathfinding = targetGrid.GetComponent<Pathfinding>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if(Physics.Raycast(ray,out hit, float.MaxValue, terrainLayer)){
                Vector2Int gridPosition = targetGrid.GetGridPosition(hit.point);
                Debug.Log(gridPosition);

                path = pathfinding.FindPath(currentPosition.x, currentPosition.y, gridPosition.x, gridPosition.y);

                currentPosition = gridPosition;
                /*
                GridObject gridObject = targetGrid.GetPlaceObject(gridPosition);
                if(gridObject == null){
                    Debug.Log("x= " +gridPosition.x + "y= "+gridPosition.y + "is empty");
                }
                else{
                    Debug.Log("There is something" + gridObject.GetComponent<Character>().charName);
                }*/
            }
        }
    }


    private void OnDrawGizmos()
    {
        if(path == null)
        {
            return;
        }

        if(path.Count == 0)
        {
            return;
        }

        for(int i = 0; i < path.Count; i++) 
        {
            Gizmos.DrawLine(targetGrid.GetWorldPosition(path[i].pos_x, path[i].pos_y, true), 
                targetGrid.GetWorldPosition(path[i + 1].pos_x, path[i + 1].pos_y, true));
        }
    }
}

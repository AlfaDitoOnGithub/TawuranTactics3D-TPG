using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GridMap : MonoBehaviour
{
    Node[,] gridMap;
    public int width = 25;
    public int length = 25;
    [SerializeField] float cellSize = 1f;
    [SerializeField] LayerMask obstacleLayer;
    [SerializeField] LayerMask terrainLayer;

    private void Awake()
    {
        GenerateGrid();
    }

    private void GenerateGrid(){
        gridMap = new Node[length,width];
        for (int z = 0; z < width; z++)
        {
            for (int x = 0; x < length; x++)
            {
                gridMap[x,z] = new Node();
            }
        }
        CalculateElevation();
        CheckPassableTerrain();
    
    }

    private void CheckPassableTerrain(){
        for (int z = 0; z < width; z++)
        {
            for (int x = 0; x < length; x++)
            {
                Vector3 worldPosition = GetWorldPosition(x,z);
                bool passable = !Physics.CheckBox(worldPosition, Vector3.one/2 * cellSize, Quaternion.identity, obstacleLayer);
                gridMap[x,z].passable = passable;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (gridMap == null) {
            for (int z = 0; z < width; z++)
            {
                for (int x = 0; x < length; x++)
                {
                    Vector3 pos = GetWorldPosition(x, z);
                    //Gizmos.color = gridMap[x,z].passable ? Color.white : Color.red;
                    Gizmos.DrawCube(pos, Vector3.one / 4);
                }
            }
        }else {
            for (int z = 0; z < width; z++)
            {
                for (int x = 0; x < length; x++)
                {
                    Vector3 pos = GetWorldPosition(x, z, true);
                    Gizmos.color = gridMap[x,z].passable ? Color.white : Color.red;
                    Gizmos.DrawCube(pos, Vector3.one / 4);
                }
            }
        }
        
    }

    public Vector3 GetWorldPosition(int x, int z,bool elevation = false)
    {
        return new Vector3(x * cellSize, elevation == true ? gridMap[x,z].elevation : 0f, z * cellSize);
    }

    public Vector2Int GetGridPosition(Vector3 worldPosition) 
    {   
       worldPosition.x += cellSize/2;
       worldPosition.z += cellSize/2;
       Vector2Int positionOnGrid = new Vector2Int((int) (worldPosition.x/cellSize), (int) (worldPosition.z/cellSize));
       return positionOnGrid;
    }
    internal void RemoveObject(Vector2Int positionOnGrid, GridObject gridObject)
    {
        if(CheckBoundary(positionOnGrid)){
          gridMap[positionOnGrid.x, positionOnGrid.y].gridObject = null;
        }
        else{
            Debug.Log("OUT OF BOUNDS");
        }
    }

    public void PlaceObject(Vector2Int positionOnGrid, GridObject gridObject)
    {   
        if(CheckBoundary(positionOnGrid)){
          gridMap[positionOnGrid.x, positionOnGrid.y].gridObject = gridObject;
        }
        else{
            Debug.Log("OUT OF BOUNDS");
        }
    }

    internal GridObject GetPlaceObject(Vector2Int gridPosition)
    {
        if (CheckBoundary(gridPosition))
        {
           GridObject gridObject = gridMap[gridPosition.x,gridPosition.y].gridObject;
           return gridObject; 
        }
        else{return null;}
    }

    public bool CheckBoundary(Vector2Int positionOnGrid){
        if(positionOnGrid.x < 0 || positionOnGrid.x >= length)
        {
            return false;
        }
        if(positionOnGrid.y < 0 || positionOnGrid.y >= width){
            return false;
        }  
        return true;
        
    }

    internal bool CheckBoundary(int posX, int posY)
    {
        if (posX < 0 || posX >= length)
        {
            return false;
        }
        if (posY < 0 || posY >= width)
        {
            return false;
        }
        return true;
    }

    private void CalculateElevation(){
        for (int z = 0; z < width; z++)
            {
                for (int x = 0; x < length; x++)
                {
                    Ray ray = new Ray(GetWorldPosition(x,z)+Vector3.up*100, Vector3.down);
                    RaycastHit hit;
                    if(Physics.Raycast(ray,out hit, float.MaxValue, terrainLayer)){
                        gridMap[x,z].elevation = hit.point.y;
                    }
                }
            }
    }

    public bool CheckWalkable(int pos_x, int pos_y)
    {
        return gridMap[pos_x, pos_y].passable;
    }

    
    public List<Vector3> ConvertPathNodesToWorldPositions(List<PathNode> path)
    {
        List<Vector3> worldPositions = new List<Vector3>();

        for (int i = 0; i < path.Count; i++)
        {
            worldPositions.Add(GetWorldPosition(path[i].pos_x, path[i].pos_y, true));
        }
        return worldPositions;
    }
}
    



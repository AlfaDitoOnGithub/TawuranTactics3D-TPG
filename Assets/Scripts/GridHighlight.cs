using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GridHighlight : MonoBehaviour
{
    GridMap gridMap;
    [SerializeField] GameObject movePoint;
    List<GameObject> movePointGOs;
    [SerializeField] GameObject movePointsContainer;
    [SerializeField] List<Vector2Int> testTargetPosition;

    private void Start()
    {
        gridMap = GetComponent<GridMap>();
        movePointGOs = new List<GameObject>();
        // CreateMovePointHighlightObject();

        Highlight(testTargetPosition);
       
    }

    private GameObject CreateMovePointHighlightObject()
    {
        GameObject go = Instantiate(movePoint);
        movePointGOs.Add(go);
        return go;
    }
    public void Highlight(List<Vector2Int> positions){
        for (int i = 0; i < positions.Count; i++)
        {
            Highlight(positions[i].x,positions[i].y, GetMovePointGO(i));
        }
    }

    private GameObject GetMovePointGO(int i)
    {
        if(movePointGOs.Count <= i){
            return movePointGOs[i];
        }

        GameObject newHighlightObject = CreateMovePointHighlightObject();
        return newHighlightObject;
    }

    public void Highlight(int posx, int posy, GameObject highlightObject)
    {
        Vector3 position = gridMap.GetWorldPosition(posx,posy, true);
        position += Vector3.up*0.2f;
        movePointGOs[0].transform.position = position;
    }
} 

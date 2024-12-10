using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridHighlight : MonoBehaviour
{
    GridMap grid;
    [SerializeField] GameObject highlightPoint;
    List<GameObject> highlightPointsGO;
    [SerializeField] GameObject container;
    
    // Start is called before the first frame update
    void Awake()
    {
        grid = GetComponentInParent<GridMap>();
        highlightPointsGO = new List<GameObject>();

        //Highlight(testTargetPosition);
    }

    private GameObject CreatePointHighlightObject()
    {
        GameObject go = Instantiate(highlightPoint);
        highlightPointsGO.Add(go);
        go.transform.SetParent(container.transform);
        return go;
    }

    public void Highlight(List<Vector2Int> positions)
    {
        for(int i = 0; i  < positions.Count; i++)
        {
            Highlight(positions[i].x, positions[i].y, GetHighlightPointGO(i));
        }
    }

    private GameObject GetHighlightPointGO(int i)
    {
        if(highlightPointsGO.Count < i)
        {
            return highlightPointsGO[i];
        }

        GameObject newHighlightObject = CreatePointHighlightObject();
        return newHighlightObject;
    }

    public void Highlight(int posX, int posY, GameObject highlightObject)
    {
        Vector3 position = grid.GetWorldPosition(posX, posY, true);
        position += Vector3.up * 0.2f;
        highlightObject.transform.position = position;

    }

    internal void Highlight(List<PathNode> walkableNodes)
    {
        for (int i = 0; i < walkableNodes.Count; i++)
        {
            Highlight(walkableNodes[i].pos_x, walkableNodes[i].pos_y, GetHighlightPointGO(i));   
        }
    }
}

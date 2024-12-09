using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    GridObject gridObject;
    CharacterAnimator characterAnimator;

    List<Vector3> pathWorldPositions;

    [SerializeField] float moveSpeed = 1f;

    public void Awake()
    {
        gridObject = GetComponent<GridObject>();
        characterAnimator = GetComponent<CharacterAnimator>();
    }

    public void Move(List<PathNode> path)
    {
        pathWorldPositions = gridObject.targetGrid.ConvertPathNodesToWorldPositions(path);
        // Debug.Log("GET" +pathWorldPositions[0]);

        gridObject.positionOnGrid.x = path[path.Count - 1].pos_x;
        gridObject.positionOnGrid.y = path[path.Count - 1].pos_y;

        
        RotateCharacter();


        
        characterAnimator.StartMoving();
        
    }

    private void Update()
    {
        if (pathWorldPositions != null) { 
            Debug.Log("path World position is null"); 
            return; }
        if (pathWorldPositions.Count == 0) { 
            Debug.Log("path World position is zero"); 
            return; }

        transform.position = Vector3.MoveTowards(transform.position, pathWorldPositions[0], moveSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position, pathWorldPositions[0]) < 0.05f ) 
        { 
            pathWorldPositions.RemoveAt(0);
            if(pathWorldPositions.Count == 0)
            {
                characterAnimator.StopMoving();
            }
            else
            {
                RotateCharacter();
            }
        }
    }

    private void RotateCharacter()
    {
        Vector3 direction = (pathWorldPositions[0] - transform.position).normalized;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);
    }
}

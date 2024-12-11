using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    // [SerializeField] GridObject selectedCharacter;
    [SerializeField] GridMap targetGrid;
    [SerializeField] GridHighlight highlight;

    // [SerializeField] LayerMask terrainLayer;

    List<Vector2Int> attackPosition;

    // private void Start()
    // {
    //     CalculateAttackArea();
    // }

    public void CalculateAttackArea(Vector2Int characterPositionOnGrid,int attackRange, bool selfTargetable = false)
    {
        if (attackPosition == null)
        {
            attackPosition = new List<Vector2Int>();
        }
        else{
            attackPosition.Clear();
        }

        // Character character = selectedCharacter.GetComponent<Character>();
        // int attackRange = character.attackRange;

        // attackPosition = new List<Vector2Int>();

        for(int x = -attackRange; x <= attackRange; x++)
        {
            for(int y = -attackRange; y <= attackRange; y++)
            {
                if(Mathf.Abs(x) + Mathf.Abs(y) > attackRange)
                {
                    continue;
                }
                if(selfTargetable == false)
                {
                    if(x == 0 && y == 0)
                    {
                        continue;
                    }
                }
                if(targetGrid.CheckBoundary(
                    characterPositionOnGrid.x + x,
                    characterPositionOnGrid.y + y) == true)
                {
                    attackPosition.Add(
                        new Vector2Int(
                            characterPositionOnGrid.x + x, 
                            characterPositionOnGrid.y + y
                            )
                        );
                }
            }
        }
        highlight.Highlight(attackPosition);

    }

    internal bool Check(Vector2Int positionOnGrid)
    {
        return attackPosition.Contains(positionOnGrid);
    }

    internal GridObject GetAttackTarget(Vector2Int positionOnGrid)
    {
        GridObject target = targetGrid.GetPlaceObject(positionOnGrid);
        return target;
    }



    // public void CalculateAttackArea(bool selfTargetable = false)
    // {
    //     Character character = selectedCharacter.GetComponent<Character>();
    //     int attackRange = character.attackRange;

    //     attackPosition = new List<Vector2Int>();

    //     for(int x = -attackRange; x <= attackRange; x++)
    //     {
    //         for(int y = -attackRange; y <= attackRange; y++)
    //         {
    //             if(Mathf.Abs(x) + Mathf.Abs(y) > attackRange)
    //             {
    //                 continue;
    //             }
    //             if(selfTargetable == false)
    //             {
    //                 if(x == 0 && y == 0)
    //                 {
    //                     continue;
    //                 }
    //             }
    //             if(targetGrid.CheckBoundary(
    //                 selectedCharacter.positionOnGrid.x + x,
    //                 selectedCharacter.positionOnGrid.y + y) == true)
    //             {
    //                 attackPosition.Add(
    //                     new Vector2Int(
    //                         selectedCharacter.positionOnGrid.x + x, 
    //                         selectedCharacter.positionOnGrid.y + y
    //                         )
    //                     );
    //             }
    //         }
    //     }
    //     highlight.Highlight(attackPosition);

    // }

    // private void Update()
    // {
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //         RaycastHit hit;

    //         if (Physics.Raycast(ray, out hit, float.MaxValue, terrainLayer))
    //         {
    //             Vector2Int gridPosition = targetGrid.GetGridPosition(hit.point);

    //             if(attackPosition.Contains(gridPosition))
    //             {
    //                 GridObject gridObject = targetGrid.GetPlaceObject(gridPosition);
    //                 if(gridObject == null)
    //                 {
    //                     return;
    //                 }
    //                 selectedCharacter.GetComponent<Attack>().AttackPosition(gridObject);
    //                 Debug.Log("Attacked! " + gridObject.name);
    //             }
    //         }
    //     }
    // }
}

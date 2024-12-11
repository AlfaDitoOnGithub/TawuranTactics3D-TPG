using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearUtility : MonoBehaviour
{
    [SerializeField] Pathfinding targetPF;
    [SerializeField] GridHighlight attackHighlight;
    [SerializeField] GridHighlight moveHighlight;

    public void ClearPF(){
        targetPF.Clear();
    }

    public void ClearAttackHighlight(){
        attackHighlight.Hide();
    }

    public void ClearMoveHighlight(){
        moveHighlight.Hide();
    }
}

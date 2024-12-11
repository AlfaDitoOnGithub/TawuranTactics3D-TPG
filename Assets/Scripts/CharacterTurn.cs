using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTurn : MonoBehaviour
{
    public bool canWalk;
    public bool canAct;

    public void Start()
    {
        GrantTurn();
    }
    public void GrantTurn(){
        canWalk =true;
        canAct =true;
    }

}

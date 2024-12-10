using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] public string charName = "nameless";
    public float movementPoints = 50f;
    public int hp = 100;
    public int attackRange = 1;
    public int damage = 20;
}

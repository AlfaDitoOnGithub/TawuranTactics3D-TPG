using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    Animator animator;
    [SerializeField] bool move;
    [SerializeField] bool attack;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void StartMoving()
    {
        move = true;
    }

    public void StopMoving()
    {
        move = false;
    }

    public void Attack()
    {
        attack = true;
    }

    private void Update()
    {
        animator.SetBool("Attack", attack);
        animator.SetBool("Move", move);
    }

    private void LateUpdate()
    {
        if(attack == true)
        {
            attack = false;
            animator.SetBool("Attack", attack);
        }
    }
}

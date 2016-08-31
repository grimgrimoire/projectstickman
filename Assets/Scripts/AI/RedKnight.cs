using System;
using System.Collections;
using UnityEngine;

public class RedKnight : MonoBehaviour, IBaseEnemy
{

    Animator animator;
    bool isAttacking = false;

    // Use this for initialization
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        if (!isAttacking)
            StartCoroutine(ActionAttack());
    }

    IEnumerator ActionAttack()
    {
        isAttacking = true;
        animator.Play("AttackEnemy", 1);
        yield return new WaitForSeconds(1f);
        animator.Play("Idle", 1);
        isAttacking = false;
    }

    public float AttackRange()
    {
        return 2.5f;
    }

    public bool CanMove()
    {
        //return !isAttacking;
        //return false;
        return true;
    }

    public float WalkingSpeed()
    {
        return 4f;
    }

    public void WalkAnimation()
    {
        if (!isAttacking)
            animator.Play("WalkEnemy2");
    }

    public void StopWalking()
    {
        //if (!isAttacking)
        //    animator.Play("Idle", 0);
    }
}

using UnityEngine;
using System.Collections;
using System;

public class RedArcher : MonoBehaviour, IBaseEnemy
{
    static string ATTACK = "AttackEnemy_R";
    static string IDLE = "Idle";

    bool isAttacking;
    Animator animator;
    BaseEnemy baseEnemy;

    // Use this for initialization
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        baseEnemy = GetComponent<BaseEnemy>();
        animator.Play("Aiming", 1);
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
        animator.SetFloat("Blend", GetAngle() / 180);
        //yield return new WaitForSeconds(0.16f);
        yield return null;
        isAttacking = false;
    }

    private float GetAngle()
    {
        Vector2 relativePos = baseEnemy.GetPlayerPosition() - transform.position;
        float angle = Vector2.Angle(Vector2.down, relativePos);
        angle %= 180;
        return angle;
    }

    public float AttackRange()
    {
        return 10f;
    }

    public bool CanMove()
    {
        return !isAttacking;
    }

    public void StopWalking()
    {
        
    }

    public void WalkAnimation()
    {
        
    }

    public float WalkingSpeed()
    {
        return 0;
    }
}

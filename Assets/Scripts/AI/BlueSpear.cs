using System;
using System.Collections;
using UnityEngine;

public class BlueSpear : MonoBehaviour, IBaseEnemy
{
    static string ATTACK = "AttackEnemy_S";
    static string IDLE = "Idle";

    public GameObject prefabDead;
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
        animator.Play(ATTACK, 2);
        yield return new WaitForSeconds(1f);
        animator.Play(IDLE, 2);
        isAttacking = false;
    }

    public float AttackRange()
    {
        return 2.5f;
    }

    public bool CanMove()
    {
        return !isAttacking;
    }

    public void WalkAnimation()
    {
        if (!isAttacking)
            animator.Play("WalkEnemy");
    }

    public void StopWalking()
    {
        animator.Play(IDLE, 0);
    }

    public void Dead()
    {
        GameObject wreckClone = (GameObject)Instantiate(prefabDead, transform.position, transform.rotation);
        wreckClone.transform.localScale = transform.localScale;
        Destroy(wreckClone, 3);
    }

}

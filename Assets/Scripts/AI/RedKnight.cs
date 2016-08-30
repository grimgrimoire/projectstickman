using System.Collections;
using UnityEngine;

public class RedKnight : MonoBehaviour, IAttackPlayer
{

    Animator animator;
    bool isAttacking = false;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attack()
    {
        if(!isAttacking)
            StartCoroutine(ActionAttack());
    }

    IEnumerator ActionAttack()
    {
        isAttacking = true;
        animator.Play("AttackEnemy");
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
        animator.Play("");
    }
}

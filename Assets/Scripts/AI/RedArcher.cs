using UnityEngine;
using System.Collections;
using System;

public class RedArcher : MonoBehaviour, IBaseEnemy
{
    static string ATTACK = "Aiming";
    static string IDLE = "Idle";
    static string BLEND = "Blend";
    public float attackRange = 15f;
    public float aimingDelay = 0.5f;
    public float attackDelay = 2f;
    public float arrowArcHeight = 5;
    public GameObject arrowPrefab;

    public GameObject prefabDead;
    bool isAttacking;
    Animator animator;
    BaseEnemy baseEnemy;

    // Use this for initialization
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        baseEnemy = GetComponent<BaseEnemy>();
        animator.Play(ATTACK, 1);
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
        animator.SetFloat(BLEND, GetAngle() / 180);
        yield return new WaitForSeconds(aimingDelay);
        ShootArrowAtPlayer();
        //DrawArrowTrajectory();
        yield return new WaitForSeconds(aimingDelay);
        animator.SetFloat(BLEND, 0);
        yield return new WaitForSeconds(attackDelay);
        isAttacking = false;
    }

    private void ShootArrowAtPlayer()
    {
        GameObject arrow = GameObject.Instantiate(arrowPrefab);
        arrow.transform.position = transform.position;
        arrow.GetComponent<Projectile>().height = arrowArcHeight;
        arrow.GetComponent<Projectile>().SetTargetPosition(baseEnemy.GetPlayerPosition());
    }

    private void DrawArrowTrajectory()
    {
        StartCoroutine(Drawpath());
    }

    IEnumerator Drawpath()
    {
        Vector2 tempStart1 = transform.position;
        Vector2 tempStart2 = transform.position;
        Vector2 tempTarget = baseEnemy.GetPlayerPosition();
        float hMovement = Mathf.Clamp(tempTarget.x - tempStart2.x, -10, 10);
        float compensation = Mathf.Abs((tempTarget.x - tempStart2.x))/Mathf.Abs(hMovement);
        float vMovement = compensation/2 * 10 + (tempTarget.y - tempStart2.y)/compensation;
        while (Vector2.Distance(tempStart1, tempTarget) >= 0.5f) {
            tempStart2 = new Vector2( tempStart2.x + (hMovement * Time.deltaTime), tempStart2.y + (vMovement * Time.deltaTime));
            vMovement -= (Time.deltaTime * 10);
            Debug.DrawLine(tempStart1, tempStart2, Color.red, 1f);
            tempStart1 = tempStart2;
            yield return new WaitForEndOfFrame();
        }
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
        return attackRange;
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
        animator.Play("Stop");
        GameObject wreckClone = (GameObject)Instantiate(prefabDead, transform.position, transform.rotation);
        wreckClone.transform.localScale = transform.localScale;
        Destroy(wreckClone, 3);
        StopAllCoroutines();
    }
}

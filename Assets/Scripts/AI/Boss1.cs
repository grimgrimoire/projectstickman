using UnityEngine;
using System.Collections;
using System;

public class Boss1 : MonoBehaviour, IBaseEnemy
{
    public float walkSpeed = 4;
    public GameObject prefabSpear;
    public GameObject bossSpear;

    GameObject player;
    Vector3 playerPosition;
    float distanceFromPlayer;
    bool canMove = true;
    Animator animator;
    Rigidbody2D rigidBody;
    private float scale;

    float cooldown = 2;

    private static String ATTACK = "AttackState";

    // Use this for initialization
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        scale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown > 0)
            cooldown -= Time.deltaTime;
        UpdatePositions();
        RollAttackPlayer();
        //if (canMove)
        //    WalkToPlayer();
    }

    public void Attack()
    {
        //Leave this empty
    }

    public float AttackRange()
    {
        return 3;
    }

    public bool CanMove()
    {
        return canMove;
    }

    public void Dead()
    {
    }

    public void StopWalking()
    {
    }

    public void WalkAnimation()
    {
    }

    private void LookAtPlayer()
    {
        if (playerPosition.x > transform.position.x)
            LookRight();
        else
            LookLeft();
    }

    private void WalkToPlayer()
    {
        LookAtPlayer();
        if (distanceFromPlayer > 10)
        {
            if (playerPosition.x > transform.position.x)
                WalkRight();
            else if (playerPosition.x < transform.position.x)
                WalkLeft();
            else
                StopMoving();

        }
        else if (distanceFromPlayer < 9)
        {
            if (playerPosition.x > transform.position.x)
                WalkLeft();
            else if (playerPosition.x < transform.position.x)
                WalkRight();
            else
                StopMoving();
        }
        else
            StopMoving();
    }

    public void WalkStraight()
    {
        rigidBody.velocity = new Vector2(walkSpeed * transform.localScale.x, rigidBody.velocity.y);
    }

    public void WalkRight()
    {
        rigidBody.velocity = new Vector2(walkSpeed, rigidBody.velocity.y);
    }

    public void WalkLeft()
    {
        rigidBody.velocity = new Vector2(walkSpeed * -1, rigidBody.velocity.y);
    }

    public void LookLeft()
    {
        transform.localScale = new Vector3(-scale, transform.localScale.y, transform.localScale.z);
    }

    public void LookRight()
    {
        transform.localScale = new Vector3(scale, transform.localScale.y, transform.localScale.z);
    }

    private void StopMoving()
    {
        rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
    }

    private void UpdatePositions()
    {
        playerPosition = player.transform.position;
        distanceFromPlayer = Vector3.Distance(transform.position, playerPosition);
    }

    private void RollAttackPlayer()
    {
        if (canMove && cooldown <= 0)
        {
            StartCoroutine(Attack3());
        }
    }

    IEnumerator Attack1Consequences()
    {
        yield return Attack2();
        yield return Attack1();
    }

    IEnumerator Attack1() // Throwing Spear
    {
        LookAtPlayer();
        cooldown = 5f;
        canMove = false;
        SetAttackAnimation(1);
        yield return new WaitForSeconds(1.58f);
        LookAtPlayer();
        GameObject spear = Instantiate(prefabSpear);
        spear.transform.position = bossSpear.transform.position;
        spear.transform.rotation = bossSpear.transform.rotation;
        spear.GetComponent<Projectile>().minSpeed = 40;
        spear.GetComponent<Projectile>().speed = 40;
        spear.GetComponent<Projectile>().SetTargetPosition(playerPosition + new Vector3(0, 1));
        SetAttackAnimation(0);
        yield return new WaitForSeconds(0.5f);
        canMove = true;
    }

    IEnumerator Attack2() // Charge
    {
        cooldown = 7f;
        SetAttackAnimation(2);
        canMove = false;
        yield return new WaitForSeconds(2);
        SetAttackAnimation(4);
        float delta = 0.5f;
        walkSpeed = 20;
        while (delta > 0)
        {
            delta -= Time.deltaTime;
            WalkStraight();
            yield return new WaitForEndOfFrame();
        }
        walkSpeed = 4;
        SetAttackAnimation(0);
        StopMoving();
        yield return new WaitForSeconds(0.5f);
        canMove = true;
    }

    IEnumerator Attack3() // Jump
    {
        cooldown = 5f;
        canMove = false;
        SetAttackAnimation(3);
        yield return new WaitForSeconds(1.5f);
        SetAttackAnimation(0);
        canMove = true;
    }

    private void SetAttackAnimation(int value)
    {
        animator.SetInteger(ATTACK, value);
    }
}

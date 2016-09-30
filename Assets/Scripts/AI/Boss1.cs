using UnityEngine;
using System.Collections;
using System;

public class Boss1 : MonoBehaviour, IBaseEnemy
{
    public float walkSpeed = 2;
    public GameObject shockwavePrefab;
    GameObject player;
    Vector3 playerPosition;
    float distanceFromPlayer;
    bool canMove = true;
    Animator animator;
    Rigidbody2D rigidBody;
    private float scale;

    float cooldown = 0;

    private static String ATTACK = "AttackState";
    private static String WALK = "Walk";
    private float speedModifier = 1f;

    // Use this for initialization
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        scale = transform.localScale.x;
        animator.speed = animator.speed / speedModifier;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePositions();
        RollAttackPlayer();
        if (canMove)
            LookAtPlayer();
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

    public void WalkBack()
    {
        rigidBody.velocity = new Vector2(walkSpeed * transform.localScale.x * -1, rigidBody.velocity.y);
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
            if (distanceFromPlayer < 5)
            {
                StartCoroutine(Attack4());
            }
            /*else if (distanceFromPlayer < 7 && distanceFromPlayer > 5)
            {
                StartCoroutine(Attack2());
            }*/
            else
            {
                if (UnityEngine.Random.Range(1, 11) > 6)
                    Attack3();
                else
                    StartCoroutine(Attack1());
            }
        }
    }


    IEnumerator Attack1() // Throwing Spear
    {
        LookAtPlayer();
        cooldown = 1f * speedModifier;
        canMove = false;
        SetAttackAnimation(1);
        yield return new WaitForSeconds(3f * speedModifier);
        yield return AttackDone();
    }

    IEnumerator Attack2() // Charge
    {
        cooldown = 1f * speedModifier;
        SetAttackAnimation(2);
        canMove = false;
        yield return new WaitForSeconds(2 * speedModifier);
        //SetAttackAnimation(4);
        float delta = 0.5f;
        walkSpeed = 40;
        while (delta > 0)
        {
            delta -= Time.deltaTime;
            WalkStraight();
            yield return new WaitForEndOfFrame();
        }
        walkSpeed = 4;
        StopMoving();
        yield return AttackDone();
    }

    public void Attack3() // Jump
    {
        LookAtPlayer();
        cooldown = 2f * speedModifier;
        canMove = false;
        SetAttackAnimation(3);
    }

    IEnumerator Attack4()// Up thrust
    {
        cooldown = 1f * speedModifier;
        canMove = false;
        SetAttackAnimation(4);
        yield return new WaitForSeconds(1.5f * speedModifier);
        yield return AttackDone();
    }

    public void JumpToPlayer()
    {
        StartCoroutine(JumpTranslation());
    }

    IEnumerator JumpTranslation()
    {
        animator.speed = 0;
        rigidBody.velocity = new Vector2((playerPosition.x - transform.position.x), 10);
        while (rigidBody.velocity.y > 0)
        {
            yield return new WaitForEndOfFrame();
        }
        animator.speed = 1;
        rigidBody.velocity = new Vector2(0, -20);
        while (rigidBody.velocity.y < 0)
        {
            yield return new WaitForEndOfFrame();
        }
        GameObject shockwave = Instantiate(shockwavePrefab);
        shockwave.transform.position = Physics2D.Raycast(transform.position, Vector2.down, 5, ConstMask.MASK_WORLD).point;
        yield return new WaitForSeconds(1f * speedModifier);
        yield return AttackDone();
        Destroy(shockwave, 3);
    }

    IEnumerator AttackDone()
    {
        SetAttackAnimation(0);
        yield return new WaitForSeconds(0.5f);
        canMove = true;
        StartCoroutine(RandomWalk());
    }

    private void SetAttackAnimation(int value)
    {
        animator.SetInteger(ATTACK, value);
    }

    private void StartWalkAnimation(int value)
    {
        animator.SetInteger(WALK, value);
    }

    IEnumerator RandomWalk()
    {
        int direction = UnityEngine.Random.Range(-1, 2);
        StartWalkAnimation(direction);
        while (cooldown > 0)
        {
            if (cooldown > 0)
                cooldown -= Time.deltaTime;
            if (direction == -1)
                WalkBack();
            else if (direction == 1)
                WalkStraight();
            yield return new WaitForEndOfFrame();
        }
        StartWalkAnimation(0);
    }
}

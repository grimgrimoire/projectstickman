using UnityEngine;
using System.Collections;
using System;

public class BaseWalkingEnemy : MonoBehaviour
{
    public float walkSpeed = 7f;
    public float jumpHeight = 8f;

    IBaseEnemy iBaseEnemy;
    GameObject player;
    Vector3 playerPosition;
    Rigidbody2D rigidBody;
    BoxCollider2D hitbox;
    float distanceFromPlayer;
    private float scale;
    bool isGrounded;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rigidBody = GetComponent<Rigidbody2D>();
        iBaseEnemy = GetComponent<IBaseEnemy>();
        hitbox = GetComponent<BoxCollider2D>();
        scale = transform.localScale.x;
        if (player == null)
        {
            throw new Exception("Player tag not found!");
        }
        if (iBaseEnemy == null)
        {
            throw new Exception("Base enemy is not attached to a legitimate enemy object");
        }
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D ground1 = Physics2D.Raycast(transform.position - new Vector3((hitbox.bounds.size.x / 2) - hitbox.offset.x, 0), Vector2.down, 1.2f, ConstMask.MASK_WORLD);
        RaycastHit2D ground2 = Physics2D.Raycast(transform.position + new Vector3((hitbox.bounds.size.x / 2) + hitbox.offset.x, 0), Vector2.down, 1.2f, ConstMask.MASK_WORLD);
        isGrounded = ground1 || ground2;
        UpdatePositions();
        ChasePlayer();
    }

    private void ChasePlayer()
    {
        if (iBaseEnemy.CanMove())
        {
            TryChasePlayer();
        }
        else
        {
            StopMoving();
        }
    }

    private void TryChasePlayer()
    {
        if (!IsNextToPlayerHorizontally() && !PlayerOnLowerElevation() && (distanceFromPlayer > iBaseEnemy.AttackRange() || !HasLineOfSight()))
        {
            DirectlyChasePlayer();
        }
        if (PlayerOnLowerElevation() && (distanceFromPlayer > iBaseEnemy.AttackRange() || !HasLineOfSight()))
        {
            if (isGrounded)
                WalkStraight();
            else if (!isGrounded)
            {
                if (rigidBody.velocity.y < -1)
                    DirectlyChasePlayer();
            }
        }
        bool wall = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, 1, ConstMask.MASK_WORLD);
        bool ground = Physics2D.Raycast(transform.position, (Vector2.right * transform.localScale.x) + Vector2.down, 2, ConstMask.MASK_WORLD);
        if ((!IsNextToPlayerHorizontally() || PlayerOnHigherElevation()) && isGrounded && wall && IsFacingPlayer())
        {
            Jump();
        }
        else if (playerPosition.y - transform.position.y > 4)
        {
            if(isGrounded && Mathf.Abs(playerPosition.x - transform.position.x) > 4 && Mathf.Abs(playerPosition.x - transform.position.x) < 7)
            {
                Jump();
            }
        }
        if (!ground & isGrounded && !PlayerOnLowerElevation())
        {
            SmallJump();
        }
    }

    private bool IsFacingPlayer()
    {
        if (transform.localScale.x > 0 && transform.position.x < playerPosition.x)
        {
            return true;
        }
        else if (transform.localScale.x < 0 && transform.position.x > playerPosition.x)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void DirectlyChasePlayer()
    {
        if (playerPosition.x - transform.position.x > 0)
        {
            WalkRight();
        }
        else if (playerPosition.x - transform.position.x < 0)
        {
            WalkLeft();
        }
        else
        {
            StopMoving();
        }
    }

    public void WalkStraight()
    {
        rigidBody.velocity = new Vector2(walkSpeed * transform.localScale.x, rigidBody.velocity.y);
        iBaseEnemy.WalkAnimation();
    }

    public void WalkRight()
    {
        transform.localScale = new Vector3(scale, transform.localScale.y, transform.localScale.z);
        rigidBody.velocity = new Vector2(walkSpeed * transform.localScale.x, rigidBody.velocity.y);
        iBaseEnemy.WalkAnimation();
    }

    public void WalkLeft()
    {
        transform.localScale = new Vector3(-scale, transform.localScale.y, transform.localScale.z);
        rigidBody.velocity = new Vector2(walkSpeed * transform.localScale.x, rigidBody.velocity.y);
        iBaseEnemy.WalkAnimation();
    }

    private void StopMoving()
    {
        rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        iBaseEnemy.StopWalking();
    }

    private bool PlayerOnLowerElevation()
    {
        return playerPosition.y - transform.position.y < -2;
    }

    private bool PlayerOnHigherElevation()
    {
        return playerPosition.y - transform.position.y > 1;
    }

    private void Jump()
    {
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpHeight);
    }

    private void SmallJump()
    {
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpHeight / 2);
    }

    private bool IsNextToPlayerHorizontally()
    {
        return Mathf.Abs(transform.position.x - playerPosition.x) < 1;
    }

    public bool HasLineOfSight()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, playerPosition - transform.position, Mathf.Infinity, ConstMask.MASK_WORLD | ConstMask.MASK_PLAYER | ConstMask.MASK_SPAWNAREA);
        if (ray)
            return ray.collider.gameObject.tag == ConstMask.TAG_PLAYER;
        else
            return false;
    }

    private void UpdatePositions()
    {
        playerPosition = player.transform.position;
        distanceFromPlayer = Vector3.Distance(transform.position, playerPosition);
    }
}

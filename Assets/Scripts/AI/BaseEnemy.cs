using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class BaseEnemy : MonoBehaviour
{
    IBaseEnemy iBaseEnemy;

    public float walkSpeed = 7f;
    public float jumpHeight = 8f;
    public float health = 100;
    public GameObject healthBarParent;

    GameObject player;
    Vector3 playerPosition;
    Rigidbody2D rigidBody;
    BoxCollider2D hitbox;
    float distanceFromPlayer;
    RectTransform healthBar;
    private float scale;
    float maxHealth;
    bool isRespawn = true;
    bool isGrounded;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rigidBody = GetComponent<Rigidbody2D>();
        iBaseEnemy = GetComponent<IBaseEnemy>();
        hitbox = GetComponent<BoxCollider2D>();
        scale = transform.localScale.x;
        if (healthBarParent != null)
            healthBar = healthBarParent.transform.GetChild(0).GetComponent<RectTransform>();
        maxHealth = health;
        if (player == null)
        {
            throw new Exception("Player tag not found!");
        }
        if (iBaseEnemy == null)
        {
            throw new Exception("Base enemy is not attached to a legitimate enemy object");
        }
        StartCoroutine(RespawnDelay());
    }

    // Update is called once per frame
    void Update()
    {
        ShowDebug();
        if (health > 0)
        {
            RaycastHit2D ground1 = Physics2D.Raycast(transform.position - new Vector3((hitbox.bounds.size.x / 2) - hitbox.offset.x, 0), Vector2.down, 1.2f, ConstMask.MASK_WORLD);
            RaycastHit2D ground2 = Physics2D.Raycast(transform.position + new Vector3((hitbox.bounds.size.x / 2) + hitbox.offset.x, 0), Vector2.down, 1.2f, ConstMask.MASK_WORLD);
            isGrounded = ground1 || ground2;
            UpdatePositions();
            ChasePlayer();
            AttackPlayer();
            UpdateHealthBar();
        }

    }

    public float GetDistanceFromPlayer()
    {
        return distanceFromPlayer;
    }

    public Vector3 GetPlayerPosition()
    {
        return playerPosition;
    }

    public void TakeDamage(int damage, Vector2 hit)
    {
        health -= damage;
        UpdateHealthBar();
        if (health <= 0)
        {
            iBaseEnemy.Dead();
            StartCoroutine(Unspawn());
        }
        //GameObject.Find("UI").GetComponent<DamageTextHandler>().ShowDamage(damage, transform.position);
    }

    IEnumerator RespawnDelay()
    {
        yield return new WaitForSeconds(1f);
        isRespawn = false;
    }

    IEnumerator Unspawn()
    {
        yield return new WaitForSeconds(0);
        Destroy(this.gameObject);
    }

    public bool IsDead()
    {
        return health <= 0;
    }

    private void UpdateHealthBar()
    {
        if (healthBarParent != null)
        {
            healthBarParent.transform.localScale = transform.localScale;
            healthBarParent.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up);
            healthBar.sizeDelta = new Vector2((health / maxHealth) * 100, 5);
        }
    }

    private void ShowDebug()
    {
        if (distanceFromPlayer < iBaseEnemy.AttackRange())
            if(HasLineOfSight())
                Debug.DrawLine(transform.position, playerPosition, Color.red, 0.1f);
            else
                Debug.DrawLine(transform.position, playerPosition, Color.blue, 0.1f);
    }

    private void UpdatePositions()
    {
        playerPosition = player.transform.position;
        distanceFromPlayer = Vector3.Distance(transform.position, playerPosition);
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
            if(isGrounded)
                WalkStraight();
            else if(!isGrounded)
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
        return playerPosition.y - transform.position.y < -1;
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

    protected void AttackPlayer()
    {
        if (distanceFromPlayer <= iBaseEnemy.AttackRange() && !isRespawn)
        {
            iBaseEnemy.Attack();
        }
    }

    public bool HasLineOfSight()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, playerPosition - transform.position, Mathf.Infinity, ConstMask.MASK_WORLD | ConstMask.MASK_PLAYER);
        return ray.collider.gameObject.tag == ConstMask.TAG_PLAYER;
    }

}

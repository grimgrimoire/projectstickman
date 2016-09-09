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
    float distanceFromPlayer;
    RectTransform healthBar;
    private float scale;
    float maxHealth;
    bool isRespawn = true;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rigidBody = GetComponent<Rigidbody2D>();
        iBaseEnemy = GetComponent<IBaseEnemy>();
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
        //ShowDebug();
        if (health > 0)
        {
            UpdatePositions();
            LookAtPlayer();
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
            gameObject.layer = 12;
            gameObject.tag = "Untagged";
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
            Debug.DrawLine(transform.position, playerPosition, Color.red, 0.1f);
    }

    private void UpdatePositions()
    {
        playerPosition = player.transform.position;
        distanceFromPlayer = Vector3.Distance(transform.position, playerPosition);
    }

    private void LookAtPlayer()
    {
        if (iBaseEnemy.CanMove())
        {
            if (playerPosition.x < transform.position.x && !IsNextToPlayerHorizontally())
                transform.localScale = new Vector3(-scale, transform.localScale.y, transform.localScale.z);
            else
                transform.localScale = new Vector3(scale, transform.localScale.y, transform.localScale.z);
        }

    }

    private void ChasePlayer()
    {
        if (iBaseEnemy.CanMove())
        {
            if ((distanceFromPlayer >= iBaseEnemy.AttackRange() && !IsNextToPlayerHorizontally()) || isRespawn)
            {
                rigidBody.velocity = new Vector2(walkSpeed * transform.localScale.x, rigidBody.velocity.y);
                iBaseEnemy.WalkAnimation();
            }
        }
        else
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            iBaseEnemy.StopWalking();
        }

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

}

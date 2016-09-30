using UnityEngine;
using System;
using System.Collections;

public class BaseEnemy : MonoBehaviour
{
    IBaseEnemy iBaseEnemy;

    public float health = 100;
    public GameObject healthBarParent;
    public bool isBoss;

    GameObject player;
    Vector3 playerPosition;
    float distanceFromPlayer;
    RectTransform healthBar;
    float maxHealth;
    bool isRespawn = true;
    private EType eType;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        iBaseEnemy = GetComponent<IBaseEnemy>();
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
        if (health > 0)
        {
            UpdatePositions();
            AttackPlayer();
            UpdateHealthBar();
        }

    }

    public void SetEType(EType eType)
    {
        this.eType = eType;
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
            GameSystem.GetGameSystem().AddKillCount(eType);
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
        if (isBoss)
        {
            GameSystem.GetGameSystem().GetPlayerUI().UpdateBossHealth(health/maxHealth);
        }
        else if (healthBarParent != null)
        {
            healthBarParent.transform.localScale = transform.localScale;
            healthBarParent.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up);
            healthBar.sizeDelta = new Vector2((health / maxHealth) * 100, 5);
        }
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

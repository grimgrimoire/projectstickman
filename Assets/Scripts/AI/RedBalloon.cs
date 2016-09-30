using UnityEngine;
using System.Collections;
using System;

public class RedBalloon : MonoBehaviour, IBaseEnemy
{

    public float attackRange = 10f;
    //public float aimingDelay = 0.5f;
    public float Height = 10f;
    public float attackDelay = 5f;
    public float moveSpeed = 7f;
    float distanceFromPlayer;
    private float scale;
    Rigidbody2D rigidBody;
    BoxCollider2D hitbox;
    public GameObject prefabDead;

    public GameObject arrowPrefab;
    bool isAttacking;
    Vector3 playerPosition;
    float playerPositionY;
    float distanceFromPlayerY;
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rigidBody = GetComponent<Rigidbody2D>();
        scale = transform.localScale.x;

    }

    // Update is called once per frame
    void Update()
    {
        UpdatePositions();
        LookAtPlayer();
        TryChasePlayer();
    }

    public void Attack()
    {
        if (!isAttacking)
            StartCoroutine(ActionAttack());
    }

    IEnumerator ActionAttack()
    {
        isAttacking = true;
        ShootArrowAtPlayer();
        yield return new WaitForSeconds(attackDelay);
        isAttacking = false;
    }

    private void ShootArrowAtPlayer()
    {
        GameObject arrow = GameObject.Instantiate(arrowPrefab);
        arrow.transform.position = transform.position;
        arrow.GetComponent<Projectile>().SetTargetPosition(GetPlayerPosition());
    }

    public Vector3 GetPlayerPosition()
    {
        return playerPosition;
    }

    public void UpdatePositions()
    {
        playerPosition = player.transform.position;
        playerPositionY = player.transform.position.y;
        distanceFromPlayer = Vector3.Distance(transform.position, playerPosition);
        distanceFromPlayerY = Mathf.Abs(transform.position.y - playerPositionY);
    }

    private void LookAtPlayer()
    {
        if (playerPosition.x < transform.position.x)
            transform.localScale = new Vector3(-scale, transform.localScale.y, transform.localScale.z);
        else if (playerPosition.x > transform.position.x)
            transform.localScale = new Vector3(scale, transform.localScale.y, transform.localScale.z);

    }

    private void TryChasePlayer()
    {
        /*if (distanceFromPlayer >= attackRange)
        {
            if (playerPosition.x < transform.position.x)
                transform.position -= transform.right * moveSpeed * Time.deltaTime;
            else if (playerPosition.x > transform.position.x)
                transform.position += transform.right * moveSpeed * Time.deltaTime;
            else
                StopMoving();
        }
        else if (distanceFromPlayer < attackRange)
        {
            if (distanceFromPlayerY >= Height )
            {
                transform.position -= transform.up * moveSpeed * Time.deltaTime;
            }else if (distanceFromPlayerY < Height-1)
            {
                transform.position += transform.up * moveSpeed * Time.deltaTime;
            }
            StopMoving();
            Attack();
        }*/

        if (distanceFromPlayerY >= Height)
        {
            transform.position -= transform.up * moveSpeed * Time.deltaTime;
        }
        else if (distanceFromPlayerY < Height - 1)
        {
            transform.position += transform.up * moveSpeed * Time.deltaTime;

        }
        else if (distanceFromPlayer >= attackRange)
        {
            if (playerPosition.x < transform.position.x)
                transform.position -= transform.right * moveSpeed * Time.deltaTime;
            else if (playerPosition.x > transform.position.x)
                transform.position += transform.right * moveSpeed * Time.deltaTime;
        }
        else
            StopMoving();
        Attack();
    }
    private void StopMoving()
    {
        rigidBody.velocity = new Vector2(0, 0);
    }

    public bool CanMove()
    {
        return false;
    }

    public void WalkAnimation()
    {
    }

    public void StopWalking()
    {
    }

    public void Dead()
    {
        GameObject wreckClone = (GameObject)Instantiate(prefabDead, transform.position, transform.rotation);
        wreckClone.transform.localScale = transform.localScale;
        Destroy(wreckClone, 5);
    }

    public float AttackRange()
    {
        return attackRange;
    }
}

using UnityEngine;
using System;

public class BaseEnemy : MonoBehaviour
{
    IBaseEnemy iBaseEnemy;

    GameObject player;
    Vector3 playerPosition;
    Rigidbody2D rigidBody;
    float distanceFromPlayer;
    public bool isRanged;
    public float jumpHeight = 8f;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rigidBody = GetComponent<Rigidbody2D>();
        iBaseEnemy = GetComponent<IBaseEnemy>();
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
        ShowDebug();
        UpdatePositions();
        LookAtPlayer();
        ChasePlayer();
        AttackPlayer();
    }

    public float GetDistanceFromPlayer()
    {
        return distanceFromPlayer;
    }

    public Vector3 GetPlayerPosition()
    {
        return playerPosition;
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
            if (playerPosition.x < transform.position.x)
                transform.localScale = new Vector3(-1, 1, 1);
            else
                transform.localScale = new Vector3(1, 1, 1);
    }

    private void ChasePlayer()
    {
        if (iBaseEnemy.CanMove() && iBaseEnemy.WalkingSpeed() > 0)
        {
            if (distanceFromPlayer >= iBaseEnemy.AttackRange())
            {
                rigidBody.velocity = new Vector2(iBaseEnemy.WalkingSpeed() * transform.localScale.x, rigidBody.velocity.y);
                iBaseEnemy.WalkAnimation();
            }
        }
        else
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            iBaseEnemy.StopWalking();
        }

    }

    private void Pathfinding()
    {

    }

    private void AttackPlayer()
    {
        if (distanceFromPlayer <= iBaseEnemy.AttackRange())
        {
            iBaseEnemy.Attack();
        }
    }

}

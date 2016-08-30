using UnityEngine;
using System;

public class BaseEnemy : MonoBehaviour
{
    IAttackPlayer iAttackPlayer;

    GameObject player;
    Vector3 playerPosition;
    Rigidbody2D rigidBody;
    float distanceFromPlayer;
    public float moveSpeed = 6f;
    public float engagementRange = 1.5f;
    public bool isRanged;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rigidBody = GetComponent<Rigidbody2D>();
        iAttackPlayer = GetComponent<IAttackPlayer>();

        if(player == null)
        {
            throw new Exception("Player tag not found!");
        }
        if(iAttackPlayer == null)
        {
            throw new Exception("Base enemy is not attached to a legitimate enemy object");
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePositions();
        LookAtPlayer();
        ChasePlayer();
        AttackPlayer();
    }

    private void UpdatePositions()
    {
        playerPosition = player.transform.position;
        distanceFromPlayer = Vector3.Distance(transform.position, playerPosition);
    }

    private void LookAtPlayer()
    {
        if (playerPosition.x < transform.position.x)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);
    }

    private void ChasePlayer()
    {
        if (distanceFromPlayer >= engagementRange)
        {
            rigidBody.velocity = new Vector2(moveSpeed * transform.localScale.x, rigidBody.velocity.y);
        }
        else
            rigidBody.velocity = Vector2.zero;
    }

    private void AttackPlayer()
    {
        if(distanceFromPlayer < engagementRange)
        {
            iAttackPlayer.Attack();
        }
    }

}

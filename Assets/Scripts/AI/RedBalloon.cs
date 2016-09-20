using UnityEngine;
using System.Collections;

public class RedBalloon : MonoBehaviour {

    public float attackRange = 10f;
    //public float aimingDelay = 0.5f;
    public float Height = 10f;
    public float attackDelay = 5f;
    public float moveSpeed = 7f;
    float distanceFromPlayer;
    private float scale;
    Rigidbody2D rigidBody;
    public GameObject healthBarParent;
    RectTransform healthBar;
    float maxHealth;
    public float health = 20;
    BoxCollider2D hitbox;

    public GameObject arrowPrefab;
    bool isAttacking;
    Vector3 playerPosition;
    float playerPositionY;
    float distanceFromPlayerY;
    GameObject player;
    bool AttackTrue=false;

    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        rigidBody = GetComponent<Rigidbody2D>();
        scale = transform.localScale.x;

        if (healthBarParent != null)
            healthBar = healthBarParent.transform.GetChild(0).GetComponent<RectTransform>();
        maxHealth = health;
    }
	
	// Update is called once per frame
	void Update () {
        if (health > 0)
        {
            UpdatePositions();
            LookAtPlayer();
            TryChasePlayer();
            UpdateHealthBar();
        }
        
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
        playerPositionY= player.transform.position.y;
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
        else if (distanceFromPlayerY < Height-1)
        {
            transform.position += transform.up * moveSpeed * Time.deltaTime;
            
        }else if (distanceFromPlayer >= attackRange)
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

    private void UpdateHealthBar()
    {
        if (healthBarParent != null)
        {
            healthBarParent.transform.localScale = transform.localScale;
            healthBarParent.transform.position = Camera.main.WorldToScreenPoint(transform.position + 2*Vector3.up);
            healthBar.sizeDelta = new Vector2((health / maxHealth) * 100, 5);
        }
    }

    public void TakeDamage(int damage, Vector2 hit)
    {
        health -= damage;
        UpdateHealthBar();
        if (health <= 0)
        {
            
        }
        //GameObject.Find("UI").GetComponent<DamageTextHandler>().ShowDamage(damage, transform.position);
    }
}

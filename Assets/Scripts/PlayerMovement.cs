using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject weaponTarget;
    public GameObject playerAimingArm;
    public PlayerAnimation playerAnimation;

    public float moveSpeed = 5f;
    public float jumpHeight = 8f;
    bool isGrounded;
    bool isAlive;
    bool isDucking;
    bool isFirstMoveInput;
    Rigidbody2D rigid;
    bool canMove;
    GunScript gun;
    BoxCollider2D hitbox;

    Vector3 leftHitbox;
    Vector3 rightHitbox;

    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        gun = GetComponentInChildren<GunScript>();
        if (playerAnimation == null)
        {
            playerAnimation = GetComponentInChildren<PlayerAnimation>();
        }
        gun.ChangeWeapon(WeaponsList.GetPrimaryWeaponOnIndex(
            GameSession.GetSession().GetPlayer().GetPrimaryWeapon()));
        hitbox = GetComponent<BoxCollider2D>();
        leftHitbox = new Vector3((hitbox.bounds.size.x / 2) - hitbox.offset.x, 0);
        rightHitbox = new Vector3((hitbox.bounds.size.x / 2) + hitbox.offset.x, 0);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D ground1 = Physics2D.Raycast(transform.position - leftHitbox, Vector2.down, 1.3f, ConstMask.MASK_WORLD);
        RaycastHit2D ground2 = Physics2D.Raycast(transform.position + rightHitbox, Vector2.down, 1.3f, ConstMask.MASK_WORLD);
        //Debug.DrawLine(transform.position - new Vector3((hitbox.bounds.size.x / 2) - hitbox.offset.x, 0), transform.position - new Vector3((hitbox.bounds.size.x / 2) - hitbox.offset.x, 1.3f), Color.red);
        //Debug.DrawLine(transform.position + new Vector3((hitbox.bounds.size.x / 2) + hitbox.offset.x, 0), transform.position + new Vector3((hitbox.bounds.size.x / 2) + hitbox.offset.x, -1.3f), Color.blue);
        RaycastHit2D groundSlope = Physics2D.Raycast(transform.position, Vector2.down, 1.3f, ConstMask.MASK_WORLD);
        isGrounded = ground1 && groundSlope || ground2 && groundSlope;
        if (groundSlope)
        {
            transform.rotation = groundSlope.collider.gameObject.transform.rotation;
        }
        else
        {
            transform.rotation = new Quaternion();
        }
    }

    public void SetAlive(bool isAlive)
    {
        this.isAlive = isAlive;
    }

    public void updateWeaponHold(bool isTwoHanded)
    {
        playerAnimation.SetHoldingAnimation(isTwoHanded);
    }

    public void HoldTrigger()
    {
        gun.HoldTrigger();
    }

    public void RemoveTrigger()
    {
        gun.RemoveTrigger();
    }

    public void UpdateAim(float angle)
    {
        if (isGrounded)
            angle -= transform.rotation.eulerAngles.z * transform.localScale.x;
        if (isAlive)
            playerAnimation.SetFloat("Aim", angle / 180);
    }

    public void MoveRight(float multiplier)
    {
        Move(1);
    }

    public void MoveLeft(float multiplier)
    {
        Move(-1);
    }

    /**
     * @direction 1 = right  -1 left
     */
    private void Move(float direction)
    {
        if (canMove && isAlive)
        {
            if (isFirstMoveInput && isGrounded)
            {
                isFirstMoveInput = false;
                rigid.velocity = new Vector2(moveSpeed * direction, -2);
            }
            else
                rigid.velocity = new Vector2(moveSpeed * direction, rigid.velocity.y);
            if (isGrounded)
            {
                playerAnimation.StartWalking(direction == transform.localScale.x);
            }
            else
                playerAnimation.Jump();
        }
    }

    public void LookRight()
    {
        if (isAlive)
            transform.localScale = new Vector3(1, 1, 1);
    }

    public void LookLeft()
    {
        if (isAlive)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    public void Standing()
    {
        canMove = true;
        isDucking = false;
    }

    public void Jump()
    {
        if (isGrounded && isAlive)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpHeight);
            playerAnimation.Jump();
        }
    }

    public void Duck()
    {
        if (isGrounded && isAlive && !isDucking)
        {
            isDucking = true;
            canMove = false;
            rigid.velocity = new Vector2(0, -4);
            playerAnimation.Duck();
        }
    }

    public void Stop()
    {
        if (isGrounded)
            rigid.velocity = new Vector2(0, 0);
        else
            rigid.velocity = new Vector2(0, rigid.velocity.y);
        playerAnimation.StopMovement(Vector2.zero);
        isFirstMoveInput = true;
    }

    public void ChangeWeapon(WeaponsPrefab weapon)
    {
        gun.ChangeWeapon(weapon);
    }

}

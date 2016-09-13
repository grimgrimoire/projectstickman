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
    Rigidbody2D rigid;
    bool canMove;
    GunScript gun;

    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        gun = GetComponentInChildren<GunScript>();
        if (playerAnimation == null)
        {
            playerAnimation = GetComponentInChildren<PlayerAnimation>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, ConstMask.MASK_WORLD);
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
        if(isAlive)
            playerAnimation.SetFloat("Aim", angle / 180);
    }

    public void MoveRight(float multiplier)
    {
        if (canMove && isAlive)
        {
            rigid.velocity = new Vector2(moveSpeed, rigid.velocity.y);
            if (isGrounded)
                playerAnimation.StartWalking();
            else
                playerAnimation.Jump();
        }
    }

    public void MoveLeft(float multiplier)
    {
        if (canMove && isAlive)
        {
            rigid.velocity = new Vector2(-moveSpeed, rigid.velocity.y);
            if (isGrounded)
                playerAnimation.StartWalking();
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
        if (isGrounded && isAlive)
        {
            canMove = false;
            rigid.velocity = new Vector2(0, -4);
            playerAnimation.Duck();
        }
    }

    public void Stop()
    {
        rigid.velocity = new Vector2(0, rigid.velocity.y);
        playerAnimation.StopMovement(Vector2.zero);
    }

}

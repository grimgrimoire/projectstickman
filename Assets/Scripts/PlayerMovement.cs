using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject weaponTarget;
    public GameObject playerAimingArm;
    public StartAnimasi anim;

    float defaultAim = 300;
    public float moveSpeed = 5f;
    public float jumpHeight = 8f;
    bool isGrounded;
    Rigidbody2D rigid;
    bool canMove;
    GunScript gun;

    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        gun = GetComponentInChildren<GunScript>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, 512);
        isGrounded = ray;
        if (ray)
        {
            Debug.DrawLine(transform.position, ray.point, Color.red, 0.1f);
        }
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
        //playerAimingArm.transform.localEulerAngles = new Vector3(0, 0, 300 + angle);
        anim.SetFloat("Aim", angle / 180);
    }

    public void MoveRight(float multiplier)
    {
        if (canMove)
        {
            rigid.velocity = new Vector2(moveSpeed, rigid.velocity.y);
            if (isGrounded)
                anim.startanimation(1);
            else
                anim.Jump();
        }
    }

    public void MoveLeft(float multiplier)
    {
        if (canMove)
        {
            rigid.velocity = new Vector2(-moveSpeed, rigid.velocity.y);
            if (isGrounded)
                anim.startanimation(1);
            else
                anim.Jump();
        }
    }

    public void LookRight()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }

    public void LookLeft()
    {
        transform.localScale = new Vector3(-1, 1, 1);
    }

    public void Standing()
    {
        canMove = true;
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpHeight);
            anim.Jump();
        }
    }

    public void Duck()
    {
        if (isGrounded)
        {
            canMove = false;
            rigid.velocity = new Vector2(0, -4);
            anim.Duck();
        }
    }

    public void Stop()
    {
        rigid.velocity = new Vector2(0, rigid.velocity.y);
        anim.stopanimation(Vector2.zero);
    }

}

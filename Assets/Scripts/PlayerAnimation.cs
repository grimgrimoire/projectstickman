using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour
{

    Animator anim;
    //int walkHash = Animator.StringToHash("Walk");
    //int Stop = Animator.StringToHash("Stop");

    public void StartWalking(bool isToFront)
    {
        if (isToFront)
            anim.Play("Walk");
        else
            anim.Play("BackWalk");
    }

    public void SetHoldingAnimation(bool isTwoHanded)
    {
        anim.SetBool("IsTwoHanded", isTwoHanded);
    }

    public void SetFloat(string name, float value)
    {
        anim.SetFloat(name, value);
    }

    public void StopMovement(Vector2 stop)
    {
        if (stop == Vector2.zero)
        {
            anim.Play("Stop");
        }
    }

    public void Jump()
    {
        anim.Play("Jump");
    }

    public void Duck()
    {
        anim.Play("Duck");
    }


    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}

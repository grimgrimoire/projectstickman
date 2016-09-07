using UnityEngine;
using System.Collections;

public class StartAnimasi : MonoBehaviour
{

    Animator anim;
    //int walkHash = Animator.StringToHash("Walk");
    //int Stop = Animator.StringToHash("Stop");
    int speeding;

    public void startanimation(float x)
    {
        anim.Play("Walk");
    }

    public void stopanimation(Vector2 stop)
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

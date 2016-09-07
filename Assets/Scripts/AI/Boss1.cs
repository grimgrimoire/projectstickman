using UnityEngine;
using System.Collections;
using System;

public class Boss1 : MonoBehaviour, IBaseEnemy {

    public void Attack()
    {
    }

    public float AttackRange()
    {
        return 3;
    }

    public bool CanMove()
    {
        return true;
    }

    public void Dead()
    {
    }

    public void StopWalking()
    {
    }

    public void WalkAnimation()
    {
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

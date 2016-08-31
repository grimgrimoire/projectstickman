using System;
using UnityEngine;

public abstract class GunScript : MonoBehaviour, IShootBullet
{

    public float accuracy;
    public float fireRateDelay;
    public bool isAutomatic;
    public int bulletPerShot;
    public int damage;
    public float damageFalloff;

    public void HoldTrigger()
    {

    }

    public void RemoveTrigger()
    {

    }
}

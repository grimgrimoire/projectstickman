using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;

public class WeaponsPrefab
{
    public float accuracy;
    public float fireRateDelay;
    public int bulletPerShot;
    public int minDamage;
    public int maxDamage;
    public float damageFalloff;
    public bool isTwoHanded;
    public bool isAutomatic;
    public Vector2 weaponTargetPosition;
    public int spriteIndex;

    public WeaponsPrefab()
    {

    }

    public WeaponsPrefab(float accuracy, float fireRate, int bullets, int minDamage, int maxDamage, float damageFall, bool isTwohanded, bool automatic)
    {
        this.accuracy = accuracy;
        this.fireRateDelay = fireRate;
        this.bulletPerShot = bullets;
        this.minDamage = minDamage;
        this.maxDamage = maxDamage;
        this.damageFalloff = damageFall;
        this.isTwoHanded = isTwohanded;
        this.isAutomatic = automatic;
    }
}

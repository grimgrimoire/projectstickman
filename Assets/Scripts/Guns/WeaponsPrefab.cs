using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;

public class WeaponsPrefab
{
    public string name;
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

    public WeaponsPrefab(string name, float accuracy, float fireRateDelay, int bulletPerShot, int minDamage, int maxDamage, float damageFalloff, bool isTwoHanded, bool isAutomatic)
    {
        this.name = name;
        this.accuracy = accuracy;
        this.fireRateDelay = fireRateDelay;
        this.bulletPerShot = bulletPerShot;
        this.minDamage = minDamage;
        this.maxDamage = maxDamage;
        this.damageFalloff = damageFalloff;
        this.isTwoHanded = isTwoHanded;
        this.isAutomatic = isAutomatic;
    }
}

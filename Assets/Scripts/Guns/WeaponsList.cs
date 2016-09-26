using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class WeaponsList
{
    public static WeaponsPrefab Pistol()
    {
        WeaponsPrefab pistol = new WeaponsPrefab(100, 0.5f, 1, 7, 10, 10, false, false);
        pistol.spriteIndex = 10;
        pistol.weaponTargetPosition = new Vector2(0.48f, 0.12f);
        return pistol;
    }

    public static WeaponsPrefab Assault()
    {
        WeaponsPrefab Assault = new WeaponsPrefab(90, 0.2f, 1, 3, 5, 10, true, true);
        Assault.spriteIndex = 16;
        Assault.weaponTargetPosition = new Vector2(1.65f, 0.309f);
        return Assault;
    }

    public static WeaponsPrefab Shotgun()
    {
        WeaponsPrefab shotgun = new WeaponsPrefab(50, 0.8f, 8, 3, 3, 5, true, false);
        shotgun.spriteIndex = 18;
        shotgun.weaponTargetPosition = new Vector2(1.6f, 0.2f);
        return shotgun;
    }

    public static WeaponsPrefab Revolver()
    {
        WeaponsPrefab revolver = new WeaponsPrefab(100, 0.7f, 1, 15, 20, 10, false, false);
        revolver.spriteIndex = 5;
        revolver.weaponTargetPosition = new Vector2(0.45f, 0.13f);
        return revolver;
    }

    public static WeaponsPrefab Magnum()
    {
        WeaponsPrefab Magnum = new WeaponsPrefab(100, 0.5f, 1, 15, 20, 10, false, false);
        Magnum.spriteIndex = 7;
        Magnum.weaponTargetPosition = new Vector2(0.60f, 0.13f);
        return Magnum;
    }

}

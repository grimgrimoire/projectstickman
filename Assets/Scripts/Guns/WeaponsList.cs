using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class WeaponsList
{

    public static int TOTAL_PRIMARY = 2;
    public static int TOTAL_SECONDARY = 3;

    public static WeaponsPrefab GetSecondaryWeaponOnIndex(int index)
    {
        switch (index)
        {
            case 0: return Pistol();
            case 1: return Revolver();
            case 2: return Magnum();
            default: throw new System.Exception("Weapon index OUT OF BOUNDS!!");
        }
    }

    public static WeaponsPrefab GetPrimaryWeaponOnIndex(int index)
    {
        switch (index)
        {
            case 0: return Assault();
            case 1: return Shotgun();
            default: throw new System.Exception("Weapon index OUT OF BOUNDS!!");
        }
    }

    // PRIMARY =============================================================================

    public static WeaponsPrefab Assault()
    {
        WeaponsPrefab Assault = new WeaponsPrefab("Assault", 90, 0.2f, 1, 3, 5, 10, true, true);
        Assault.spriteIndex = 16;
        Assault.weaponTargetPosition = new Vector2(1.65f, 0.309f);
        return Assault;
    }

    public static WeaponsPrefab Shotgun()
    {
        WeaponsPrefab shotgun = new WeaponsPrefab("Shotgun", 50, 0.8f, 8, 3, 3, 5, true, false);
        shotgun.spriteIndex = 18;
        shotgun.weaponTargetPosition = new Vector2(1.6f, 0.2f);
        return shotgun;
    }

    // SECONDARY =============================================================================

    public static WeaponsPrefab Pistol()
    {
        WeaponsPrefab pistol = new WeaponsPrefab("Pistol", 100, 0.5f, 1, 7, 10, 10, false, false);
        pistol.spriteIndex = 10;
        pistol.weaponTargetPosition = new Vector2(0.48f, 0.12f);
        return pistol;
    }

    public static WeaponsPrefab Revolver()
    {
        WeaponsPrefab revolver = new WeaponsPrefab("Revolver", 100, 0.7f, 1, 15, 20, 10, false, false);
        revolver.spriteIndex = 5;
        revolver.weaponTargetPosition = new Vector2(0.45f, 0.13f);
        return revolver;
    }

    public static WeaponsPrefab Magnum()
    {
        WeaponsPrefab Magnum = new WeaponsPrefab("Magnum", 100, 0.5f, 1, 15, 20, 10, false, false);
        Magnum.spriteIndex = 7;
        Magnum.weaponTargetPosition = new Vector2(0.60f, 0.13f);
        return Magnum;
    }

}

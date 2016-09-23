using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class WeaponsList
{
    public static WeaponsPrefab Pistol()
    {
        WeaponsPrefab pistol = new WeaponsPrefab(100, 0.7f, 1, 7, 10, 20, false, false);
        pistol.spriteIndex = 0;
        pistol.weaponTargetPosition = new Vector2(0.545f, 0.209f);
        return pistol;
    }

    public static WeaponsPrefab Assault()
    {
        WeaponsPrefab assault = new WeaponsPrefab(95, 0.18f, 1, 5, 8, 20, true, true);
        assault.spriteIndex = 16;
        assault.weaponTargetPosition = new Vector2(1.634f, 0.33f);
        return assault;
    }

}

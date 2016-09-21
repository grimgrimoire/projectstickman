using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class WeaponsList
{
    public static WeaponsPrefab Pistol()
    {
        WeaponsPrefab pistol = new WeaponsPrefab(100, 0.7f, 1, 7, 10, 10, false, false);
        pistol.spriteIndex = 0;
        pistol.weaponTargetPosition = new Vector2(0.545f, 0.209f);
        return pistol;
    }

}

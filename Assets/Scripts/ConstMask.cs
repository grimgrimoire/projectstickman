using UnityEngine;
using System.Collections;

public class ConstMask {

    public const string TAG_PLAYER = "Player";
    public const string TAG_ENEMY = "Hostile";
    public const string TAG_WORLD = "World";
    public const string TAG_PROJECTILE = "Projectile";

    public const int MASK_PLAYER = 1 << 8;
    public const int MASK_WORLD = 1 << 9;
    public const int MASK_PROJECTILE = 1 << 10;
    public const int MASK_ENEMY = 1 << 11;
    public const int MASK_DEAD = 1 << 12;
    public const int MASK_SPAWNAREA = 1 << 13;

    public const string GAME_SYSTEM = "GameSystem";

    public const int SCENE_MAIN_MENU = 0;
}

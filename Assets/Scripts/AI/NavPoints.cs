using System;
using System.Collections;
using UnityEngine;

class NavPoints
{
    public enum NavType {none, platform, edgeLeft, edgeRight, solo}

    public Vector2 coordinate;
    public int jumpValue = 0;
    public NavType navType;
    public int value;
    public int deltaOrigin;
    public int deltaTarget;
}
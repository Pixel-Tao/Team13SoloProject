using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Defines
{
    public enum ELayerMask
    {
        Default = 0,
        TransparentFX = 1,
        IgnoreRaycast = 2,
        Water = 4,
        UI = 5,
        Player = 6,
        Enemy = 7,
        Npc = 8,
    }

    public enum  EPoolTarget
    {
        None,
        Player,
        Npc,
        Enemy,
        Projectile,
    }
}

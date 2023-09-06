using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Traps Configuratiosn")]
public class Traps_Config : ScriptableObject
{
    public Level_Configurations[] levels;
}

[Serializable]
public class Level_Configurations
{
    [Header("Trap")]
    [Space(5)]
    public float Range;
    public float CoolDown;
    
    [Header("Bullet")]
    [Space(5)]

    public int Damage;
    public float Power;

    [Header("Level Cost")]
    [Space(5)]

    public int Coins;
    public int Weed;
}

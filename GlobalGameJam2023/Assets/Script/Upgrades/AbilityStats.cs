using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using NaughtyAttributes;

[Serializable]
public class AbilityStats
{
    [HideInInspector] public int UpgradeLevel;
    public bool IsAbilityUnlocked()
    {
        return UpgradeLevel > 0;
    }
}

[Serializable]
public class DashStats : AbilityStats
{
    [SerializeField] private float baseDistance;
    public float BaseDistance{ get { return baseDistance; } }

    [SerializeField] private float baseCooldown;
    public float BaseCooldown{ get { return baseCooldown; } }

    [SerializeField] private int baseCharges;
    public int BaseCharges{ get { return baseCharges; } }

    public DashStats()
    {
        Init();
    }

    public void Init()
    {
        Distance = baseDistance;
        Cooldown = baseCooldown;
        Charges = baseCharges;
    }

    [HideInInspector]public float Distance;
    [HideInInspector]public float Cooldown;
    [HideInInspector]public int Charges;
}


[Serializable]
public class ThronsStats : AbilityStats
{
    [SerializeField] private float baseDamage;
    public float BaseDamage{ get { return baseDamage; } }

    [SerializeField] private float baseCooldown;
    public float BaseCooldown{ get { return baseCooldown; } }

    [SerializeField] private int baseShootingDirections;
    public int BaseShootingDirections{ get { return baseShootingDirections; } }

    public ThronsStats()
    {
        Init();
    }

    public void Init()
    {
        Damage = baseDamage;
        Cooldown = baseCooldown;
        ShootingDirections = baseShootingDirections;
    }

    [HideInInspector]public float Damage;
    [HideInInspector]public float Cooldown;
    [HideInInspector]public int ShootingDirections;
}

[Serializable]
public class GlaiveStats : AbilityStats
{
    [SerializeField] private float baseDamage;
    public float BaseDamage{ get { return baseDamage; } }

    [SerializeField] private float baseSpeed;
    public float BaseSpeed{ get { return baseSpeed; } }

    [SerializeField] private float baseSize;
    public float BaseSize{ get { return baseSize; } }

    public GlaiveStats()
    {
        Init();
    }
    
    public void Init()
    {
        Damage = baseDamage;
        Speed = baseSpeed;
        Size = baseSize;
    }


    [HideInInspector]public float Damage;
    [HideInInspector]public float Speed;
    [HideInInspector]public float Size;
}

[Serializable]
public class ClusterBombStats : AbilityStats
{
    [SerializeField] private float baseDamage;
    public float BaseDamage{ get { return baseDamage; } }

    [SerializeField] private int baseBombAmount;
    public int BaseBombAmount{ get { return baseBombAmount; } }

    [SerializeField] private float baseSize;
    public float BaseSize{ get { return baseSize; } }

    public ClusterBombStats()
    {
        Init();
    }

    public void Init()
    {
        Damage = baseDamage;
        BombAmount = baseBombAmount;
        Size = baseSize;
    }

    [HideInInspector]public float Damage;
    [HideInInspector]public int BombAmount;
    [HideInInspector]public float Size;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class DamageUpgrade : PlayerUpgrade
{
    public DamageUpgrade(PlayerStats playerStats) : base("Damage", "Increases the damage of all the palyers' projectiles", 5, playerStats)
    {
    }

    public override void ChooseUpgrade()
    {
        throw new NotImplementedException();
    }
}

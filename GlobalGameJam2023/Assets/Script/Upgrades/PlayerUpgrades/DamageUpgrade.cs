using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class DamageUpgrade : PlayerUpgrade
{
    public DamageUpgrade(PlayerStats playerStats) : base("Damage", "Increases the damage of all the players' projectiles", 5, playerStats)
    {
        
    }

    public override void ChooseUpgrade()
    {
        base.ChooseUpgrade();
        playerStats.Damage = playerStats.BaseDamage + (2 * UpgradeLevel);
    }
}

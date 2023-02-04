using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ProjectileAmountUpgrade : PlayerUpgrade
{
    public ProjectileAmountUpgrade(PlayerStats playerStats) : base("Projectile Amount", "Increases the amount of projectiles you shoot", 4, playerStats)
    {
        
    }

    public override void ChooseUpgrade()
    {
        base.ChooseUpgrade();
        playerStats.ProjectileAmount = playerStats.BaseProjectileAmount + UpgradeLevel;
        Debug.Log(playerStats.ProjectileAmount);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ProjectileSpeedUpgrade : PlayerUpgrade
{
    public ProjectileSpeedUpgrade(PlayerStats playerStats) : base("Projectile Speed", "Increases the speed of projectiles", 5, playerStats)
    {
        
    }

    public override void ChooseUpgrade()
    {
        base.ChooseUpgrade();
        playerStats.ProjectileSpeed = playerStats.BaseProjectileSpeed * (1.0f + ((float)UpgradeLevel / 5.0f * 0.75f));
        Debug.Log(playerStats.ProjectileSpeed);
    }
}

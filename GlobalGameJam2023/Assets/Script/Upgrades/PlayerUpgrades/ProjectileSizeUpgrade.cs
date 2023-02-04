using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ProjectileSizeUpgrade : PlayerUpgrade
{
    public ProjectileSizeUpgrade(PlayerStats playerStats) : base("Projectile Size", "Increases the size of your projectiles", 5, playerStats)
    {
        
    }

    public override void ChooseUpgrade()
    {
        base.ChooseUpgrade();
        playerStats.ProjectileSize = playerStats.BaseProjectileSize * (1.0f + ((float)UpgradeLevel / 10));
        Debug.Log(playerStats.ProjectileSize);
    }
}

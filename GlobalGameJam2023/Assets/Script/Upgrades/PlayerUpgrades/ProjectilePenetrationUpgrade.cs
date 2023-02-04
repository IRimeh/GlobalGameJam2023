using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ProjectilePenetrationUpgrade : PlayerUpgrade
{
    public ProjectilePenetrationUpgrade(PlayerStats playerStats) : base("Projectile Penetration", "Increases the amount enemies a projectile can shoot through", 3, playerStats)
    {
        
    }

    public override void ChooseUpgrade()
    {
        base.ChooseUpgrade();
        playerStats.ProjectilePenetration = playerStats.BaseProjectilePenetration + UpgradeLevel;
        Debug.Log(playerStats.ProjectilePenetration);
    }
}

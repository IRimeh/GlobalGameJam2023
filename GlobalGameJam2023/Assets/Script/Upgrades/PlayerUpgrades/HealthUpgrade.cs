using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class HealthUpgrade : PlayerUpgrade
{
    public HealthUpgrade(PlayerStats playerStats) : base("Health", "Increases the health of the player", 5, playerStats)
    {
        
    }

    public override void ChooseUpgrade()
    {
        base.ChooseUpgrade();
        playerStats.Health = (int)((float)playerStats.BaseHealth * (1.0f + (float)UpgradeLevel / 5));
    }
}

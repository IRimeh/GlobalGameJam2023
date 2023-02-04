using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class FireRateUpgrade : PlayerUpgrade
{
    public FireRateUpgrade(PlayerStats playerStats) : base("Fire Rate", "Increases the fire rate of the player", 5, playerStats)
    {
        
    }

    public override void ChooseUpgrade()
    {
        base.ChooseUpgrade();
        playerStats.FireRate = playerStats.BaseFireRate * (1.0f + ((float)UpgradeLevel / 5));
        Debug.Log(playerStats.FireRate);
    }
}

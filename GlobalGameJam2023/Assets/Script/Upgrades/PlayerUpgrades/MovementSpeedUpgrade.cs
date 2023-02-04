using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MovementSpeedUpgrade : PlayerUpgrade
{
    public MovementSpeedUpgrade(PlayerStats playerStats) : base("Movement Speed", "Increases the movement speed of the player", 5, playerStats)
    {
        
    }

    public override void ChooseUpgrade()
    {
        base.ChooseUpgrade();
        playerStats.MovementSpeed = playerStats.BaseMovementSpeed * (1.0f + ((float)UpgradeLevel / 10));
        Debug.Log(playerStats.MovementSpeed);
    }
}

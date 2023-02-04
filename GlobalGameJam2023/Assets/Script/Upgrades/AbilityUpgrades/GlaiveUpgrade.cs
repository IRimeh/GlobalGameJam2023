using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlaiveUpgrade : AbilityUpgrade
{
    public GlaiveUpgrade(PlayerStats playerStats) : base("Glaive Upgrade", "Upgrades the players glaive ability", 6, playerStats, new List<string>()
    {
        "Unlocks the glaive ability",
        "Increases the speed of the glaive",
        "Increases the damage of the glaive",
        "Increases the speed of the glaive",
        "Increases the damage of the glaive",
        "Increases the size and damage of your glaive"
    })
    {
    }

    public override void ChooseUpgrade()
    {
        base.ChooseUpgrade();
        playerStats.GlaiveStats.UpgradeLevel = UpgradeLevel;

        switch(UpgradeLevel)
        {
            case 2:
                playerStats.GlaiveStats.Speed = playerStats.GlaiveStats.BaseSpeed * 1.25f;
                break;
            case 3:
                playerStats.GlaiveStats.Damage = playerStats.GlaiveStats.BaseDamage + 3;
                break;
            case 4:
                playerStats.GlaiveStats.Speed = playerStats.GlaiveStats.BaseSpeed * 1.5f;
                break;
            case 5:
                playerStats.GlaiveStats.Damage = playerStats.GlaiveStats.BaseDamage + 6;
                break;
            case 6:
                playerStats.GlaiveStats.Damage = playerStats.GlaiveStats.BaseDamage + 10;
                playerStats.GlaiveStats.Size = playerStats.GlaiveStats.BaseSize * 1.5f;
                break;
        }
    }
}

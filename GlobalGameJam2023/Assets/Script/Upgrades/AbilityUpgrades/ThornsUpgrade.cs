using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThronsUpgrade : AbilityUpgrade
{
    public ThronsUpgrade(PlayerStats playerStats) : base("Thorns Upgrade", "Upgrades the players thorns ability", 6, playerStats, new List<string>()
    {
        "Unlocks the thorns ability",
        "Increases the damage of your thorns ability",
        "Increases the damage of your thorns ability",
        "Increases the amount of bullets your thorns ability shoots in",
        "Reduces the cooldown of your thorns ability",
        "Increases the damage, reduces the cooldown and increases the amount of bullets of your thorns ability"
    })
    {
    }

    public override void ChooseUpgrade()
    {
        base.ChooseUpgrade();
        playerStats.ThronsStats.UpgradeLevel = UpgradeLevel;

        switch(UpgradeLevel)
        {
            case 2:
                playerStats.ThronsStats.Damage = playerStats.ThronsStats.BaseDamage * 1.25f;
                break;
            case 3:
                playerStats.ThronsStats.Damage = playerStats.ThronsStats.BaseDamage * 1.5f;
                break;
            case 4:
                playerStats.ThronsStats.ShootingDirections = playerStats.ThronsStats.BaseShootingDirections + 1;
                break;
            case 5:
                playerStats.ThronsStats.Cooldown = playerStats.ThronsStats.BaseCooldown - 2.0f;
                break;
            case 6:
                playerStats.ThronsStats.Damage = playerStats.ThronsStats.BaseDamage * 2.0f;
                playerStats.ThronsStats.Cooldown = playerStats.ThronsStats.BaseCooldown - 5.0f;
                playerStats.ThronsStats.ShootingDirections = playerStats.ThronsStats.BaseShootingDirections + 3;
                break;
        }
    }
}

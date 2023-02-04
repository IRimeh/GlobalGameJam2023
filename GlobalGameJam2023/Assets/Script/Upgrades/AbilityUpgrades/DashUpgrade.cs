using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashUpgrade : AbilityUpgrade
{
    public DashUpgrade(PlayerStats playerStats) : base("Dash Upgrade", "Upgrades the players dash ability", 6, playerStats, new List<string>()
    {
        "Unlocks the dash ability",
        "Increases the distance of your dash ability",
        "Decreases the cooldown of your dash ability",
        "Decreases the cooldown of your dash ability",
        "Increases the distance of your dash ability",
        "Gives your dash ability a second charge"
    })
    {
    }

    public override void ChooseUpgrade()
    {
        Debug.Log("is unlocked:" + playerStats.ThronsStats.IsAbilityUnlocked());
        Debug.Log(playerStats.ThronsStats.Damage);
        Debug.Log(playerStats.ThronsStats.Cooldown);
        Debug.Log(playerStats.ThronsStats.ShootingDirections);

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

        Debug.Log("is unlocked:" + playerStats.ThronsStats.IsAbilityUnlocked());
        Debug.Log(playerStats.ThronsStats.Damage);
        Debug.Log(playerStats.ThronsStats.Cooldown);
        Debug.Log(playerStats.ThronsStats.ShootingDirections);
    }
}

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
        Debug.Log("is unlocked:" + playerStats.DashStats.IsAbilityUnlocked());
        Debug.Log(playerStats.DashStats.Distance);
        Debug.Log(playerStats.DashStats.Cooldown);
        Debug.Log(playerStats.DashStats.Charges);

        base.ChooseUpgrade();
        playerStats.DashStats.UpgradeLevel = UpgradeLevel;

        switch(UpgradeLevel)
        {
            case 2:
                playerStats.DashStats.Distance = playerStats.DashStats.BaseDistance * 1.5f;
                break;
            case 3:
                playerStats.DashStats.Cooldown = playerStats.DashStats.BaseCooldown - 2.0f;
                break;
            case 4:
                playerStats.DashStats.Cooldown = playerStats.DashStats.BaseCooldown - 4.0f;
                break;
            case 5:
                playerStats.DashStats.Distance = playerStats.DashStats.BaseDistance * 2.0f;
                break;
            case 6:
                playerStats.DashStats.Charges = 2;
                break;
        }

        Debug.Log("is unlocked:" + playerStats.DashStats.IsAbilityUnlocked());
        Debug.Log(playerStats.DashStats.Distance);
        Debug.Log(playerStats.DashStats.Cooldown);
        Debug.Log(playerStats.DashStats.Charges);
    }
}

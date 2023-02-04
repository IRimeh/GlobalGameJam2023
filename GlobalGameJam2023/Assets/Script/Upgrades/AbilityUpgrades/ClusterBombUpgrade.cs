using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterBombUpgrade : AbilityUpgrade
{
    public ClusterBombUpgrade(PlayerStats playerStats) : base("Cluster Bomb Upgrade", "Upgrades the players cluster bomb ability", 6, playerStats, new List<string>()
    {
        "Unlocks the cluster bomb ability",
        "Increases the size of the cluster bomb",
        "Increases the size of the cluster bomb",
        "Increases the amount of clusters coming from the bomb",
        "Increases the amount of clusters coming from the bomb",
        "Increases the size and damage of the cluster bomb"
    })
    {
    }

    public override void ChooseUpgrade()
    {
        Debug.Log("is unlocked:" + playerStats.ClusterBombStats.IsAbilityUnlocked());
        Debug.Log(playerStats.ClusterBombStats.Damage);
        Debug.Log(playerStats.ClusterBombStats.BombAmount);
        Debug.Log(playerStats.ClusterBombStats.Size);

        base.ChooseUpgrade();
        playerStats.ClusterBombStats.UpgradeLevel = UpgradeLevel;

        switch(UpgradeLevel)
        {
            case 2:
                playerStats.ClusterBombStats.Size = playerStats.ClusterBombStats.BaseSize * 1.25f;
                break;
            case 3:
                playerStats.ClusterBombStats.Size = playerStats.ClusterBombStats.BaseSize * 1.5f;
                break;
            case 4:
                playerStats.ClusterBombStats.BombAmount = playerStats.ClusterBombStats.BaseBombAmount + 1;
                break;
            case 5:
                playerStats.ClusterBombStats.BombAmount = playerStats.ClusterBombStats.BaseBombAmount + 2;
                break;
            case 6:
                playerStats.ClusterBombStats.Damage = playerStats.ClusterBombStats.BaseDamage + 5;
                playerStats.ClusterBombStats.Size = playerStats.ClusterBombStats.BaseSize * 2.0f;
                break;
        }

        Debug.Log("is unlocked:" + playerStats.ClusterBombStats.IsAbilityUnlocked());
        Debug.Log(playerStats.ClusterBombStats.Damage);
        Debug.Log(playerStats.ClusterBombStats.BombAmount);
        Debug.Log(playerStats.ClusterBombStats.Size);
    }
}

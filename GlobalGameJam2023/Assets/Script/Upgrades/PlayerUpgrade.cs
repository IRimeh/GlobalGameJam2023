using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public abstract class PlayerUpgrade : Upgrade
{
    public PlayerUpgrade(string upgradeName, string upgradeDescription, int maxLevel, PlayerStats playerStats) : base(upgradeName, upgradeDescription, maxLevel, playerStats)
    {
    }
}

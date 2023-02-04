using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class AbilityUpgrade : Upgrade
{
    internal List<string> levelDescriptions;
    public List<string> LevelDescriptions { get { return levelDescriptions; } }

    public AbilityUpgrade(string upgradeName, string upgradeDescription, int maxLevel, PlayerStats playerStats, List<string> _levelDescriptions) : base(upgradeName, upgradeDescription, maxLevel, playerStats)
    {
        levelDescriptions = _levelDescriptions;
    }

    public string GetCurrentLevelDescription()
    {
        return levelDescriptions[UpgradeLevel];
    }
}
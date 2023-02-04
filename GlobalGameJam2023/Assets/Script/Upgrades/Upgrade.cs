using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using NaughtyAttributes;

[Serializable]
public class Upgrade 
{
    [SerializeField] private string _upgradeName;
    [SerializeField] private int _upgradeLevel;

    public string UpgradeName {get; internal set; }
    public string UpgradeDescription {get; internal set;}
    public int UpgradeLevel { get; protected set; }
    public int MaxLevel {get; internal set; }

    protected PlayerStats playerStats;

    public Upgrade(string upgradeName, string upgradeDescription, int maxLevel, PlayerStats _playerStats)
    {
        _upgradeName = upgradeName;
        _upgradeLevel = 0;

        UpgradeName = upgradeName;
        UpgradeDescription = upgradeDescription;
        UpgradeLevel = 0;
        MaxLevel = maxLevel;

        playerStats = _playerStats;
    }

    public virtual void ChooseUpgrade()
    {
        if(CanBeUpgraded())
            UpgradeLevel++;
    }

    public bool CanBeUpgraded()
    {
        return UpgradeLevel < MaxLevel;
    }
}

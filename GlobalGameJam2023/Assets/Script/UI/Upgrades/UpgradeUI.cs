using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeUI : MonoBehaviour
{
    public UpgradeOption upgradeOption1;
    public UpgradeOption upgradeOption2;
    public UpgradeOption upgradeOption3;

    public void ShowUpgradeOptions(Upgrade upgrade1, Upgrade upgrade2, Upgrade upgrade3, Action onChoseUpgrade)
    {
        upgradeOption1.Initialize(upgrade1);
        upgradeOption2.Initialize(upgrade2);
        upgradeOption3.Initialize(upgrade3);

        upgradeOption1.OnChoseUpgrade += onChoseUpgrade;
        upgradeOption1.OnChoseUpgrade += HideUpgradeOptions;

        upgradeOption2.OnChoseUpgrade += onChoseUpgrade;
        upgradeOption2.OnChoseUpgrade += HideUpgradeOptions;

        upgradeOption3.OnChoseUpgrade += onChoseUpgrade;
        upgradeOption3.OnChoseUpgrade += HideUpgradeOptions;
    }

    public void HideUpgradeOptions()
    {
        upgradeOption1.gameObject.SetActive(false);
        upgradeOption2.gameObject.SetActive(false);
        upgradeOption3.gameObject.SetActive(false);
    }
}

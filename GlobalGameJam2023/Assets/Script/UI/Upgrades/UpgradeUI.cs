using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    public UpgradeOption upgradeOption1;
    public UpgradeOption upgradeOption2;
    public UpgradeOption upgradeOption3;

    [SerializeField]
    private Image roots;
    [SerializeField]
    private Image blackBar;
    [SerializeField]
    private Image blackFade;

    [SerializeField]
    private Sprite speedIcon, healthIcon, damageIcon, fireRateIcon, projPenetrationIcon, projSpeedIcon, projSizeIcon, projAmountIcon;
    [SerializeField]
    private Sprite dashIcon, thornsIcon, glaiveIcon;

    private void OnEnable()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Sucking", 0, false);
        blackBar.transform.localScale = new Vector3(1, 0, 1);
        blackBar.transform.DOScaleY(1.0f, 0.2f).SetUpdate(true);

        roots.color = new Color(1,1,1, 0);
        roots.DOColor(Color.white, 0.2f).SetUpdate(true);

        blackFade.color = new Color(blackFade.color.r, blackFade.color.g, blackFade.color.b, 0);
        blackFade.DOColor(new Color(blackFade.color.r, blackFade.color.g, blackFade.color.b, 0.75f), 0.1f).SetUpdate(true);
    }

    public void ShowUpgradeOptions(Upgrade upgrade1, Upgrade upgrade2, Upgrade upgrade3, Action onChoseUpgrade)
    {
        gameObject.SetActive(true);

        upgradeOption1.Initialize(upgrade1, GetIconFromUpgrade(upgrade1));
        upgradeOption2.Initialize(upgrade2, GetIconFromUpgrade(upgrade2));
        upgradeOption3.Initialize(upgrade3, GetIconFromUpgrade(upgrade3));

        upgradeOption1.OnChoseUpgrade += onChoseUpgrade;
        upgradeOption1.OnChoseUpgrade += HideUpgradeOptions;

        upgradeOption2.OnChoseUpgrade += onChoseUpgrade;
        upgradeOption2.OnChoseUpgrade += HideUpgradeOptions;

        upgradeOption3.OnChoseUpgrade += onChoseUpgrade;
        upgradeOption3.OnChoseUpgrade += HideUpgradeOptions;
    }

    public void HideUpgradeOptions()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Upgrade");
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Sucking", 1, false);
        upgradeOption1.gameObject.SetActive(false);
        upgradeOption2.gameObject.SetActive(false);
        upgradeOption3.gameObject.SetActive(false);

        gameObject.SetActive(false);
    }

    private Sprite GetIconFromUpgrade(Upgrade upgrade)
    {
        switch(upgrade)
        {
            case DamageUpgrade:
                return damageIcon;
            case HealthUpgrade:
                return healthIcon;
            case FireRateUpgrade:
                return fireRateIcon;
            case MovementSpeedUpgrade:
                return speedIcon;
            case ProjectileAmountUpgrade:
                return projAmountIcon;
            case ProjectilePenetrationUpgrade:
                return projPenetrationIcon;
            case ProjectileSizeUpgrade:
                return projSizeIcon;
            case ProjectileSpeedUpgrade:
                return projSpeedIcon;

            case DashUpgrade:
                return dashIcon;
            case ThronsUpgrade:
                return thornsIcon;
            case GlaiveUpgrade:
                return glaiveIcon;
        }
        return null;
    }
}

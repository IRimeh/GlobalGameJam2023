using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class UpgradeOption : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField]
    private GameObject outlineObj;
    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private TextMeshProUGUI descriptionText;
    
    private Upgrade currentUpgrade = null;

    public Action OnChoseUpgrade;

    public void Initialize(Upgrade upgrade)
    {
        gameObject.SetActive(true);
        currentUpgrade = upgrade;
        nameText.text = upgrade.UpgradeName;

        if(upgrade is AbilityUpgrade)
            descriptionText.text = (upgrade as AbilityUpgrade).GetCurrentLevelDescription();
        else
            descriptionText.text = upgrade.UpgradeDescription;
    }

    private void OnDisable()
    {
        OnChoseUpgrade = null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        outlineObj.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        outlineObj.gameObject.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(currentUpgrade != null)
            currentUpgrade.ChooseUpgrade();

        if(OnChoseUpgrade != null)
            OnChoseUpgrade.Invoke();
        OnChoseUpgrade = null;

        outlineObj.gameObject.SetActive(false);
    }
}

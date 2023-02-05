using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System;
using UnityEngine.UI;
using DG.Tweening;

public class UpgradeOption : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField]
    private Image img;
    [SerializeField]
    private Image Icon;
    [SerializeField]
    private Color notSelectedColor;
    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private TextMeshProUGUI descriptionText;
    [SerializeField]
    private GameObject leafObj;
    
    private Upgrade currentUpgrade = null;

    public Action OnChoseUpgrade;

    public void Initialize(Upgrade upgrade, Sprite icon)
    {
        gameObject.SetActive(true);
        currentUpgrade = upgrade;
        nameText.text = upgrade.UpgradeName;
        Icon.sprite = icon;

        if(upgrade is AbilityUpgrade)
            descriptionText.text = (upgrade as AbilityUpgrade).GetCurrentLevelDescription();
        else
            descriptionText.text = upgrade.UpgradeDescription;

        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, 0.3f).SetUpdate(true);
    }

    private void OnDisable()
    {
        OnChoseUpgrade = null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        leafObj.gameObject.SetActive(true);
        img.color = Color.white;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        leafObj.gameObject.SetActive(false);
        img.color = notSelectedColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(currentUpgrade != null)
            currentUpgrade.ChooseUpgrade();

        if(OnChoseUpgrade != null)
            OnChoseUpgrade.Invoke();
        OnChoseUpgrade = null;

        img.color = notSelectedColor;
    }
}

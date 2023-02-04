using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Plant : MonoBehaviour
{
    [SerializeField]
    private int bloodNeededToWin = 1000;
    [SerializeField]
    private int playerLayer = 8;
    [SerializeField]
    private int upgradeCount = 25;
    [SerializeField]
    private AnimationCurve bloodNeededCurve;


    [SerializeField][ReadOnly]
    private int upgradesUnlocked = 0;
    [SerializeField][ReadOnly]
    private float currentCollectedBlood = 0;
    

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == playerLayer)
        {
            BloodInventory bloodInv = other.GetComponent<BloodInventory>();
            CollectBlood(bloodInv.TakeBlood(), other.gameObject);
        }
    }

    private IEnumerator TakeBlood()
    {
        yield return null;
    }

    private void CollectBlood(float bloodAmount, GameObject playerObj)
    {
        currentCollectedBlood += bloodAmount;
        float ratio = currentCollectedBlood / bloodNeededToWin;

        int upgrades = Mathf.FloorToInt(bloodNeededCurve.Evaluate(ratio) * upgradeCount);
        if(upgradesUnlocked < upgrades)
            StartCoroutine(UnlockUpgrades(upgrades - upgradesUnlocked, playerObj));
    }

    private IEnumerator UnlockUpgrades(int amountOfUpgrades, GameObject playerObj)
    {
        yield return null;
    }
}

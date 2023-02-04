using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Plant : MonoBehaviour
{
    [SerializeField]
    private int bloodNeededToWin = 1000;
    [SerializeField]
    private float bloodNeededForLevel = 10.0f;
    [SerializeField]
    private float bloodNeededMultiplier = 1.1f;
    [SerializeField]
    private int playerLayer = 8;
    [SerializeField]
    private int upgradeCount = 25;
    [SerializeField]
    private UpgradeUI upgradeUI;


    [SerializeField][ReadOnly]
    private int upgradesUnlocked = 0;
    [SerializeField][ReadOnly]
    private float currentCollectedBlood = 0;
    [SerializeField][ReadOnly]
    private float bloodTillNextUpgrade = 0;

    private float totalBloodCollected = 0;


    // Visuals
    [SerializeField]
    private ParticleSystem bloodSuckingParticles;
    [SerializeField]
    private SpriteRenderer bloodFillSprite;

    private bool isChoosingUpgrades = false;
    private MaterialPropertyBlock propBlock;

    private void Awake()
    {
        propBlock = new MaterialPropertyBlock();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == playerLayer)
        {
            BloodInventory bloodInv = other.GetComponent<BloodInventory>();
            if(bloodInv.GetBloodAmount() >= 0.1f)
            {
                bloodSuckingParticles.Play();
                var shape = bloodSuckingParticles.shape;
                shape.position = PlayerController.Position - bloodSuckingParticles.transform.position;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == playerLayer)
        {
            bloodSuckingParticles.Stop();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == playerLayer)
        {
            BloodInventory bloodInv = other.GetComponent<BloodInventory>();
            if(bloodInv.GetBloodAmount() >= 0.1f)
            {
                if(!isChoosingUpgrades)
                    CollectBlood(bloodInv.TakeBlood(0.1f), other.gameObject);

                if(!bloodSuckingParticles.isPlaying)
                    bloodSuckingParticles.Play();

                var shape = bloodSuckingParticles.shape;
                shape.position = (PlayerController.Position + new Vector3(0, 1, 0)) - bloodSuckingParticles.transform.position;
            }
            else
                bloodSuckingParticles.Stop();
        }
    }

    private void CollectBlood(float bloodAmount, GameObject playerObj)
    {
        totalBloodCollected += bloodAmount;
        currentCollectedBlood += bloodAmount;
        SetBloodFillPercentage(currentCollectedBlood / bloodNeededForLevel);

        int upgradesUnlockedAfterCollectingBlood = 0;
        while(currentCollectedBlood > bloodNeededForLevel)
        {
            currentCollectedBlood -= bloodNeededForLevel;
            bloodNeededForLevel *= bloodNeededMultiplier;
            upgradesUnlockedAfterCollectingBlood++;
        }

        StartCoroutine(UnlockUpgrades(upgradesUnlockedAfterCollectingBlood, playerObj));
    }

    private IEnumerator UnlockUpgrades(int amountOfUpgrades, GameObject playerObj)
    {
        isChoosingUpgrades = true;
        GameController.SetTimeScale(0.0f, 0.4f);

        bool hasChosenUpgrade = false;
        for(int i = 0; i < amountOfUpgrades; i++)
        {
            hasChosenUpgrade = false;

            PlayerStats playerStats = playerObj.GetComponent<PlayerStats>();
            
            (Upgrade, Upgrade, Upgrade) upgrades = GetRandomUpgrades(playerStats);
            yield return new WaitForSecondsRealtime(0.3f);
            upgradeUI.ShowUpgradeOptions(upgrades.Item1, upgrades.Item2, upgrades.Item3, ChoseUpgrade);

            yield return new WaitUntil(() => hasChosenUpgrade);
            upgradesUnlocked++;
        }

        void ChoseUpgrade()
        {
            hasChosenUpgrade = true;
        }
        
        GameController.SetTimeScale(1.0f, 0.4f);
        isChoosingUpgrades = false;
    }

    private (Upgrade, Upgrade, Upgrade) GetRandomUpgrades(PlayerStats playerStats)
    {
        List<Upgrade> allUpgrades = playerStats.GetAllUpgrades();
        List<Upgrade> availableUpgrades = new List<Upgrade>(allUpgrades);
        List<Upgrade> chosenUpgrades = new List<Upgrade>();
        while(chosenUpgrades.Count < 3)
        {
            Upgrade upgrade = availableUpgrades[Random.Range(0, availableUpgrades.Count)];

            if(upgrade.CanBeUpgraded())
                chosenUpgrades.Add(upgrade);

            List<Upgrade> newList = new List<Upgrade>();
            foreach(Upgrade upgrd in availableUpgrades)
            {
                if(upgrd != upgrade)
                    newList.Add(upgrd);
            }
            availableUpgrades = newList;
        }

        return (chosenUpgrades[0], chosenUpgrades[1], chosenUpgrades[2]);
    }


    private void SetBloodFillPercentage(float perc)
    {
        bloodFillSprite.GetPropertyBlock(propBlock);
        propBlock.SetFloat("_FillPercentage", perc);
        bloodFillSprite.SetPropertyBlock(propBlock);
    }
}

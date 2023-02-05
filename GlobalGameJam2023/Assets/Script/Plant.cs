using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using DG.Tweening;
using TMPro;

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
    [SerializeField][ReadOnly]
    private float totalBloodCollected = 0;


    // Visuals
    [SerializeField]
    private ParticleSystem bloodSuckingParticles;
    [SerializeField]
    private SpriteRenderer bloodFillSprite;
    [SerializeField]
    private SpriteRenderer upgradePickup;
    [SerializeField]
    private float upgradePickupRotationSpeed = 180.0f;
    private Vector3 defaultPickupPos;

    private bool isChoosingUpgrades = false;
    private MaterialPropertyBlock propBlock;

    // Win condition stuff
    [SerializeField]
    private TextMeshProUGUI bloodToCollectText;
    [SerializeField]
    private Renderer rootsOverlay;

    private BloodInventory playerBloodInv;
    private bool canShowPickup = true;

    private void Awake()
    {
        propBlock = new MaterialPropertyBlock();
        defaultPickupPos = upgradePickup.transform.position;
        bloodToCollectText.text = bloodNeededToWin.ToString();
        bloodToCollectText.transform.DOPunchScale(Vector3.one * 1.5f, 0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == playerLayer)
        {
            BloodInventory bloodInv = other.GetComponent<BloodInventory>();
            playerBloodInv = bloodInv;
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
            float bloodSiphonAmount = Mathf.Max(0.1f, bloodInv.GetBloodAmount() * 0.01f);
            if(bloodInv.GetBloodAmount() >= bloodSiphonAmount)
            {
                if(!isChoosingUpgrades)
                    CollectBlood(bloodInv.TakeBlood(bloodSiphonAmount), other.gameObject);

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
        bloodToCollectText.text = Mathf.RoundToInt(bloodNeededToWin - totalBloodCollected).ToString();
        bloodToCollectText.transform.DOKill();
        bloodToCollectText.transform.localScale = Vector3.one;
        bloodToCollectText.transform.DOShakeScale(0.05f, 0.25f);
        rootsOverlay.sharedMaterial.SetFloat("_ShowPercentage", totalBloodCollected / bloodNeededToWin);

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
            PickupUpgradeVisual();
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

    private void ResetUpgradePickup()
    {
        if(playerBloodInv != null && bloodNeededForLevel < currentCollectedBlood + playerBloodInv.GetBloodAmount())
        {
            upgradePickup.transform.position = defaultPickupPos;
            upgradePickup.transform.DOScale(Vector3.one, 0.3f);
        }
    }

    private void AllowShowingOfPickup()
    {
        canShowPickup = true;
        upgradePickup.transform.localScale = Vector3.zero;
    }

    private void PickupUpgradeVisual()
    {
        canShowPickup = false;
        upgradePickup.transform.DOMove(PlayerController.Position, 0.2f);
        upgradePickup.transform.DOScale(Vector3.zero, 0.2f);

        Invoke("AllowShowingOfPickup", 0.2f);
    }

    private void Update()
    {
        ResetUpgradePickup();
    }
}

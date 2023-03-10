using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using DG.Tweening;
using TMPro;

public class Plant : MonoBehaviour
{
    [SerializeField]
    private GameObject babySprite;
    [SerializeField]
    private GameObject plantSprite;
    [SerializeField]
    private ParticleSystem daBabyParticles;
    [SerializeField]
    private EnemySpawnController enemySpawnController;
    [SerializeField]
    private BabyRootSpawner rootSpawner;
    [SerializeField]
    private int bloodNeededToWin = 1000;
    [SerializeField]
    private float babyPhaseTimeInMinutes = 5.0f;

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
    private List<Renderer> groundOverlays;

    private BloodInventory playerBloodInv;
    private bool canShowPickup = true;
    private bool daBaby = false;
    private float timeSinceInDaBabyPhase = 0.0f;

    private void Awake()
    {
        propBlock = new MaterialPropertyBlock();
        defaultPickupPos = upgradePickup.transform.position;
        bloodToCollectText.text = bloodNeededToWin.ToString();
        bloodToCollectText.transform.DOPunchScale(Vector3.one * 2.0f, 0.5f, 5, 0.3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == playerLayer)
        {
            BloodInventory bloodInv = other.GetComponent<BloodInventory>();
            playerBloodInv = bloodInv;
            if(bloodInv.GetBloodAmount() >= 0.1f)
            {
                FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Sucking", 1, false);
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
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Sucking", 0, false);
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
        if(daBaby)
            return;

        totalBloodCollected += bloodAmount;
        currentCollectedBlood += bloodAmount;
        SetBloodFillPercentage(currentCollectedBlood / bloodNeededForLevel);
        bloodToCollectText.text = Mathf.RoundToInt(bloodNeededToWin - totalBloodCollected).ToString();
        bloodToCollectText.transform.DOKill();
        bloodToCollectText.transform.localScale = Vector3.one;
        bloodToCollectText.transform.DOShakeScale(0.05f, 0.25f);
        foreach(Renderer renderer in groundOverlays)
        {
            renderer.sharedMaterial.SetFloat("_ShowPercentage", (totalBloodCollected / (bloodNeededToWin * 0.75f)));
        }

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
        if(canShowPickup && playerBloodInv != null && bloodNeededForLevel < currentCollectedBlood + playerBloodInv.GetBloodAmount())
        {
            upgradePickup.transform.position = defaultPickupPos;
            upgradePickup.transform.DOScale(Vector3.one, 0.3f);
        }
    }

    private void AllowShowingOfPickup()
    {
        canShowPickup = true;
        upgradePickup.transform.DOKill();
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

        if(totalBloodCollected >= bloodNeededToWin && !daBaby)
        {
            GoIntoBabyMode();
        }

        if(daBaby)
        {
            timeSinceInDaBabyPhase += Time.deltaTime;

            float timeLeft = (babyPhaseTimeInMinutes * 60.0F) - timeSinceInDaBabyPhase;

            string minutes = Mathf.Floor(timeLeft / 60).ToString("00");
            string seconds = (timeLeft % 60).ToString("00");

            bloodToCollectText.text = minutes + ":" + seconds;
        }
    }

    private void GoIntoBabyMode()
    {
        babySprite.gameObject.SetActive(true);
        plantSprite.gameObject.SetActive(false);
        daBaby = true;
        daBabyParticles.Play();
        FMODUnity.RuntimeManager.PlayOneShot("event:/BabyWokeUp-001", transform.position);
        //Start root spawning
        rootSpawner.StartSpawningRoots();

        //Up dificulty?

        StartCoroutine(WinGameSequence());
    }

    private IEnumerator WinGameSequence()
    {
        yield return new WaitForSeconds(60.0f * babyPhaseTimeInMinutes);

        if(!PlayerController.IsDead)
        {
            bloodToCollectText.gameObject.SetActive(false);
            enemySpawnController.StopSpawn();
            List<EnemyScript> enemies = new List<EnemyScript>(GameObject.FindObjectsOfType<EnemyScript>());
            enemies.ForEach(e => e.GetComponent<Health>().TakeDamage(1000.0f));
            CameraShake.Instance.StartShake(0.3f);

            yield return new WaitForSeconds(3.0f);
            PlayerController.IsDead = true;

            playerBloodInv.transform.DOMove(transform.position, 0.2f);
            playerBloodInv.transform.DOScale(Vector3.zero, 0.2f);

            yield return new WaitForSeconds(0.4f);

            HUDCanvas.Instance.FadeOut();
            yield return new WaitForSeconds(0.4f);

            // Show "win" screen
            GameController.Instance.gameOverScreen.SetActive(true);
        }
    }
}

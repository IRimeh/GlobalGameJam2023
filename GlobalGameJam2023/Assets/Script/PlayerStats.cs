using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PlayerStats : MonoBehaviour
{
    // Base stats
    [Header("Base Stats")]
    [SerializeField] private int baseDamage = 10;
    public int BaseDamage { get { return baseDamage; } }
    [SerializeField] private int baseHealth = 100;
    public int BaseHealth { get { return baseHealth; } }
    [SerializeField] private float baseFireRate = 0.3f;
    public float BaseFireRate { get { return baseFireRate; } }
    [SerializeField] private float baseMovementSpeed = 15.0f;
    public float BaseMovementSpeed { get { return baseMovementSpeed; } }
    [SerializeField] private float baseProjectileSpeed = 10.0f;
    public float BaseProjectileSpeed { get { return baseProjectileSpeed; } }
    [SerializeField] private int baseProjectileAmount = 1;
    public int BaseProjectileAmount { get { return baseProjectileAmount; } }
    [SerializeField] private float baseProjectileSize = 1.0f;
    public float BaseProjectileSize { get { return baseProjectileSize; } }
    [SerializeField] private int baseProjectilePenetration = 0;
    public int BaseProjectilePenetration { get { return baseProjectilePenetration; } }

    // Ability stats
    public DashStats DashStats = new DashStats();
    public ThronsStats ThronsStats = new ThronsStats();
    public GlaiveStats GlaiveStats = new GlaiveStats();
    public ClusterBombStats ClusterBombStats = new ClusterBombStats();

    // Player Upgrades
    [Space(10)]
    [SerializeReference][ReadOnly]
    private List<PlayerUpgrade> playerUpgrades = new List<PlayerUpgrade>();

    // Ability Upgrades
    [Space(10)]
    [SerializeReference][ReadOnly]
    private List<AbilityUpgrade> abilityUpgrades = new List<AbilityUpgrade>();
    

    private void Start()
    {
        DashStats.Init();
        ThronsStats.Init();
        GlaiveStats.Init();
        ClusterBombStats.Init();

        playerUpgrades = new List<PlayerUpgrade>()
        {
            new DamageUpgrade(this),
            new HealthUpgrade(this),
            new FireRateUpgrade(this),
            new MovementSpeedUpgrade(this),
            new ProjectileSpeedUpgrade(this),
            new ProjectileAmountUpgrade(this),
            new ProjectilePenetrationUpgrade(this),
            new ProjectileSizeUpgrade(this)
        };

        abilityUpgrades = new List<AbilityUpgrade>()
        {
            new DashUpgrade(this)
        };

        Damage = baseDamage;
        Health = baseHealth;
        FireRate = baseFireRate;
        MovementSpeed = baseMovementSpeed;
        ProjectileSpeed = baseProjectileSpeed;
        ProjectileAmount = baseProjectileAmount;
        ProjectileSize = baseProjectileSize;
        ProjectilePenetration = baseProjectilePenetration;
    }

    // Current stats
    [HideInInspector] public int Damage;
    [HideInInspector] public int Health;
    [HideInInspector] public float FireRate;
    [HideInInspector] public float MovementSpeed;
    [HideInInspector] public float ProjectileSpeed;
    [HideInInspector] public int ProjectileAmount;
    [HideInInspector] public float ProjectileSize;
    [HideInInspector] public int ProjectilePenetration;

    [Button()]
    private void TestButton()
    {
        abilityUpgrades[abilityUpgrades.Count - 1].ChooseUpgrade();
    }

    // // Ability Upgrades
    // [Foldout("Ability Upgrades")][SerializeField][ReadOnly] private int dash;
    // [Foldout("Ability Upgrades")][SerializeField][ReadOnly] private int thorns;
    // [Foldout("Ability Upgrades")][SerializeField][ReadOnly] private int glaive;
    // [Foldout("Ability Upgrades")][SerializeField][ReadOnly] private int clusterBomb;
}

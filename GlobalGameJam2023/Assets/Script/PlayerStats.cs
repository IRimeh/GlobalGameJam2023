using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PlayerStats : MonoBehaviour
{
    // Base stats
    [SerializeField] private int baseDamage = 10;
    [SerializeField] private int baseHealth = 100;
    [SerializeField] private float baseFireRate = 0.3f;
    [SerializeField] private float baseMovementSpeed = 15.0f;
    [SerializeField] private float baseProjectileSpeed = 10.0f;
    [SerializeField] private int baseProjectileAmount = 1;
    [SerializeField] private float baseProjectileSize = 1.0f;
    [SerializeField] private int baseProjectilePenetration = 0;

    // Player Upgrades
    [SerializeField][ReadOnly]
    private List<PlayerUpgrade> playerUpgrades;

    private void Start()
    {
        playerUpgrades = new List<PlayerUpgrade>()
        {
            new DamageUpgrade(this)
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

    // [Foldout("Player Upgrades")][ReadOnly] public Upgrade damage;
    // [Foldout("Player Upgrades")][ReadOnly] public Upgrade health;
    // [Foldout("Player Upgrades")][ReadOnly] public Upgrade fireRate;
    // [Foldout("Player Upgrades")][ReadOnly] public Upgrade movementSpeed;
    // [Foldout("Player Upgrades")][ReadOnly] public Upgrade projectileSpeed;
    // [Foldout("Player Upgrades")][ReadOnly] public Upgrade projectileAmount;
    // [Foldout("Player Upgrades")][ReadOnly] public Upgrade projectileSize;
    // [Foldout("Player Upgrades")][ReadOnly] public Upgrade projectilePenetration;


    // // Ability Upgrades
    // [Foldout("Ability Upgrades")][SerializeField][ReadOnly] private int dash;
    // [Foldout("Ability Upgrades")][SerializeField][ReadOnly] private int thorns;
    // [Foldout("Ability Upgrades")][SerializeField][ReadOnly] private int glaive;
    // [Foldout("Ability Upgrades")][SerializeField][ReadOnly] private int clusterBomb;
}

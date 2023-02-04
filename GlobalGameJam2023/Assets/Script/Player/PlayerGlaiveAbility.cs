using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PlayerGlaiveAbility: MonoBehaviour
{
    [SerializeField]
    private PlayerStats playerStats;

    [SerializeField]
    private GameObject projectilePrefab;

    [SerializeField]
    private float glaiveCooldown = 12.0f;

    private Transform projectileParentTransform;
    private float timeSinceShot = float.MaxValue;

    private void Start()
    {
        GameObject projectilesParent = GameObject.Find("-- Projectiles --");
        if(projectilesParent != null)
            projectileParentTransform = projectilesParent.transform;
        else
            Debug.LogError("Cannot find \"-- Projectiles --\" object");
    }

    // Update is called once per frame
    private void Update()
    {
        if(!playerStats.GlaiveStats.IsAbilityUnlocked())
            return;

        if(timeSinceShot < glaiveCooldown)
        {
            timeSinceShot += Time.deltaTime;
            return;
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            timeSinceShot = 0;
            Shoot();
        }
    }

    private void Shoot()
    {
        Vector3 direction = PlayerController.AimDirection;
        Vector3 offset = PlayerController.AimDirection + new Vector3(0, 1, 0);
        GameObject projectileObj = Instantiate(projectilePrefab, transform.position + offset, Quaternion.LookRotation(direction, Vector3.up), projectileParentTransform);
        Projectile projectile = projectileObj.GetComponent<Projectile>();
        projectile.InitProjectile(playerStats.GlaiveStats.Damage + playerStats.Damage, playerStats.GlaiveStats.Speed + playerStats.ProjectileSpeed, direction, 10000, playerStats.GlaiveStats.Size * playerStats.ProjectileSize);
    }
}

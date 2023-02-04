using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PlayerThornsAbility: MonoBehaviour
{
    [SerializeField]
    private PlayerStats playerStats;

    [SerializeField]
    private GameObject projectilePrefab;

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
        if(!playerStats.ThronsStats.IsAbilityUnlocked())
            return;

        if(timeSinceShot < playerStats.ThronsStats.Cooldown)
        {
            timeSinceShot += Time.deltaTime;
            return;
        }

        if(Input.GetMouseButton(1))
        {
            timeSinceShot = 0;
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        float totalAngle = 360.0f;
        float halfTotalAngle = totalAngle * 0.5f;
        float angleInBetweenProjectiles = totalAngle / playerStats.ThronsStats.ShootingDirections;

        for (int j = 0; j < playerStats.ProjectileAmount; j++)
        {
            for (int i = 0; i < playerStats.ThronsStats.ShootingDirections; i++)
            {
                float angleOffset = angleInBetweenProjectiles * i;
                float inBetweenOffset = (angleInBetweenProjectiles / playerStats.ProjectileAmount) * j;
                Vector3 direction = Quaternion.Euler(0, -halfTotalAngle + angleOffset + inBetweenOffset, 0) * PlayerController.AimDirection;

                Vector3 offset = PlayerController.AimDirection + new Vector3(0, 1, 0);
                GameObject projectileObj = Instantiate(projectilePrefab, transform.position + offset, Quaternion.LookRotation(direction, Vector3.up), projectileParentTransform);
                Projectile projectile = projectileObj.GetComponent<Projectile>();
                projectile.InitProjectile(playerStats.ThronsStats.Damage + playerStats.Damage, playerStats.ProjectileSpeed, direction, playerStats.ProjectilePenetration, playerStats.ProjectileSize);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}

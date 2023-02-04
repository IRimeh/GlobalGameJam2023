using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]
    private PlayerStats playerStats;
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private float angleInBetweenProjectiles = 10.0f;

    private float timeSinceShot = 0;
    private Transform projectileParentTransform;

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
        if(timeSinceShot < (1.0f / playerStats.FireRate))
        {
            timeSinceShot += Time.deltaTime;
            return;
        }

        if(Input.GetMouseButton(0))
        {
            timeSinceShot = 0;
            Shoot();
        }
    }

    private void Shoot()
    {
        float totalAngle = angleInBetweenProjectiles * (playerStats.ProjectileAmount - 1);
        float halfTotalAngle = totalAngle * 0.5f;
        for (int i = 0; i < playerStats.ProjectileAmount; i++)
        {
            float angleOffset = angleInBetweenProjectiles * i;
            Vector3 direction = Quaternion.Euler(0, -halfTotalAngle + angleOffset, 0) * PlayerController.AimDirection;

            GameObject projectileObj = Instantiate(projectilePrefab, transform.position + PlayerController.AimDirection, Quaternion.LookRotation(direction, Vector3.up), projectileParentTransform);
            Projectile projectile = projectileObj.GetComponent<Projectile>();
            projectile.InitProjectile(playerStats.Damage, playerStats.ProjectileSpeed, direction, playerStats.ProjectilePenetration, playerStats.ProjectileSize);
        }
    }
}

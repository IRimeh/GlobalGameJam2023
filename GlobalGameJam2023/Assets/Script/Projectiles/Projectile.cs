using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private int collisionLayer = 7;

    private float projectileDamage;
    private float projectileSpeed;
    private Vector3 projectileDirection;
    private int projectilePenetration;
    private float projectileSize;

    private int penetrationsLeft = 0;
    private Coroutine lifeTimeCoroutine = null;

    public void InitProjectile(float damage, float speed, Vector3 direction, int penetration, float size, float lifeTime = 10.0f)
    {
        projectileDamage = damage;
        projectileSpeed = speed;
        projectileDirection = direction;
        projectilePenetration = penetration;
        projectileSize = size;
        penetrationsLeft = penetration;

        rb.velocity = direction * speed;
        transform.localScale = Vector3.one * size;

        lifeTimeCoroutine = StartCoroutine(LifeTimeCoroutine(lifeTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == collisionLayer && other.TryGetComponent<Health>(out Health health))
        {
            DealDamage(health);

            if(penetrationsLeft < 1)
                DestroyProjectile();
            else
                penetrationsLeft--;
        }
    }

    protected virtual void DealDamage(Health health)
    {
        // Deal damage to enemy
        health.TakeDamage(projectileDamage);
    }

    private IEnumerator LifeTimeCoroutine(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        DestroyProjectile();
    }

    protected virtual void DestroyProjectile()
    {
        StopCoroutine(lifeTimeCoroutine);
        Destroy(this.gameObject);
    }
}

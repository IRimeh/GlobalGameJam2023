using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    protected Rigidbody rb;
    [SerializeField]
    protected int collisionLayer = 7;

    protected float projectileDamage;
    protected float projectileSpeed;
    protected Vector3 projectileDirection;
    protected int projectilePenetration;
    protected float projectileSize;

    protected int penetrationsLeft = 0;
    protected Coroutine lifeTimeCoroutine = null;

    public virtual void InitProjectile(float damage, float speed, Vector3 direction, int penetration, float size, float lifeTime = 10.0f)
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

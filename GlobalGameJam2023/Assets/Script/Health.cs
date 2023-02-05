using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float MaxHealth = 20;
    public GameObject DeathParticlesPrefab;
    public bool ShowDamageNumber = false;
    public bool SpawnDeathParticles = true;

    public UnityEvent<float, float, float, float> OnHealthUpdated;
    public UnityEvent<float, float> OnTakeDamage;
    public UnityEvent OnDeath;

    private float currentHealth = 0;
    private SpriteRenderer spriteRenderer;
    private MaterialPropertyBlock propBlock;


    private void Start()
    {
        currentHealth = MaxHealth;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        propBlock = new MaterialPropertyBlock();
    }

    public void Init(float health)
    {
        MaxHealth = health;
        currentHealth = health;
        this.enabled = true;
    }

    public void AddHealth(int oldMaxHealth = 0, int newMaxHealth = 0)
    {
        int diff = newMaxHealth - oldMaxHealth;
        OnHealthUpdated.Invoke(currentHealth, MaxHealth, currentHealth + diff, MaxHealth + diff);

        MaxHealth += diff;
        currentHealth += diff;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
            Die();
        else
            OnTakeDamage.Invoke(damage, currentHealth);

        // Damage number
        DamageNumbersController.Instance.SpawnDamageNumber(transform.position, (int)damage);

        // Flash red
        if(spriteRenderer != null)
        {
            spriteRenderer.GetPropertyBlock(propBlock);
            propBlock.SetFloat("_TakeDamageTime", Time.time);
            spriteRenderer.SetPropertyBlock(propBlock);
        }
    }

    private void Die()
    {
        OnDeath.Invoke();

        if(SpawnDeathParticles)
            Instantiate(DeathParticlesPrefab, transform.position, Quaternion.identity);

        this.enabled = false;
    }
}

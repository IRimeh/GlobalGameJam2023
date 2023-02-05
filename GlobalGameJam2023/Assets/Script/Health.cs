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
    public float DamageTakeCooldown = 0;
    public bool IsPlayer = false;

    public UnityEvent<float, float, float, float> OnHealthUpdated;
    public UnityEvent<float, float> OnTakeDamage;
    public UnityEvent OnDeath;

    private float currentHealth = 0;
    private SpriteRenderer spriteRenderer;
    private MaterialPropertyBlock propBlock;

    private float timeSinceLastTakenDamage = 0;
    private bool CanTakeDamage
    {
        get { return timeSinceLastTakenDamage >= DamageTakeCooldown; }
    }


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
        if(IsPlayer)
            HealthBarUI.Instance.AddHealth(currentHealth, MaxHealth, currentHealth + diff, MaxHealth + diff);

        MaxHealth += diff;
        currentHealth += diff;
    }

    public void TakeDamage(float damage)
    {

        

        if(!CanTakeDamage)
            return;

        timeSinceLastTakenDamage = 0;


        currentHealth -= damage;
        if(currentHealth <= 0)
            Die();
        else
            OnTakeDamage.Invoke(damage, currentHealth);

        if(IsPlayer)
        {
            HealthBarUI.Instance.TakeDamage(damage, currentHealth);
            CameraShake.Instance.StartShake();
            FMODUnity.RuntimeManager.PlayOneShot("event:/PlayerHit", transform.position);
        }
        else
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/EnemyHit", transform.position);
        }

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

    private void Update()
    {
        if(timeSinceLastTakenDamage < DamageTakeCooldown)
            timeSinceLastTakenDamage += Time.deltaTime;
    }

    private void Die()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/EnemyDie", transform.position);
        OnDeath.Invoke();

        if(SpawnDeathParticles)
            Instantiate(DeathParticlesPrefab, transform.position, Quaternion.identity);

        this.enabled = false;
    }
}

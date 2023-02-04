using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float MaxHealth = 20;
    public UnityEvent<float, float> OnTakeDamage;
    public UnityEvent OnDeath;

    private float currentHealth = 0;


    private void Start()
    {
        currentHealth = MaxHealth;
    }

    public void Init(float health)
    {
        MaxHealth = health;
        currentHealth = health;
        this.enabled = true;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
            Die();
        else
            OnTakeDamage.Invoke(damage, currentHealth);
    }

    private void Die()
    {
        OnDeath.Invoke();
        this.enabled = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using NaughtyAttributes;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField]
    private Image healthBar;
    
    private Health playerHealth;

    private float currentPerc = 1.0f;
    private float maxHealth = 100;

    public static HealthBarUI Instance;

    private void Start()
    {
        currentPerc = 1.0f;
        Instance = this;
    }

    public void TakeDamage(float damageTaken, float currentHealth)
    {
        float ratio = currentHealth / maxHealth;
        healthBar.transform.DOScaleX(ratio, 0.2f);
        currentPerc = ratio;
    }

    public void AddHealth(float oldCurrentHealth, float oldMaxHealth, float newCurrentHealth, float newMaxHealth)
    {
        float ratio = newCurrentHealth / newMaxHealth;
        maxHealth = newMaxHealth;
        healthBar.transform.DOScaleX(ratio, 0.2f);
        currentPerc = ratio;
    }

    
    // [Button]
    // public void TestTakeDamage()
    // {
    //     TakeDamage(5.0f, maxHealth * currentPerc - 5.0f);
    // }

    // [Button]
    // public void TestAddHealth()
    // {
    //     float currentHealth = maxHealth * currentPerc;
    //     AddHealth(currentHealth, maxHealth, currentHealth + 20.0f, maxHealth + 20.0f);
    // }
}

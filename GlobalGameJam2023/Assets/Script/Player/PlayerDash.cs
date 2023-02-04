using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PlayerDash : MonoBehaviour
{
    [SerializeField]
    private PlayerStats playerStats;

    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private float dashForce = 250.0f;

    [SerializeField]
    [ReadOnly]
    private float dashTimer = 0;
    [SerializeField]
    [ReadOnly]
    private int currentCharges = 1;

    void Start()
    {
        dashTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerStats.DashStats.IsAbilityUnlocked())
            return;

        if(currentCharges < playerStats.DashStats.Charges)
        {
            if(dashTimer < playerStats.DashStats.Cooldown)
                dashTimer += Time.deltaTime;

            if(dashTimer >= playerStats.DashStats.Cooldown)
            {
                currentCharges++;
                dashTimer = 0;
            }
        }

        if(currentCharges > 0 && Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 dir = PlayerController.AimDirection;
            rb.AddForce(dir * playerStats.DashStats.Distance * dashForce);
            currentCharges--;
        }
    }
}

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

    [SerializeField]
    private ParticleSystem dashParticles;

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
            dashParticles.Play();
            Vector3 dir = PlayerController.AimDirection;
            rb.AddForce(dir * playerStats.DashStats.Distance * dashForce);
            currentCharges--;
        }

        UpdateIcon();
    }


    private void UpdateIcon()
    {
        HUDCanvas.Instance.DashIcon.gameObject.SetActive(playerStats.DashStats.IsAbilityUnlocked());
        HUDCanvas.Instance.DashChargesText.gameObject.SetActive(playerStats.DashStats.UpgradeLevel > 5);
        HUDCanvas.Instance.DashKeyText.gameObject.SetActive(currentCharges > 0);
        HUDCanvas.Instance.DashChargesText.text = currentCharges.ToString();

        int chargeThreshold = playerStats.DashStats.UpgradeLevel > 5 ? 2 : 1;
        if(currentCharges < chargeThreshold)
        {
            HUDCanvas.Instance.DashOverlay.gameObject.SetActive(true);
            float ratio = dashTimer / playerStats.DashStats.Cooldown;
            Vector3 scale = HUDCanvas.Instance.DashOverlay.transform.localScale;
            HUDCanvas.Instance.DashOverlay.transform.localScale = new Vector3(scale.x, 1.0f - ratio, scale.y);
        }
        else
            HUDCanvas.Instance.DashOverlay.gameObject.SetActive(false);
    }
}

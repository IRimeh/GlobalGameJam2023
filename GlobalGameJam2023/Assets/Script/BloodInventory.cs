using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodInventory : MonoBehaviour
{
    [SerializeField]
    private float BloodAmount = 0;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            AddBlood(100);
        }
    }

    public void AddBlood(float amount)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/BloodPickup", transform.position);
        BloodAmount += amount;
    }

    public float TakeBlood()
    {
        float amount = BloodAmount;
        BloodAmount = 0;
        return amount;
    }

    public float TakeBlood(float amount)
    {
        if(BloodAmount > amount)
        {
            BloodAmount -= amount;
            return amount;
        }
        else
            return TakeBlood();
    }

    public float GetBloodAmount()
    {
        return BloodAmount;
    }
}

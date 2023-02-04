using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodPickup : MonoBehaviour
{
    [SerializeField]
    private int playerLayer;

    [SerializeField]
    private float timeToPickup = 0.2f;

    private bool isBeingPickedUp = false;

    private void OnTriggerEnter(Collider other)
    {
        if(isBeingPickedUp)
            return;

        if(other.gameObject.layer == playerLayer)
            StartCoroutine(PickupSequence(other.gameObject));
    }

    private IEnumerator PickupSequence(GameObject playerObj)
    {
        isBeingPickedUp = true;
        float timeElapsed = 0.0f;
        while(timeElapsed < timeToPickup)
        {
            timeElapsed += Time.deltaTime;
            float ratio = timeElapsed / timeToPickup;

            transform.position = Vector3.Lerp(transform.position, playerObj.transform.position + new Vector3(0, 1, 0), ratio);
            transform.localScale = Vector3.one * (1.0f - ratio);
            yield return null;
        }

        Pickup(playerObj);
    }

    private void Pickup(GameObject playerObj)
    {
        BloodInventory inv = playerObj.GetComponent<BloodInventory>();
        inv.AddBlood(1.0f);
        Destroy(this.gameObject);
    }
}

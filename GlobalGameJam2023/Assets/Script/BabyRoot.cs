using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BabyRoot : MonoBehaviour
{
    [SerializeField]
    private int playerLayer = 8;
    [SerializeField]
    private float damage = 20.0f;
    [SerializeField]
    private List<Collider> colliders;


    private void OnEnable()
    {
        StartCoroutine(IOnEnable());
    }

    private IEnumerator IOnEnable()
    {
        transform.localScale = new Vector3(2, 0, 2);
        transform.DOScaleY(2.0f, 0.5f);

        yield return new WaitForSeconds(0.5f);

        EnableAllColliders(true);
    }

    private void EnableAllColliders(bool enable = true)
    {
        foreach (Collider col in colliders)
        {
            col.gameObject.SetActive(enable);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == playerLayer)
        {
            Health health = other.GetComponent<Health>();
            health.TakeDamage(damage);
            StartCoroutine(KillRoot());
        }
    }

    private IEnumerator KillRoot()
    {
        EnableAllColliders(false);
        transform.DOScaleY(0.0f, 0.5f);

        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}

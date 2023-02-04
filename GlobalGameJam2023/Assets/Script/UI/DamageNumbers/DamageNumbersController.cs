using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumbersController : MonoBehaviour
{
    [SerializeField]
    private GameObject DamageNumberPrefab;

    public static DamageNumbersController Instance = null;

    private void Start()
    {
        Instance = this;
        StartCoroutine(SpawnFirstDamageNumber());
    }

    private IEnumerator SpawnFirstDamageNumber()
    {
        yield return new WaitForFixedUpdate();
        SpawnDamageNumber(new Vector3(0, 1000000, 0), 0);
    }

    public void SpawnDamageNumber(Vector3 damagePosition, int damage)
    {
        GameObject dmgNumObj = Instantiate(DamageNumberPrefab, Vector3.zero, Quaternion.identity, transform);
        DamageNumber dmgNumber = dmgNumObj.GetComponent<DamageNumber>();
        dmgNumber.InitDamageNumber(damagePosition, damage);
    }
}

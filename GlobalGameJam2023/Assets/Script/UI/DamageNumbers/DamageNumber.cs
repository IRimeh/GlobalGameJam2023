using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageNumber : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI damageNumber;
    [SerializeField]
    private float damageNumberLifeTimeMin = 0.65f;
    [SerializeField]
    private float damageNumberLifeTimeMax = 0.85f;
    [SerializeField]
    private Gradient colorGradient;
    [SerializeField]
    private AnimationCurve fontScale;
    [SerializeField]
    private float baseFontScale = 12.0f;
    [SerializeField]
    private Vector3 offset = new Vector3(0, 1.0f, 0);

    private Vector3 position;
    private float currentLifeTime = 0.0f;
    private float lifeTime = 0.0f;

    public void InitDamageNumber(Vector3 pos, int damage)
    {
        position = pos + offset;
        transform.position = Camera.main.WorldToScreenPoint(position);
        damageNumber.text = damage.ToString();
        currentLifeTime = damageNumberLifeTimeMin;
        lifeTime = Random.Range(damageNumberLifeTimeMin, damageNumberLifeTimeMax);

        StartCoroutine(DamageNumberUpdate());
    }

    private IEnumerator DamageNumberUpdate()
    {
        while(currentLifeTime > 0)
        {
            float ratio = 1.0f - (currentLifeTime / lifeTime);
            currentLifeTime -= Time.deltaTime;
            damageNumber.color = colorGradient.Evaluate(ratio);
            damageNumber.fontSize = baseFontScale * fontScale.Evaluate(ratio);
            transform.position = Camera.main.WorldToScreenPoint(position);
            
            yield return null;
        }

        Destroy(this.gameObject);
    }
}

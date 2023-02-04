using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GlaiveProjectile : Projectile
{
    [SerializeField]
    private GameObject spriteObj;
    [SerializeField]
    private float spinningSpeed;
    [SerializeField]
    private float comeBackAfterSeconds = 2.0f;
    [SerializeField]
    private float turnSpeed = 4.0f;

    bool isComingBack = false;

    public override void InitProjectile(float damage, float speed, Vector3 direction, int penetration, float size, float lifeTime = 10)
    {
        base.InitProjectile(damage, speed, direction, penetration, size, lifeTime);

        Invoke("StartComingBack", comeBackAfterSeconds);
    }

    private void Update()
    {
        // Spin
        spriteObj.transform.rotation = Quaternion.Euler(spriteObj.transform.rotation.eulerAngles.x, 
                                                        spriteObj.transform.rotation.eulerAngles.y + Time.deltaTime * spinningSpeed,
                                                        spriteObj.transform.rotation.eulerAngles.z);

        if(isComingBack)
            ComeBack();
    }

    private void ComeBack()
    {
        Vector3 dir = (PlayerController.Position - transform.position);
        dir = (new Vector3(dir.x, 0, dir.y)).normalized;
        rb.velocity = Vector3.Lerp(rb.velocity, dir * projectileSpeed, Time.deltaTime * turnSpeed);

        if(Vector3.Distance(PlayerController.Position, transform.position) < 3.0f)
        {
            DestroyProjectile();
        }
    }

    private void StartComingBack()
    {
        isComingBack = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    private Health health;
    [SerializeField]
    private GameObject pickupPrefab;
    [SerializeField]
    private int playerLayer = 8;
    public EnemyInfo enemyInfo;
    public SpriteRenderer spriteRenderer;
    public NavMeshAgent agent;
    
    private MaterialPropertyBlock propBlock;
    private float offset = 0;
    private float animationSpeed = 0.0f;
    private bool isMoving = false;
    
    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        agent = GetComponent<NavMeshAgent>();
        propBlock = new MaterialPropertyBlock();
        offset = Random.Range(0.0f, 1.0f);
        animationSpeed = spriteRenderer.material.GetFloat("_AnimationSpeed");
    }

    public void SetEnemyInfo(EnemyInfo info)
    {
        gameObject.SetActive(true);
        enemyInfo = info;
        enemyInfo.isActive = true;
        if (enemyInfo.sprite) { SetSprite(enemyInfo.sprite); } else { Debug.Log("HEY WTF"); }
        SetTarget(enemyInfo.target);
        health.Init(info.health);
    }

    public void SetInactive()
    {
        //if Die
        gameObject.SetActive(false);
        enemyInfo.isActive = false;
        StopAllCoroutines();
    }

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
        spriteRenderer.GetPropertyBlock(propBlock);
        propBlock.SetFloat("_TimeOffset", offset);
        spriteRenderer.SetPropertyBlock(propBlock);
    }

    public void SetTarget(TargetEnum nTarget)
    {
        enemyInfo.target = nTarget;
        if(enemyInfo.target == TargetEnum.FollowPlayer)
        {
            StartCoroutine(nameof(DefaultAIBehaviour));
        }
        else
        {
            StartCoroutine(nameof(PlantTargetingAIBehaviour));
        }
    }

    private void FlipSpriteBasedOnPlayerPos()
    {
        bool flip = PlayerController.Position.x < transform.position.x;
        spriteRenderer.flipX = flip;
    }

    private IEnumerator DefaultAIBehaviour()
    {
        while (true)
        {
            if(PlayerController.IsDead)
            {
                agent.velocity = Vector3.zero;
                yield break;
            }

            isMoving = (Time.time * animationSpeed + offset) % 2.0f >= 1.0f;
            if(isMoving)
                agent.velocity = (PlayerController.Position - transform.position).normalized * agent.speed;

            FlipSpriteBasedOnPlayerPos();

            yield return null;//yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator PlantTargetingAIBehaviour()
    {
        while (true)
        {
            FlipSpriteBasedOnPlayerPos();

            //agent.destination = PlantPos;
            //when reach plant run away
            //???
            //profit

            yield return new WaitForSeconds(0.1f);
        }
    }

    public void DropPickup()
    {
        if(enemyInfo.health > 50.0f)
        {
            Instantiate(pickupPrefab, transform.position + new Vector3(-1, 0, -0.5f), Quaternion.identity);
            Instantiate(pickupPrefab, transform.position + new Vector3(1, 0, -0.5f), Quaternion.identity);
            Instantiate(pickupPrefab, transform.position + new Vector3(0, 0, 1.0f), Quaternion.identity);
        }
        else
            Instantiate(pickupPrefab, transform.position, Quaternion.identity);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(!isMoving)
            return;

        foreach (ContactPoint contact in collision.contacts)
        {
            if(contact.otherCollider.gameObject.layer == playerLayer)
            {
                DealDamage(contact.otherCollider.gameObject);
            }
        }
    }

    public void OnCollisionStay(Collision collision)
    {
        if(!isMoving)
            return;

        foreach (ContactPoint contact in collision.contacts)
        {
            if(contact.otherCollider.gameObject.layer == playerLayer)
            {
                DealDamage(contact.otherCollider.gameObject);
            }
        }
    }

    private void DealDamage(GameObject player)
    {
        player.GetComponent<Health>().TakeDamage(20.0f);
    }
}

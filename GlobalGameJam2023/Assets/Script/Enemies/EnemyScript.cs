using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class EnemyScript : MonoBehaviour
{
    public EnemyInfo enemyInfo;
    public SpriteRenderer spriteRenderer;
    public NavMeshAgent agent;
    
    
    
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        agent = GetComponent<NavMeshAgent>();
    }

    public void SetEnemyInfo(EnemyInfo info)
    {
        gameObject.SetActive(true);
        enemyInfo = info;
        enemyInfo.isActive = true;
        if (enemyInfo.sprite) { SetSprite(enemyInfo.sprite); } else { Debug.Log("HEY WTF"); }
        SetTarget(enemyInfo.target);
    }

    public void SetInactive()
    {//if Die
        gameObject.SetActive(false);
        enemyInfo.isActive = false;
        StopAllCoroutines();
    }

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
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

    private IEnumerator DefaultAIBehaviour()
    {
        while (true)
        {
            agent.velocity = (PlayerController.Position - transform.position).normalized * agent.speed;

            yield return null;//yield return new WaitForSeconds(0.1f);
        }
    }
    private IEnumerator PlantTargetingAIBehaviour()
    {
        while (true)
        {

            //agent.destination = PlantPos;
            //when reach plant run away
            //???
            //profit

            yield return new WaitForSeconds(0.1f);
        }
    }
}

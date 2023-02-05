using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EnemyInfo
{
    public bool isActive;
    public int health;
    public float speed;
    public Sprite sprite;
    Vector3 colliderSize;
    public TargetEnum target;
}

public enum TargetEnum { FollowPlayer, TargetPlant }

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    EnemyInfo currentSpawningEnemy;

    [SerializeField]
    private float maxSpawnOffset = 5.0f;

    public float offset = 0f;

    public static float spawnDelay = 2f;

    public void StartSpawn()
    {
        StartCoroutine(nameof(SpawnLoop));
    }

    public void StopSpawn()
    {
        StopAllCoroutines();
    }

    public void SetEnemyType(EnemyInfo info)
    {
        currentSpawningEnemy = info;
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            Spawn();
            yield return new WaitForSeconds(offset);
            yield return new WaitForSeconds(EnemySpawnController.SpawnRate);
        }
    }

    private void Spawn()
    {
        if ((PlayerController.Position - transform.position).magnitude < 30) return;
        EnemyScript enemy = EnemySpawnController.GetEnemyDummy();

        Vector2 randomOffset = (Random.insideUnitCircle * maxSpawnOffset);
        enemy.transform.position = this.transform.position + new Vector3(randomOffset.x, 0, randomOffset.y);
        enemy.SetEnemyInfo(currentSpawningEnemy);
    }
}

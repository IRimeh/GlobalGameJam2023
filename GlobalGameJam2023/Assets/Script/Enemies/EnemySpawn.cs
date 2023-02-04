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
    public float offset = 0f;

    public static float spawnDelay = 2f;

    public void StartSpawn()
    {
        StartCoroutine(nameof(SpawnLoop));
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            Spawn();
            yield return new WaitForSeconds(offset);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void Spawn()
    {
        EnemyScript enemy = EnemySpawnController.GetEnemyDummy();

        enemy.transform.position = this.transform.position;
        enemy.SetEnemyInfo(currentSpawningEnemy);
    }
}

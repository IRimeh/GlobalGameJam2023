using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    static EnemySpawnController I;

    [SerializeField]
    Transform DummyHolder;
    [SerializeField]
    EnemySpawn[] enemySpawners;
    GameObject dummyObject;

    List<EnemyScript> enemies = new List<EnemyScript>();

    private void Awake()
    {
        I = this;
        dummyObject = Resources.Load<GameObject>("EnemyDummy");
    }

    public void StartGame()
    {
        Debug.Log(1);
        for (int i = 0; i < 500; i++) 
        {
            EnemyScript enemy = Instantiate(dummyObject, DummyHolder).GetComponent<EnemyScript>();
            enemy.SetInactive();
            enemies.Add(enemy);
        }
        for (int i = 0; i < enemySpawners.Length; i++)
        {
            enemySpawners[i].StartSpawn();
        }
    }

    public static EnemyScript GetEnemyDummy()
    {
        return I.GetEnemyDummyInternal();
    }

    EnemyScript GetEnemyDummyInternal()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (!enemies[i].enemyInfo.isActive) return enemies[i];
        }
        EnemyScript enemy = Instantiate(dummyObject, DummyHolder).GetComponent<EnemyScript>();
        enemies.Add(enemy);
        Debug.LogWarning("Running out of Dummies");
        return enemy;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    static EnemySpawnController I;

    [SerializeField]
    AnimationCurve SpawnIntervalCurve;
    static public float SpawnRate { get { return I.SpawnIntervalCurve.Evaluate(I.upgradeAmount); } }
    static public void Upgrade() { I.upgradeAmount++; }
    [SerializeField]
    float upgradeAmount = 0;

    [SerializeField]
    Transform DummyHolder;
    [SerializeField]
    EnemySpawn[] enemySpawners;
    GameObject dummyObject;

    List<EnemyScript> enemies = new List<EnemyScript>();

    [SerializeField]
    EnemyInfo WaveInfo1, WaveInfo2;

    private void Awake()
    {
        I = this;
        dummyObject = Resources.Load<GameObject>("EnemyDummy");
        StartCoroutine(nameof(Wave2));
        StartCoroutine(nameof(Wave3));
        StartCoroutine(nameof(Wave4));
    }

    IEnumerator Wave2()
    {
        yield return new WaitUntil(() => upgradeAmount >= 20);
        for (int i = 0; i < enemySpawners.Length; i++)
        {
            if (i % 2 == 0) 
            { 
                SetEnemyType(i, WaveInfo2);
            }
            else
            {
                SetEnemyType(i, WaveInfo1);
            }

        }
    }
    IEnumerator Wave3()
    {
        yield return new WaitUntil(() => upgradeAmount >= 30);
        for (int i = 0; i < enemySpawners.Length; i++)
        {
            SetEnemyType(i, WaveInfo1);
        }
    }
    IEnumerator Wave4()
    {
        yield return new WaitUntil(() => upgradeAmount >= 50);
        for (int i = 0; i < enemySpawners.Length; i++)
        {
            SetEnemyType(i, WaveInfo2);
        }
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

    void SetEnemyType(int spawnerID, EnemyInfo info)
    {
            enemySpawners[spawnerID].SetEnemyType(info);
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
        //EnemyScript enemy = Instantiate(dummyObject, DummyHolder).GetComponent<EnemyScript>();
        //enemies.Add(enemy);
        Debug.LogWarning("Running out of Dummies");
        return enemies[enemies.Count];
    }
}

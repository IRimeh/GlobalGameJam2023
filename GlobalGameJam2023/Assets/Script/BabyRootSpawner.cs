using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyRootSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform rootParent;

    [SerializeField]
    private float timeToSpawnRootsIn = 60.0f * 4.0f;

    private bool canSpawnRoots = false;
    private int rootsSpawned = 0;
    private float timeElapsed = 0.0f;
    private float rootSpawnTimeDelta;

    private int totalRootCount;

    private void Start()
    {
        foreach (Transform child in rootParent)
        {
            totalRootCount++;
        }
        rootSpawnTimeDelta = timeToSpawnRootsIn / totalRootCount;
    }

    private void Update()
    {
        if(canSpawnRoots)
        {
            timeElapsed += Time.deltaTime;
            
            if(Mathf.Floor(timeElapsed / rootSpawnTimeDelta) > rootsSpawned && rootsSpawned < totalRootCount)
            {
                SpawnRoot();
            }
        }
    }

    public void StartSpawningRoots()
    {
        canSpawnRoots = true;
        timeElapsed = 0.0f;
    }

    private void SpawnRoot()
    {
        rootsSpawned++;

        int currentRootCount = 0;
        List<Transform> possibleRoots = new List<Transform>();
        foreach (Transform child in rootParent)
        {
            currentRootCount++;
            possibleRoots.Add(child);
        }

        bool hasChosenRoot = false;
        Transform root = null;
        while(!hasChosenRoot)
        {
            int randIndex = UnityEngine.Random.Range(0, rootParent.childCount);
            Transform child = rootParent.GetChild(randIndex);

            if(!child.gameObject.activeSelf)
            {
                hasChosenRoot = true;
                root = child;
            }
            else
            {
                possibleRoots.Remove(child);
                currentRootCount--;
            }
        }

        if(root)
        {
            root.gameObject.SetActive(true);
        }
    }
}

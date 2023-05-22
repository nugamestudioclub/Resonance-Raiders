using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    [SerializeField]
    private float timeBetweenSpawn = 1.0f;
    private float waitSpawnTime = 0;

    Stack<GameObject> spawnStack = new Stack<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        waitSpawnTime -= Time.deltaTime;
        while (spawnStack.Count > 0 && waitSpawnTime <= 0)
        {
            Debug.Log("Spawning Enemy");
            waitSpawnTime = timeBetweenSpawn;
            Instantiate(spawnStack.Pop(), transform);
        }

    }

    public void SpawnEnemies(RoundData data)
    {
        SpawnEnemies(data.pathCreator, data.enemyCount);
    }

    public void SpawnEnemies(PathCreator pathCreator, int enemyCount)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            enemyPrefab.GetComponent<Enemy>().pathCreator = pathCreator;
            spawnStack.Push(enemyPrefab);
        }
    }

    public bool FinishedSpawning()
    {
        
        return spawnStack.Count == 0 && transform.childCount == 0;
    }
}

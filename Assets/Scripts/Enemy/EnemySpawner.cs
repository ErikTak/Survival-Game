using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public List<EnemyGroup> enemyGroups; // A list of groups of enemies to spawn this wave
        public int waveQuota; // Total number of enemies to spawn in the wave
        public float spawnInterval; // Time for the wave
        public int spawnCount; // Number of enemies already spawned in the wave
    }

    [System.Serializable]
    public class EnemyGroup
    {
        public string enemyName;
        public int enemyCount; // The number of enemies spawned this wave
        public int spawnCount; // The number of enemies ALREADY spawned this wave
        public GameObject enemyPrefab;
    }

    public List<Wave> waves; // List of all waves
    public int currentWaveCount; // Index of current wave

    [Header("Spawner Attributes")]
    float spawnTimer; // Time used to determin when to spawn the next enemy
    public int enemiesAlive; // Enemies currently alive on the screen
    public int maxEnemiesAllowed; // Maximum number of enemies that can be on the screen at one time
    public bool maxEnemiesReached = false;
    public float waveInterval;

    [Header("Spawn Position")]
    public List<Transform> relativeSpawnPoints; //List to store all relative spawn points of enemies

    Transform player;

    void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform;
        CalculateWaveQuota();
    }

    void Update()
    {
        if (currentWaveCount < waves.Count && waves[currentWaveCount].spawnCount == 0)
        {
            StartCoroutine(BeginNextWave());
        }

        spawnTimer += Time.deltaTime;

        // Check if it's time to spawn enemy
        if (spawnTimer >= waves[currentWaveCount].spawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemies();
        }
    }

    IEnumerator BeginNextWave()
    {
        // Wait a given amount of seconds before starting next wave
        yield return new WaitForSeconds(waveInterval);

        // If there are more waves to start after the current wave, start the next wave
        if (currentWaveCount < waves.Count - 1)
        {
            currentWaveCount++;
            CalculateWaveQuota();
        }
    }

    void CalculateWaveQuota()
    {
        int currentWaveQuota = 0;
        foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
        {
            currentWaveQuota += enemyGroup.enemyCount;
        }

        waves[currentWaveCount].waveQuota = currentWaveQuota;
        Debug.Log(currentWaveQuota);
    }


    // Method for spawning enemies in waves and checking if the number of maximum enemies at the screen is reached yet
    void SpawnEnemies()
    {
        // Check if the minimum number of enemies have been spawned in the wave
        if (waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota && !maxEnemiesReached)
        {
            // Spawn each type of enemy until the quota is filled
            foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
            {
                // Check if the minimum number of enemies of this type have been spawned
                if (enemyGroup.spawnCount < enemyGroup.enemyCount)
                {
                    // Limit the number of enemies that can be spawned at one time
                    if (enemiesAlive >= maxEnemiesAllowed)
                    {
                        maxEnemiesReached = true;
                        return;
                    }

                    // Spawn the enemies at the predetermined point
                    Instantiate(enemyGroup.enemyPrefab, player.position + relativeSpawnPoints[Random.Range(0, relativeSpawnPoints.Count)].position, Quaternion.identity);

                    enemyGroup.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                    enemiesAlive++;
                }
            }
        }

        if (enemiesAlive < maxEnemiesAllowed)
        {
            maxEnemiesReached = false;
        }
    }

    public void OnEnemyKilled()
    {
        // Reduce the number of enemies currently alive
        enemiesAlive--;
    }
}

/*

    public class EnemySpawner : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float innerRadius;
    public float outerRadius;

    [SerializeField]
    private GameObject batEnemyPrefab;
    [SerializeField]
    private GameObject skeletonEnemyPrefab;

    [SerializeField]
    private GameObject sShapeEnemyPrefab;

    [SerializeField]
    private float batEnemyInterval;
    [SerializeField]
    private float skeletonEnemyInterval;

    [SerializeField]
    private float sShapeEnemyInterval;


    void Start()
    {
        StartCoroutine(spawnEnemy(batEnemyInterval, batEnemyPrefab));
        StartCoroutine(spawnEnemy(skeletonEnemyInterval, skeletonEnemyPrefab));

        StartCoroutine(spawnEnemy(sShapeEnemyInterval, sShapeEnemyPrefab));
    }

    // move the spawner with the player
    void Update()
    {
        transform.position = target.position + offset;
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, target.position + (transform.position = Random.insideUnitCircle.normalized * Random.Range(innerRadius, outerRadius)), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }
}

//GameObject newEnemy = Instantiate(enemy, target.position + new Vector3(Random.Range(-5f, 5), Random.Range(-6f, 6), 0), Quaternion.identity);

*/
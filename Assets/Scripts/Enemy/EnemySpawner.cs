using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public enum SpawnState { Spawning, Waiting, Counting };

    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public Transform enemy;
        public int spawnCount;
        public float spawnDelay;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public float timeBetweenWaves = 5f;
    private float waveCountdown;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.Counting;

    [Header("Spawn Position")]
    public Transform[] relativeSpawnPoints; //List to store all relative spawn points of enemies

    Transform player;

    void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform;
        if (relativeSpawnPoints.Length == 0)
        {
            Debug.LogError("No Spawn points referenced.");
        }

        waveCountdown = timeBetweenWaves;
    }

    void Update()
    {
        if (state == SpawnState.Waiting)
        {
            if (!EnemyIsAlive())
            {
                // Begin new round
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.Spawning)
            {
                Debug.Log("state is now spawning");
                StartCoroutine(BeginNextWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        Debug.Log("wave completed");

        state = SpawnState.Counting;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            // LAST CALL WHEN ALL WAVES ARE COMPLETE
            nextWave = 0;
            Debug.Log("All waves complete");
        }
        else
        {
            nextWave++;
        }
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            //if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0) <-- can maybe be used to spawn more enemy if number falls below certain amount?
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }


    IEnumerator BeginNextWave(Wave _wave)
    {
        Debug.Log("Spawnig wave: " + _wave.waveName);

        state = SpawnState.Spawning;

        for (int i = 0; i < _wave.spawnCount; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(_wave.spawnDelay);
        }

        state = SpawnState.Waiting;

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        Debug.Log("Spawning enemy" + _enemy.name);
        Transform _sp = relativeSpawnPoints[Random.Range(0, relativeSpawnPoints.Length)];
        Vector3 spawnPosition = player.position + _sp.position;

        Instantiate(_enemy, spawnPosition, Quaternion.identity);
    }
}

/*
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
        spawnTimer += Time.deltaTime;

        // Check if it's time to spawn enemy
        if (spawnTimer >= waves[currentWaveCount].spawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemies();
        }

        // Check if the current wave is complete
        if (waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota)
        {
            // Check if there are more waves to start
            if (currentWaveCount < waves.Count - 1)
            {
                // Wait for waveInterval seconds before starting the next wave
                StartCoroutine(BeginNextWave());
            }
        }
    }

    IEnumerator BeginNextWave()
    {
        Debug.Log("BeginNextWave() was called");

        // Wait a given amount of seconds before starting next wave
        yield return new WaitForSeconds(waveInterval);

        if (currentWaveCount < waves.Count - 1)
        {
            currentWaveCount++;
            CalculateWaveQuota();
        }
        yield break;
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
        Debug.Log("BeginNextWave() was called");

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
*/
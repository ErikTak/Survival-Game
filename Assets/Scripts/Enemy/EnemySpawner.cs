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
        public List<EnemyGroup> enemyGroups; // List of all enemies
    }

    [System.Serializable]
    public class EnemyGroup
    {
        public string enemyName;
        public int enemyCount; // The number of enemies spawned this wave
        public int enemySpawnCount; // The number of enemies to spawn this wave
        public GameObject enemyPrefab;
    }

    public Wave[] waves;
    public int nextWave = 0;

    public float timeBetweenWaves = 5f;
    private float waveCountdown;

    public float waveTimer;
    public float maximumWaveTime;

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
        waveTimer = maximumWaveTime;
    }

    void Update()
    {
        if (state == SpawnState.Waiting)
        {
            if (!EnemyIsAlive() || waveTimer <= 0)
            {
                // Begin new round
                WaveCompleted();
            }
            else
            {
                waveTimer -= Time.deltaTime;
                return;
            }
        }

        if (state == SpawnState.Spawning)
        {
            waveTimer -= Time.deltaTime;
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
        waveTimer = maximumWaveTime;

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

        // Spawn each type of enemy until the quota is filled
        foreach (var enemyGroup in waves[nextWave].enemyGroups)
        {
            int enemiesToSpawn = enemyGroup.enemySpawnCount - enemyGroup.enemyCount;
            //Debug.Log($"Spawning {enemiesToSpawn} {enemyGroup.enemyName}(s)");

            for (int i = 0; i < enemiesToSpawn; i++)
            {

                SpawnEnemy(enemyGroup.enemyPrefab);
                enemyGroup.enemyCount++;

                yield return new WaitForSeconds(maximumWaveTime / GetTotalEnemyCount(_wave));

            }
        }

        state = SpawnState.Waiting;

        yield break;
    }

    int GetTotalEnemyCount(Wave wave)
    {
        int count = 0;
        foreach (var enemyGroup in wave.enemyGroups)
        {
            count += enemyGroup.enemySpawnCount;
        }
        return count;
    }


    void SpawnEnemy(GameObject _enemy)
    {
        Transform _sp = relativeSpawnPoints[Random.Range(0, relativeSpawnPoints.Length)];
        Vector3 spawnPosition = player.position + _sp.position;

        Instantiate(_enemy, spawnPosition, Quaternion.identity);
    }

}
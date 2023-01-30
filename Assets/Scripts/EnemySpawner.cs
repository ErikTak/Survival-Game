using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private float batEnemyInterval;
    [SerializeField]
    private float skeletonEnemyInterval;

    void Start()
    {
        StartCoroutine(spawnEnemy(batEnemyInterval, batEnemyPrefab));
        StartCoroutine(spawnEnemy(skeletonEnemyInterval, skeletonEnemyPrefab));
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

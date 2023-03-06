using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private ColoredFlash flashEffect;
    [SerializeField] private Color flashColors;

    public GameUiElements scoreUI;
    public EnemyScriptableObject enemyData;
    public HealthBar healthBar;

    // Current stats
    [HideInInspector]
    public float currentMoveSpeed;
    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public float currentDamage;
    [HideInInspector]
    public int currentValue;

    public float despawnDistance = 40f;
    Transform player;

    void Awake()
    {
        currentValue = enemyData.ScoreValue;
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;
        healthBar.SetMaxHealth(currentHealth);
    }

    void Start()
    {
        scoreUI = FindObjectOfType<GameUiElements>();
        player = FindObjectOfType<PlayerStats>().transform;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) >= despawnDistance)
        {
            ReturnEnemy();
        }
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
        healthBar.SetHealth(currentHealth);
        flashEffect.Flash(flashColors);

        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        // Refenrece the script from the collided collider and deal damage using TakeDamage()
        if (col.gameObject.CompareTag("Player"))
        {
            PlayerStats player = col.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(currentDamage); // Make sure to use currentDamage instead of weaponData.damage in case any damage multipliers in the future

            // Check if the enemy's movePattern is set to Circle
            EnemyMove enemyMove = GetComponent<EnemyMove>();
            if (enemyMove.movePattern == EnemyMove.Pattern.Circle)
            {
                Kill();
            }
        }
    }

    private void OnDestroy()
    {
        EnemySpawner es = FindObjectOfType<EnemySpawner>();
        es.OnEnemyKilled();
        scoreUI.CountScore(currentValue);
    }

    void ReturnEnemy()
    {
        EnemySpawner es = FindObjectOfType<EnemySpawner>();
        transform.position = player.position + es.relativeSpawnPoints[Random.Range(0, es.relativeSpawnPoints.Count)].position;
    }

}

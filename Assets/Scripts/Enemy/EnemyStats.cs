using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private ColoredFlash flashEffect;
    [SerializeField] private Color flashColors;

    Animator am;
    DropRateManager drm;

    [HideInInspector]
    public GameUiElements scoreUI;
    public EnemyScriptableObject enemyData;
    public HealthBar healthBar;

    CapsuleCollider2D collider;

    // Current stats
    [HideInInspector]
    public float currentMoveSpeed;
    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public bool isDead;
    [HideInInspector]
    public float currentDamage;
    [HideInInspector]
    public int currentValue;

    public float despawnDistance = 40f;
    Transform player;
    

    void Awake()
    {
        collider = GetComponent<CapsuleCollider2D>();
        am = GetComponent<Animator>();
        drm = GetComponent<DropRateManager>();
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

        isDead = false;
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
        isDead = true;
        collider.enabled = false;
        drm.SpawnTheDrop();
        StartCoroutine(DestroyAfterDelay(am.GetCurrentAnimatorStateInfo(0).length));
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
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
        scoreUI.CountScore(currentValue);
    }

    void ReturnEnemy()
    {
        EnemySpawner es = FindObjectOfType<EnemySpawner>();
        transform.position = player.position + es.relativeSpawnPoints[Random.Range(0, es.relativeSpawnPoints.Length)].position;
    }
}

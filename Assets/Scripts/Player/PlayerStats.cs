using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public CharacterScriptableObject characterData;
    Animator am;
    public HealthBar healthBar;
    public ExperienceBar expBar;
    public PauseMenu pauseMenu;
    
    public SFXController sfxController;

    //delete this if not working
    public WeaponManager wpmng;
    public GameObject levelUpFX;

    public bool isDead = false;
    public bool isDashing = false;


    [SerializeField] private ColoredFlash flashEffect;
    [SerializeField] private Color flashColors;

    // Current stats
    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public float currentRecovery;
    [HideInInspector]
    public float currentMoveSpeed;
    [HideInInspector]
    public float currentMight;
    [HideInInspector]
    public float currentProjectileSpeed;
    [HideInInspector]
    public float currentMagnet;

    public float originalMoveSpeed;

    // Experience and level of the player
    [Header("Experience/Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap;

    // Class for defining a level range and the corresponding experience cap increase for that range
    [System.Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncrease;
    }

    // I-frames
    [Header("I-Frames")]
    public float invincibilityDuration;
    float invincibilityTimer;
    bool isInvincible;

    public List<LevelRange> levelRanges;

    void Awake()
    {
        sfxController = FindObjectOfType<SFXController>();
        am = GetComponent<Animator>();
        // Assign the variables
        currentHealth = characterData.MaxHealth;
        currentRecovery = characterData.Recovery;
        currentMoveSpeed = characterData.MoveSpeed;
        currentMight = characterData.Might;
        currentProjectileSpeed = characterData.ProjectileSpeed;
        currentMagnet = characterData.Magnet;
        originalMoveSpeed = currentMoveSpeed;
    }

    void Start()
    {
        // Initialize the experience cap as the first experience cap increase
        experienceCap = levelRanges[0].experienceCapIncrease;
        healthBar.SetMaxHealth(currentHealth);
        expBar.SetMaxExp(experienceCap);
        expBar.SetExp(1);
        isDead = true;
        StartCoroutine(DelayStartGame(1.5f));
    }

    void Update()
    {
        // Check if the invincibilityTimer is 0 and reduce the timer; if It's 0 then set the flag to false
        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }   
        else if (isInvincible)
        {
            isInvincible = false;
        }

        Recover();
        healthBar.SetHealth(currentHealth);
    }

    private IEnumerator DelayStartGame(float delay)
    {
        yield return new WaitForSeconds(delay);

        isDead = false;
    }

    public void IncreaseExperience(int amount)
    {
        experience += amount;
        expBar.SetExp(experience);
        LevelUpChecker();
    }

    void LevelUpChecker()
    {
        if (experience >= experienceCap)
        {
            sfxController.Play("LevelUp");
            level++;
            experience -= experienceCap;

            int experienceCapIncrease = 0;
            foreach (LevelRange range in levelRanges)
            {
                if (level >= range.startLevel && level <= range.endLevel)
                {
                    experienceCapIncrease = range.experienceCapIncrease;
                    break;
                }
            }
            experienceCap += experienceCapIncrease;

            // Instantiate the levelupFX on the player and open the levelup menu
            Instantiate(levelUpFX, transform.position, Quaternion.identity);
            pauseMenu.ShowLvlUpMenu();

            // Randomize rewards when leveling up
            wpmng.RewardTypeChooser();

            expBar.SetMaxExp(experienceCap);
            expBar.SetExp(experience);
        }
    }

    public void Dash()
    {
            invincibilityTimer = invincibilityDuration;
            isInvincible = true;
    }

    public void TakeDamage(float dmg)
    {
        if (!isInvincible && !isDead)
        {
            sfxController.Play("TakeDamage");

            currentHealth -= dmg;
            healthBar.SetHealth(currentHealth);
            flashEffect.Flash(flashColors);

            invincibilityTimer = invincibilityDuration;
            isInvincible = true;

            if (currentHealth <= 0)
            {
                Kill();
            }
        }
    }

    public void Kill()
    {
        isDead = true;

        sfxController.Play("PlayerDeath");

        float animationDelay = am.GetCurrentAnimatorStateInfo(0).length;
        StartCoroutine(DelayedEndGame(animationDelay));
    }

    private IEnumerator DelayedEndGame(float delay)
    {
        // Wait for 2 seconds before ending the game
        yield return new WaitForSeconds(delay);

        sfxController.Play("GameOver");
        FindObjectOfType<PauseMenu>().EndGame();
    }

    public void RestoreHealth(float amount)
    {
        sfxController.Play("HealUp");

        if (currentHealth < characterData.MaxHealth)
        {
            currentHealth += amount;
            healthBar.SetHealth(currentHealth);

            if (currentHealth > characterData.MaxHealth)
            {
                currentHealth = characterData.MaxHealth;
            }

        }
    }

    void Recover()
    {
        // While the characters health is below the maxHealth, heal the caracter by the recovery amount
        if(currentHealth < characterData.MaxHealth)
        {
            currentHealth += currentRecovery * Time.deltaTime;

            // if the healing would go over the maxHealth, set it as the max health
            if (currentHealth > characterData.MaxHealth)
            {
                currentHealth = characterData.MaxHealth;
            }
        }
    }

    // These two functions increases the currentMagnet radius and then returns to the original value after a given amount of time
    public void IncreaseMagnet()
    {
        float originalMagnet = currentMagnet;
        currentMagnet *= 10f;
        StartCoroutine(ResetMagnet(originalMagnet, 3f));
    }

    private IEnumerator ResetMagnet(float originalMagnet, float duration)
    {
        yield return new WaitForSeconds(duration);
        currentMagnet = originalMagnet;
    }

    // These two functions increases the currentMoveSpeed value and then returns to the original value after a given amount of time
    public void IncreaseMoveSpeed()
    {
        currentMoveSpeed = 10f;
        StartCoroutine(ResetMoveSpeed(originalMoveSpeed, 5f));
    }

    private IEnumerator ResetMoveSpeed(float originalMoveSpeed, float duration)
    {
        yield return new WaitForSeconds(duration);
        currentMoveSpeed = originalMoveSpeed;
    }
}

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

    public bool isDead = false;


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
    }

    void Start()
    {
        // Initialize the experience cap as the first experience cap increase
        experienceCap = levelRanges[0].experienceCapIncrease;
        healthBar.SetMaxHealth(currentHealth);
        expBar.SetMaxExp(experienceCap);
        expBar.SetExp(1);
        isDead = false;
        Debug.Log("playerstats Start is called");
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
            pauseMenu.ShowLvlUpMenu();

            // Randomize rewards when leveling up
            wpmng.RewardTypeChooser();

            expBar.SetMaxExp(experienceCap);
            expBar.SetExp(experience);
        }
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
}

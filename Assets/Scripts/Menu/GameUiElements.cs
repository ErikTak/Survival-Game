using UnityEngine;
using UnityEngine.UI;
using System;

public class GameUiElements : MonoBehaviour
{
    public TMPro.TextMeshProUGUI levelDisplay;
    public TMPro.TextMeshProUGUI waveDisplay;
    public TMPro.TextMeshProUGUI timeDisplay;
    public TMPro.TextMeshProUGUI scoreDisplay;

    // Used to keep track of the active game time
    TimeSpan gameDuration;

    private PlayerStats ps;
    private EnemySpawner es;
    private float startTime;

    public int score;

    private void Start()
    {
        ps = FindObjectOfType<PlayerStats>();
        es = FindObjectOfType<EnemySpawner>();
        startTime = Time.time;
    }

    void Update()
    {
        levelDisplay.text = "level: " + ps.level.ToString();
        waveDisplay.text = es.waves[es.currentWaveCount].waveName;

        // Calculate the duration of the game
        float timeElapsed = Time.time - startTime;
        TimeSpan timeSpan = TimeSpan.FromSeconds(timeElapsed);

        // Display the duration in the format "mm:ss"
        timeDisplay.text = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);

        // Display the score rounded to the nearest whole number
        scoreDisplay.text = score.ToString();
    }

    // Count the score (killed enemy's value * the multiplier (1.0f * current wave))
    public void CountScore(int enemyValue)
    {
        float currentScore = score;
        float newScore;
        int currentWaveCount = es.currentWaveCount;
        float multiplier = 1.0f + (currentWaveCount / 10.0f);
        newScore = enemyValue * multiplier;

        // Round the sum of the current score and the new score
        score = ((int)Math.Round(currentScore + newScore));
    }
}
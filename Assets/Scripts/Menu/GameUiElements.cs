using UnityEngine;
using UnityEngine.UI;
using System;

public class GameUiElements : MonoBehaviour
{
    public TMPro.TextMeshProUGUI levelDisplay;
    public TMPro.TextMeshProUGUI waveDisplay;
    public TMPro.TextMeshProUGUI timeDisplay;

    TimeSpan gameDuration;

    private PlayerStats ps;
    private EnemySpawner es;
    private float startTime;

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
    }
}
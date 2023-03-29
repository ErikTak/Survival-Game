using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameMenu : MonoBehaviour
{
    public TMPro.TextMeshProUGUI scoreDisplay;
    public TMPro.TextMeshProUGUI highScoreDisplay;

    GameUiElements gameUI;

    void Start()
    {
        highScoreDisplay.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    public void DisplayScores(int score)
    {
        gameUI = FindObjectOfType<GameUiElements>();

        scoreDisplay.text = score.ToString();
        
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScoreDisplay.text = score.ToString();
        }
    }

    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
    }
}

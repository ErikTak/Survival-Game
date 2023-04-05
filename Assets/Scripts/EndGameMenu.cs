using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameMenu : MonoBehaviour
{
    public TMPro.TextMeshProUGUI scoreDisplay;
    public TMPro.TextMeshProUGUI highScoreDisplay;
    public TMPro.TextMeshProUGUI highScoreLabelDisplay;

    int rewardOption;

    GameUiElements gameUI;

    void Start()
    {
        rewardOption = PlayerPrefs.GetInt("RewardOption");

        Debug.Log("RewardOption is: " + rewardOption);

        if (rewardOption == 0)
        {
            highScoreDisplay.text = PlayerPrefs.GetInt("BasicHighScore", 0).ToString();
            highScoreLabelDisplay.text = "<color=blue>Control</color>\n High Score:"; 
        }
        if (rewardOption == 1)
        {
            highScoreDisplay.text = PlayerPrefs.GetInt("RandomHighScore", 0).ToString();
            highScoreLabelDisplay.text = "<color=blue>Case 1</color>\n High Score:";
        }
        if (rewardOption == 2)
        {
            highScoreDisplay.text = PlayerPrefs.GetInt("ChooseHighScore", 0).ToString();
            highScoreLabelDisplay.text = "<color=blue>Case 2</color>\n High Score:";
        }
    }

    public void DisplayScores(int score)
    {
        rewardOption = PlayerPrefs.GetInt("RewardOption");
        Debug.Log("RewardOption is: " + rewardOption);

        gameUI = FindObjectOfType<GameUiElements>();

        scoreDisplay.text = score.ToString();
        
        if (rewardOption == 0)
        {
            if (score > PlayerPrefs.GetInt("BasicHighScore", 0))
            {
                PlayerPrefs.SetInt("BasicHighScore", score);
                highScoreDisplay.text = score.ToString();
            }
        }
        if (rewardOption == 1)
        {
            if (score > PlayerPrefs.GetInt("RandomHighScore", 0))
            {
                PlayerPrefs.SetInt("RandomHighScore", score);
                highScoreDisplay.text = score.ToString();
            }
        }
        if (rewardOption == 2)
        {
            if (score > PlayerPrefs.GetInt("ChooseHighScore", 0))
            {
                PlayerPrefs.SetInt("ChooseHighScore", score);
                highScoreDisplay.text = score.ToString();
            }
        }
    }

    public void ResetHighScore()
    {
        if (rewardOption == 0)
        {
            PlayerPrefs.DeleteKey("BasicHighScore");
            highScoreDisplay.text = PlayerPrefs.GetInt("BasicHighScore", 0).ToString();
        }
        if (rewardOption == 1)
        {
            PlayerPrefs.DeleteKey("RandomHighScore");
            highScoreDisplay.text = PlayerPrefs.GetInt("RandomHighScore", 0).ToString();
        }
        if (rewardOption == 2)
        {
            PlayerPrefs.DeleteKey("ChooseHighScore");
            highScoreDisplay.text = PlayerPrefs.GetInt("ChooseHighScore", 0).ToString();
        }
    }
}

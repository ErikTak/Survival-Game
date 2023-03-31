using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreMenuDisplay : MonoBehaviour
{
    public TMPro.TextMeshProUGUI basicHighScoreDisplay;
    public TMPro.TextMeshProUGUI randomHighScoreDisplay;
    public TMPro.TextMeshProUGUI chooseHighScoreDisplay;

    void Start()
    {
        basicHighScoreDisplay.text = PlayerPrefs.GetInt("BasicHighScore", 0).ToString();
        randomHighScoreDisplay.text = PlayerPrefs.GetInt("RandomHighScore", 0).ToString();
        chooseHighScoreDisplay.text = PlayerPrefs.GetInt("ChooseHighScore", 0).ToString();
    }

    public void ResetBasicHighScore()
    {
        PlayerPrefs.DeleteKey("BasicHighScore");
    }
    public void ResetRandomHighScore()
    {
        PlayerPrefs.DeleteKey("RandomHighScore");
    }
    public void ResetChooseHighScore()
    {
        PlayerPrefs.DeleteKey("ChooseHighScore");
    }
}

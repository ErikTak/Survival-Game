using UnityEngine;
using UnityEngine.UI;

public class GameUiElements : MonoBehaviour
{
    public PlayerStats ps;
    public TMPro.TextMeshProUGUI levelDisplay;

    // Update is called once per frame
    void Update()
    {
        levelDisplay.text = ps.level.ToString();
    }
}

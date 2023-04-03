using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RewardTypeDropdown : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public TMPro.TextMeshProUGUI gameTypeText;

    private void Update()
    {
        int rewardOption = PlayerPrefs.GetInt("RewardOption", 0);

        dropdown.value = rewardOption;
        
        if (rewardOption == 0)
        {
            gameTypeText.text = "Your game mode is currently set to Basic. All upgrades you get for leveling up have been preselected for you.";
        }
        if (rewardOption == 1)
        {
            gameTypeText.text = "Your game mode is currently set to Random. Your upgrades will be randomly selected when leveling up.";
        }
        if (rewardOption == 2)
        {
            gameTypeText.text = "Your game mode is currently set to Choose. You get to see all possible upgrade options and choose the one you like the most.";
        }

    }

    public void SetDropdownValue()
    {
        // Save the selected value to PlayerPrefs
        PlayerPrefs.SetInt("RewardOption", dropdown.value);
    }
}

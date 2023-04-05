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
            gameTypeText.text = "<color=blue>Control.</color> \n Level up rewards have been pre-selected for you. You get the same order of rewards every attempt.";
        }
        if (rewardOption == 1)
        {
            gameTypeText.text = "<color=blue>Case 1.</color> \n Your reward is randomly selected when leveling up.";
        }
        if (rewardOption == 2)
        {
            gameTypeText.text = "<color=blue>Case 2.</color> \n At level up, you get to see all possible upgrade options and choose the one you like the most.";
        }

    }

    public void SetDropdownValue()
    {
        if (dropdown.value == 0)
        {
            PlayerPrefs.SetInt("RewardOption", 0);
        }        
        if (dropdown.value == 1)
        {
            PlayerPrefs.SetInt("RewardOption", 1);
        }        
        if (dropdown.value == 2)
        {
            PlayerPrefs.SetInt("RewardOption", 2);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardDropDownManager : MonoBehaviour
{
    public Dropdown rewardDropdown;

    public void HandleInputData(int val)
    {
        if (val == 0)
        {
            PlayerPrefs.SetInt("RewardOption", 0);
        }
        if (val == 1)
        {
            PlayerPrefs.SetInt("RewardOption", 1);
        }
        if (val == 2)
        {
            PlayerPrefs.SetInt("RewardOption", 2);
        }
    }
}

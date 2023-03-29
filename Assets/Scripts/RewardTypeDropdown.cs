using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RewardTypeDropdown : MonoBehaviour
{
    public TMP_Dropdown dropdown; // Reference to the TMP_Dropdown component

    private void Update()
    {
        dropdown.value = PlayerPrefs.GetInt("RewardOption", 0);
    }

    public void SetDropdownValue()
    {
        // Save the selected value to PlayerPrefs
        PlayerPrefs.SetInt("RewardOption", dropdown.value);
    }
}

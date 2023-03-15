using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    public GameObject[] weaponControllers;
    public GameObject singleUpgradeButtonObject;
    public GameObject[] fixedUpgradeButtonObjects;
    public GameObject[] imageObjects;


    // List and index for the controlled reward system
    private List<int> numbers;
    private int index;

    private void Start()
    {
        SetInventoryImage();

        numbers = new List<int> { 0, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 4, 3, 2, 6, 7, 4, 1, 8, 5, 3, 2, 1 };
        index = 0;
    }

    public void RewardTypeChooser()
    {
        int rewardOption = PlayerPrefs.GetInt("RewardOption");

        if (rewardOption == 0)
        {
            Debug.Log("rewardoption is 0");
            ControlledReward(GetNextNumber());
        }
        if (rewardOption == 1)
        {
            Debug.Log("rewardoption is 1");
            RandomizeOneReward();
        }
        if (rewardOption == 2)
        {
            Debug.Log("rewardoption is 2");
            RandomizeRewards();
        }

    }

    /// <summary>
    /// CONTROLLED REWARD (GET A PREDETERMINED REWARD)
    /// </summary>
    /// 
    public int GetNextNumber()
    {
        int nextNumber = numbers[index];
        index = (index + 1) % numbers.Count;
        return nextNumber;
    }

    public void ControlledReward(int weaponControllerIndex)
    {
        HideFixedButtons();

        // Activate the specified weapon controller
        weaponControllers[weaponControllerIndex].SetActive(true);


        // Set the card display for the single upgrade button
        CardDisplay cardDisplay = singleUpgradeButtonObject.GetComponent<CardDisplay>();
        if (cardDisplay != null)
        {
            WeaponScriptableObject weapon = weaponControllers[weaponControllerIndex].GetComponent<WeaponController>().weapons[weaponControllers[weaponControllerIndex].GetComponent<WeaponController>().currentWeaponIndex];
            cardDisplay.weapon = weapon;
            cardDisplay.weaponController = weaponControllers[weaponControllerIndex];
            cardDisplay.SetCardDetails();
        }
        else
        {
            Debug.LogError("CardDisplay component not found on singleUpgradeButtonObject");
        }

    }




    /// <summary>
    /// RANDOM REWARD (ONLY 1 OPTION TO CHOOSE)
    /// </summary>
    public void RandomizeOneReward()
    {
        HideFixedButtons();

        int randomIndex = Random.Range(0, weaponControllers.Length);
        CardDisplay cardDisplay = singleUpgradeButtonObject.GetComponent<CardDisplay>();
        if (cardDisplay != null)
        {
            WeaponScriptableObject weapon = weaponControllers[randomIndex].GetComponent<WeaponController>().weapons[0];
            cardDisplay.weapon = weapon;
            cardDisplay.weaponController = weaponControllers[randomIndex];
            cardDisplay.SetCardDetails();
        }
        else
        {
            Debug.LogError("CardDisplay component not found on singleUpgradeButtonObject");
        }
    }



    /// <summary>
    /// SEMI-RANDOM REWARD (OPTION TO CHOOSE FROM 3)
    /// </summary>
    public void RandomizeRewards()
    {
        HideSingleButton();

        int[] usedCards = new int[weaponControllers.Length];
        for (int i = 0; i < fixedUpgradeButtonObjects.Length; i++)
        {
            // Assign weapon controller to fixedUpgradeButtonObject[i]
            int randomIndex = Random.Range(0, weaponControllers.Length);
            while (usedCards[randomIndex] == 1)
            {
                randomIndex = Random.Range(0, weaponControllers.Length);
            }
            usedCards[randomIndex] = 1;

            CardDisplay cardDisplay = fixedUpgradeButtonObjects[i].GetComponent<CardDisplay>();
            if (cardDisplay != null)
            {
                WeaponScriptableObject weapon;

                if (!weaponControllers[randomIndex].activeSelf)
                {
                    weapon = weaponControllers[randomIndex].GetComponent<WeaponController>().weapons[weaponControllers[randomIndex].GetComponent<WeaponController>().currentWeaponIndex];
                }
                else
                {
                    weapon = weaponControllers[randomIndex].GetComponent<WeaponController>().weapons[weaponControllers[randomIndex].GetComponent<WeaponController>().currentWeaponIndex + 1];
                }
                cardDisplay.weapon = weapon;
                cardDisplay.weaponController = weaponControllers[randomIndex];
                cardDisplay.SetCardDetails();
            }
            else
            {
                Debug.LogError("CardDisplay component not found on fixedUpgradeButtonObject[" + i + "]");
            }
        }
    }


    public void SetInventoryImage()
    {
        int currentWeaponControllerIndex = 0;

        for (int i = 0; i < imageObjects.Length; i++)
        {
            InventoryImageDisplay imageDisplay = imageObjects[i].GetComponent<InventoryImageDisplay>();

            while (currentWeaponControllerIndex < weaponControllers.Length && !weaponControllers[currentWeaponControllerIndex].activeSelf)
            {
                currentWeaponControllerIndex++;
            }

            if (currentWeaponControllerIndex < weaponControllers.Length)
            {
                WeaponScriptableObject weapon = weaponControllers[currentWeaponControllerIndex].GetComponent<WeaponController>().weapons[weaponControllers[currentWeaponControllerIndex].GetComponent<WeaponController>().currentWeaponIndex];
                imageDisplay.weapon = weapon;
                currentWeaponControllerIndex++;
            }
            else
            {
                imageDisplay.weapon = null;
            }

            imageDisplay.SetInventoryImage();
        }
    }

    public void HideFixedButtons()
    {
        singleUpgradeButtonObject.SetActive(true);

        foreach (GameObject fixedUpgradeButtonObject in fixedUpgradeButtonObjects)
        {
            fixedUpgradeButtonObject.SetActive(false);
        }
    }

    public void HideSingleButton()
    {
        singleUpgradeButtonObject.SetActive(false);

        foreach (GameObject fixedUpgradeButtonObject in fixedUpgradeButtonObjects)
        {
            fixedUpgradeButtonObject.SetActive(true);
        }
    }

}
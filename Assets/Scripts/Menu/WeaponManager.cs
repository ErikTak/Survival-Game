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

        switch (rewardOption)
        {
            case 0:
                ControlledReward(GetNextNumber());
                break;
            case 1:
                RandomizeOneReward();
                break;
            case 2:
                RandomizeRewards();
                break;
            default:
                Debug.LogError("Invalid reward option selected: " + rewardOption);
                break;
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
            WeaponScriptableObject weapon;

            if (!weaponControllers[weaponControllerIndex].activeSelf)
            {
                weapon = weaponControllers[weaponControllerIndex].GetComponent<WeaponController>().weapons[weaponControllers[weaponControllerIndex].GetComponent<WeaponController>().currentWeaponIndex];
            }
            else
            {
                weapon = weaponControllers[weaponControllerIndex].GetComponent<WeaponController>().weapons[weaponControllers[weaponControllerIndex].GetComponent<WeaponController>().currentWeaponIndex + 1];
            }
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

        int randomIndex;
        WeaponScriptableObject weapon;
        CardDisplay cardDisplay = singleUpgradeButtonObject.GetComponent<CardDisplay>();

        do
        {
            randomIndex = Random.Range(0, weaponControllers.Length);

            if (!weaponControllers[randomIndex].activeSelf)
            {
                weapon = weaponControllers[randomIndex].GetComponent<WeaponController>().weapons[weaponControllers[randomIndex].GetComponent<WeaponController>().currentWeaponIndex];
            }
            else
            {
                weapon = weaponControllers[randomIndex].GetComponent<WeaponController>().weapons[weaponControllers[randomIndex].GetComponent<WeaponController>().currentWeaponIndex + 1];
            }

        } while (weaponControllers[randomIndex].GetComponent<WeaponController>().currentWeaponIndex == 10);

        cardDisplay.weapon = weapon;
        cardDisplay.weaponController = weaponControllers[randomIndex];
        cardDisplay.SetCardDetails();
    }




    /// <summary>
    /// SEMI-RANDOM REWARD (OPTION TO CHOOSE FROM 3)
    /// </summary>
    public void RandomizeRewards()
    {
        HideSingleButton();

        int[] usedCards = new int[weaponControllers.Length];
        int controllerIndex = 0;
        for (int i = 0; i < fixedUpgradeButtonObjects.Length; i++)
        {
            // Assign weapon controller to fixedUpgradeButtonObject[i]
            if (controllerIndex >= weaponControllers.Length)
            {
                controllerIndex = 0;
            }
            while (usedCards[controllerIndex] == 1)
            {
                controllerIndex++;
                if (controllerIndex >= weaponControllers.Length)
                {
                    controllerIndex = 0;
                }
            }
            usedCards[controllerIndex] = 1;

            CardDisplay cardDisplay = fixedUpgradeButtonObjects[i].GetComponent<CardDisplay>();
            if (cardDisplay != null)
            {
                WeaponScriptableObject weapon;
                WeaponController weaponController = weaponControllers[controllerIndex].GetComponent<WeaponController>();

                if (weaponController.currentWeaponIndex == 10)
                {
                    cardDisplay.gameObject.SetActive(false);
                    continue;
                }

                if (!weaponControllers[controllerIndex].activeSelf)
                {
                    weapon = weaponController.weapons[weaponController.currentWeaponIndex];
                }
                else
                {
                    weapon = weaponController.weapons[weaponController.currentWeaponIndex + 1];
                }
                cardDisplay.weapon = weapon;
                cardDisplay.weaponController = weaponControllers[controllerIndex];
                cardDisplay.SetCardDetails();
            }
            else
            {
                Debug.LogError("CardDisplay component not found on fixedUpgradeButtonObject[" + i + "]");
            }
            controllerIndex++;
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
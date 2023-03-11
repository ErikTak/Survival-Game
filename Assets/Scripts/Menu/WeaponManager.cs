using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    public GameObject[] weaponControllers;
    public GameObject[] upgradeButtonObjects;
    public GameObject[] imageObjects;

    private void Start()
    {
        SetInventoryImage();
    }

    public void RewardTypeChooser()
    {
        int rewardOption = PlayerPrefs.GetInt("RewardOption");

        if (rewardOption == 0)
        {
            Debug.Log("rewardoption is 0");

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
    /// <param name="weaponControllerIndex"></param>
    public void ControlledReward(int weaponControllerIndex)
    {
        // Disable all upgrade buttons except the second one
        for (int i = 0; i < upgradeButtonObjects.Length; i++)
        {
            upgradeButtonObjects[i].SetActive(i == 1);
        }

        // Activate the specified weapon controller
        for (int i = 0; i < weaponControllers.Length; i++)
        {
            weaponControllers[i].SetActive(i == weaponControllerIndex);
        }

        // Set the card display for the second upgrade button
        CardDisplay cardDisplay = upgradeButtonObjects[1].GetComponent<CardDisplay>();
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
            Debug.LogError("CardDisplay component not found on upgradeButtonObject[1]");
        }
    }


    /// <summary>
    /// RANDOM REWARD (ONLY 1 OPTION TO CHOOSE)
    /// </summary>
    public void RandomizeOneReward()
    {
        int[] usedCards = new int[weaponControllers.Length];
        for (int i = 0; i < upgradeButtonObjects.Length; i++)
        {
            // Disable all upgrade buttons except the second one
            upgradeButtonObjects[i].SetActive(i == 1);
        }

        // Repeat the randomization 5 times with a 1 second delay
        StartCoroutine(RandomizeOneRewardCoroutine(5, usedCards));
    }

    private IEnumerator RandomizeOneRewardCoroutine(int numIterations, int[] usedCards)
    {
        for (int i = 0; i < numIterations; i++)
        {
            int randomIndex = Random.Range(0, weaponControllers.Length);
            while (usedCards[randomIndex] == 1)
            {
                randomIndex = Random.Range(0, weaponControllers.Length);
            }
            usedCards[randomIndex] = 1;

            CardDisplay cardDisplay = upgradeButtonObjects[1].GetComponent<CardDisplay>();
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
                Debug.LogError("CardDisplay component not found on upgradeButtonObject[1]");
            }

            yield return new WaitForSeconds(1f);
        }

        // Re-enable all upgrade buttons
        for (int i = 0; i < upgradeButtonObjects.Length; i++)
        {
            upgradeButtonObjects[i].SetActive(true);
        }
    }


    /// <summary>
    /// SEMI-RANDOM REWARD (OPTION TO CHOOSE FROM 3)
    /// </summary>
    public void RandomizeRewards()
    {
        int[] usedCards = new int[weaponControllers.Length];
        for (int i = 0; i < upgradeButtonObjects.Length; i++)
        {
            int randomIndex = Random.Range(0, weaponControllers.Length);
            while (usedCards[randomIndex] == 1)
            {
                randomIndex = Random.Range(0, weaponControllers.Length);
            }
            usedCards[randomIndex] = 1;

            Debug.Log(upgradeButtonObjects.Length);

            CardDisplay cardDisplay = upgradeButtonObjects[i].GetComponent<CardDisplay>();
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
                Debug.LogError("CardDisplay component not found on upgradeButtonObject[" + i + "]");
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
}
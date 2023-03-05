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

            CardDisplay cardDisplay = upgradeButtonObjects[i].GetComponent<CardDisplay>();
            if (cardDisplay != null)
            {
                WeaponScriptableObject weapon = weaponControllers[randomIndex].GetComponent<WeaponController>().weapons[weaponControllers[randomIndex].GetComponent<WeaponController>().currentWeaponIndex];
                cardDisplay.weapon = weapon;
                cardDisplay.weaponController = weaponControllers[randomIndex];
                cardDisplay.SetCardDetails();
            }
            else
            {
                Debug.LogError("CardDisplay component not found on upgradeButtonObject[" + i + "]");
            }

        }
        //FindObjectOfType<CardDisplay>().SetCardDetails();
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
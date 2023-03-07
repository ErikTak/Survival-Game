using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public WeaponScriptableObject weapon;
    public GameObject weaponController;
    public PauseMenu menu;
    public WeaponManager wm;

    public TMPro.TextMeshProUGUI nameText;
    public TMPro.TextMeshProUGUI descriptionText;
    public TMPro.TextMeshProUGUI upgradeNumber;

    public Image artworkImage;

    private void Awake()
    {
        menu = FindObjectOfType<PauseMenu>();
        wm = FindObjectOfType<WeaponManager>();
    }

    public void SetCardDetails()
    {
        // Debug.Log(weapon.WeaponDescription + weapon.WeaponDescription);
        nameText.text = weapon.WeaponName;
        descriptionText.text = weapon.WeaponDescription;
        artworkImage.sprite = weapon.WeaponArtwork;

        upgradeNumber.text = "+" + weapon.UpgradeNum.ToString();
    }

    public void UpgradeWeapon()
    {
        if (!weaponController.activeSelf)
        {
            weaponController.SetActive(true);
        }
        else
        {
            weaponController.GetComponent<WeaponController>().currentWeaponIndex++;
        }
        wm.SetInventoryImage();
        menu.HideLvlUpMenu();
    }
}

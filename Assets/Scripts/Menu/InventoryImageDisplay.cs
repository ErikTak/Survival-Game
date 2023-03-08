using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryImageDisplay : MonoBehaviour
{
    public WeaponScriptableObject weapon;

    public Image artworkImage;
    public TMPro.TextMeshProUGUI upgradeNumber;

    public void SetInventoryImage()
    {
        if (weapon == null)
        {
            artworkImage.enabled = false;
            upgradeNumber.enabled = false;
        }
        else
        {
            artworkImage.sprite = weapon.WeaponArtwork;
            upgradeNumber.text = "+" + weapon.UpgradeNum.ToString();
            artworkImage.enabled = true;
            upgradeNumber.enabled = true;
        }
    }

}

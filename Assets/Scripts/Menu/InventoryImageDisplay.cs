using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryImageDisplay : MonoBehaviour
{
    public WeaponScriptableObject weapon;

    public Image artworkImage;

    public void SetInventoryImage()
    {
        if (weapon == null)
        {
            artworkImage.enabled = false;
        }
        else
        {
            artworkImage.sprite = weapon.WeaponArtwork;
            artworkImage.enabled = true;
        }
    }

}

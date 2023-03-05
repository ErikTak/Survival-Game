using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardScriptableObject", menuName = "ScriptableObjects/Card")]
public class CardScriptableObject : ScriptableObject
{
    public string cardName;
    public string cardDescription;

    public bool isUpgraded;

    public Sprite cardArtwork;

    public float damage;
    public float speed;
    public float cooldown;
    public float pierce;

}

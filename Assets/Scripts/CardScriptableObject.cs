using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardScriptableObject", menuName = "ScriptableObjects/Card")]
public class CardScriptableObject : ScriptableObject
{
    public new string name;
    public string description;

    public Sprite artwork;

    public float damage;
    public float speed;
    public float cooldown;
    public float pierce;
}

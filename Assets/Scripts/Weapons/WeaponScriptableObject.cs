using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponScriptableObject", menuName = "ScriptableObjects/Weapon")]

public class WeaponScriptableObject : ScriptableObject
{
    [SerializeField]
    GameObject prefab;
    public GameObject Prefab { get => prefab; private set => prefab = value; }

    // Base stats for weapons
    [SerializeField]
    float damage;
    public float Damage { get => damage; private set => damage = value; }

    [SerializeField]
    float speed;
    public float Speed { get => speed; private set => speed = value; }

    [SerializeField]
    float cooldownDuration;
    public float CooldownDuration { get => cooldownDuration; private set => cooldownDuration = value; }

    [SerializeField]
    float destroyAfterSeconds;
    public float DestroyAfterSeconds { get => destroyAfterSeconds; private set => destroyAfterSeconds = value; }

    [SerializeField]
    int pierce;
    public int Pierce { get => pierce; private set => pierce = value; }

    [SerializeField]
    int projCount;
    public int ProjCount { get => projCount; private set => projCount = value; }

    [SerializeField]
    float offsetRadius;
    public float OffsetRadius { get => offsetRadius; private set => offsetRadius = value; }

    [SerializeField]
    float projDelay;
    public float ProjDelay { get => projDelay; private set => projDelay = value; }

    [SerializeField]
    string weaponName;
    public string WeaponName { get => weaponName; private set => weaponName = value; }

    [SerializeField]
    int upgradeNum;
    public int UpgradeNum { get => upgradeNum; private set => upgradeNum = value; }

    [SerializeField]
    string weaponDescription;
    public string WeaponDescription { get => weaponDescription; private set => weaponDescription = value; }

    [SerializeField]
    Sprite weaponArtwork;
    public Sprite WeaponArtwork { get => weaponArtwork; private set => weaponArtwork = value; }

}
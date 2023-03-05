using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Weapon Stats")]
    public List<WeaponScriptableObject> weapons;
    public int currentWeaponIndex;
    float currentCooldown;

    public string weaponName;
    public string weaponDescription;
    public Sprite artworkImage;

    protected PlayerMove pm;
    public EnemyMove[] enemies;
    public GameObject nearestEnemy;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        pm = FindObjectOfType<PlayerMove>();

        currentCooldown = weapons[currentWeaponIndex].CooldownDuration; // At the start set the current cooldown to be the cooldown duration of the current weapon
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        weaponName = weapons[currentWeaponIndex].WeaponName;
        weaponDescription = weapons[currentWeaponIndex].WeaponDescription;
        artworkImage = weapons[currentWeaponIndex].WeaponArtwork;
        enemies = FindObjectsOfType<EnemyMove>();
        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0f) // Once the cooldown becomes 0, attack
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {
        currentCooldown = weapons[currentWeaponIndex].CooldownDuration;
    }

    protected void FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        float nearestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = enemy;
            }
        }
    }
}
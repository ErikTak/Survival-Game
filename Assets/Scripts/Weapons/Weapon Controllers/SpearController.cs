using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearController : WeaponController
{

    private GameObject bulletPrefab;
    private float bulletSpeed;

    private GameObject nearestEnemy;

    protected override void Start()
    {
        base.Start();
        bulletPrefab = weaponData.Prefab;
        bulletSpeed = weaponData.Speed;
    }

    protected override void Attack()
    {
        base.Attack();
        FindNearestEnemy();

        if (nearestEnemy != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Vector2 direction = (nearestEnemy.transform.position - transform.position).normalized;
            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
            float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) -45f;
            bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }


    void FindNearestEnemy()
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

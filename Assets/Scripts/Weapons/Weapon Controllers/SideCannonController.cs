using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideCannonController : WeaponController
{
    public Transform firePoint;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        Shoot();
    }

    protected override void Update()
    {
        base.Update();
    }

    void Shoot()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        int projCount = weapons[currentWeaponIndex].ProjCount;
        float projAngle = weapons[currentWeaponIndex].OffsetRadius; // The angle between each projectile
        float startAngle = -projAngle * (projCount - 1) / 2f; // The starting angle for the projectiles

        for (int i = 0; i < projCount; i++)
        {
            // Create a new bullet
            GameObject bullet = Instantiate(weapons[currentWeaponIndex].Prefab, firePoint.position, Quaternion.identity);

            // Calculate the direction and velocity of the bullet
            Vector3 shootDirection = Quaternion.AngleAxis(startAngle + i * projAngle, Vector3.back) * (mousePosition - firePoint.position).normalized;
            bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * weapons[currentWeaponIndex].Speed;

            // Set the rotation of the bullet
            float angle = (Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg);
            bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            FindObjectOfType<SFXController>().Play("SideCannonWepSFX");
        }
    }
}
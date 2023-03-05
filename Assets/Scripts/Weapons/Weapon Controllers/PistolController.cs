using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolController : WeaponController
{
    public Transform firePoint;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            base.Attack();
            Shoot();
        }
    }

    protected override void Update()
    {
        base.Update();
    }

    void Shoot()
    {
        Debug.Log("shoot is called");
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        GameObject bullet = Instantiate(weapons[currentWeaponIndex].Prefab, firePoint.position, Quaternion.identity);
        Vector3 shootDirection = (mousePosition - firePoint.position).normalized;
        bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * weapons[currentWeaponIndex].Speed;
        bullet.GetComponent<PistolBehaviour>().DirectionChecker(shootDirection); // Reference and set direction
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarshotController : WeaponController
{
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
        WeaponScriptableObject currentWeapon = weapons[currentWeaponIndex];

        Debug.Log("ProjCount: " + currentWeapon.ProjCount);

        // Define firing directions based on the projectile count of the weapon
        List<Vector2> firingDirections = new List<Vector2>();
        switch (currentWeapon.ProjCount)
        {
            case 1:
                firingDirections.Add(transform.up);
                break;
            case 2:
                firingDirections.Add(transform.up);
                firingDirections.Add(-transform.right);
                break;
            case 3:
                firingDirections.Add(transform.up);
                firingDirections.Add(-transform.right);
                firingDirections.Add(-transform.up);
                break;
            case 4:
                firingDirections.Add(transform.up);
                firingDirections.Add(-transform.right);
                firingDirections.Add(-transform.up);
                firingDirections.Add(transform.right);
                break;
            case 5:
                firingDirections.Add(transform.up);
                firingDirections.Add(-transform.right);
                firingDirections.Add(-transform.up);
                firingDirections.Add(transform.right);
                firingDirections.Add(transform.up - transform.right);
                break;
            case 6:
                firingDirections.Add(transform.up);
                firingDirections.Add(-transform.right);
                firingDirections.Add(-transform.up);
                firingDirections.Add(transform.right);
                firingDirections.Add(transform.up - transform.right);
                firingDirections.Add(-transform.up - transform.right);
                break;
            case 7:
                firingDirections.Add(transform.up);
                firingDirections.Add(-transform.right);
                firingDirections.Add(-transform.up);
                firingDirections.Add(transform.right);
                firingDirections.Add(transform.up - transform.right);
                firingDirections.Add(-transform.up - transform.right);
                firingDirections.Add(-transform.up + transform.right);
                break;
            case 8:
                firingDirections.Add(transform.up);
                firingDirections.Add(-transform.right);
                firingDirections.Add(-transform.up);
                firingDirections.Add(transform.right);
                firingDirections.Add((transform.up - transform.right) /2);
                firingDirections.Add((-transform.up - transform.right) /2);
                firingDirections.Add((-transform.up + transform.right) / 2);
                firingDirections.Add((transform.up + transform.right) / 2);
                break;
            default: // For projectile counts > 7
                //firingDirections.Add(transform.up);
                for (int i = 1; i < currentWeapon.ProjCount; i++)
                {
                    float angle = (i - 1) * 360f / (currentWeapon.ProjCount - 1);
                    Vector2 direction = Quaternion.Euler(0f, 0f, angle) * transform.up;
                    firingDirections.Add(direction);
                }
                break;
        }

        // Instantiate projectiles in all firing directions
        foreach (Vector2 direction in firingDirections)
        {
            Instantiate(currentWeapon.Prefab, transform.position, Quaternion.LookRotation(Vector3.forward, Quaternion.Euler(0f, 0f, 90f) * direction))
                .GetComponent<StarshotBehaviour>().projDirection = direction;
        }

        FindObjectOfType<SFXController>().Play("StarshotWepSFX");
    }
}

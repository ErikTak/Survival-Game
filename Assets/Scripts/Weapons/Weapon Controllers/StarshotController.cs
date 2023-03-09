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
        float projAngleIncrement = 360f / (currentWeapon.ProjCount * 2);
        Vector2 projDirection = transform.right;

        for (int i = 0; i < currentWeapon.ProjCount; i++)
        {
            // Shoot in four directions (up, right, down, left)
            Vector2 projDirectionUp = Quaternion.Euler(0f, 0f, i * projAngleIncrement) * projDirection;
            Vector2 projDirectionRight = Quaternion.Euler(0f, 0f, (i * projAngleIncrement) + 90f) * projDirection;
            Vector2 projDirectionDown = Quaternion.Euler(0f, 0f, (i * projAngleIncrement) + 180f) * projDirection;
            Vector2 projDirectionLeft = Quaternion.Euler(0f, 0f, (i * projAngleIncrement) + 270f) * projDirection;

            // Shoot in four diagonal directions (up-right, down-right, up-left, down-left)
            Vector2 projDirectionUpRight = Quaternion.Euler(0f, 0f, (i * projAngleIncrement) + 45f) * projDirection;
            Vector2 projDirectionDownRight = Quaternion.Euler(0f, 0f, (i * projAngleIncrement) + 135f) * projDirection;
            Vector2 projDirectionUpLeft = Quaternion.Euler(0f, 0f, (i * projAngleIncrement) + 225f) * projDirection;
            Vector2 projDirectionDownLeft = Quaternion.Euler(0f, 0f, (i * projAngleIncrement) + 315f) * projDirection;

            // Instantiate projectiles in all eight directions
            Instantiate(currentWeapon.Prefab, transform.position, Quaternion.LookRotation(Vector3.forward, Quaternion.Euler(0f, 0f, 90f) * projDirectionUp)).GetComponent<StarshotBehaviour>().projDirection = projDirectionUp;
            Instantiate(currentWeapon.Prefab, transform.position, Quaternion.LookRotation(Vector3.forward, Quaternion.Euler(0f, 0f, 90f) * projDirectionRight)).GetComponent<StarshotBehaviour>().projDirection = projDirectionRight;
            Instantiate(currentWeapon.Prefab, transform.position, Quaternion.LookRotation(Vector3.forward, Quaternion.Euler(0f, 0f, 90f) * projDirectionDown)).GetComponent<StarshotBehaviour>().projDirection = projDirectionDown;
            Instantiate(currentWeapon.Prefab, transform.position, Quaternion.LookRotation(Vector3.forward, Quaternion.Euler(0f, 0f, 90f) * projDirectionLeft)).GetComponent<StarshotBehaviour>().projDirection = projDirectionLeft;
            Instantiate(currentWeapon.Prefab, transform.position, Quaternion.LookRotation(Vector3.forward, Quaternion.Euler(0f, 0f, 90f) * projDirectionUpRight)).GetComponent<StarshotBehaviour>().projDirection = projDirectionUpRight;
            Instantiate(currentWeapon.Prefab, transform.position, Quaternion.LookRotation(Vector3.forward, Quaternion.Euler(0f, 0f, 90f) * projDirectionDownRight)).GetComponent<StarshotBehaviour>().projDirection = projDirectionDownRight;
            Instantiate(currentWeapon.Prefab, transform.position, Quaternion.LookRotation(Vector3.forward, Quaternion.Euler(0f, 0f, 90f) * projDirectionUpLeft)).GetComponent<StarshotBehaviour>().projDirection = projDirectionUpLeft;
            Instantiate(currentWeapon.Prefab, transform.position, Quaternion.LookRotation(Vector3.forward, Quaternion.Euler(0f, 0f, 90f) * projDirectionDownLeft)).GetComponent<StarshotBehaviour>().projDirection = projDirectionDownLeft;

            FindObjectOfType<SFXController>().Play("StarshotWepSFX");
        }
    }
}

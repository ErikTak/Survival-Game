using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneTrapController : WeaponController
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        StartCoroutine(ShootCoroutine());
    }
    private IEnumerator ShootCoroutine()
    {
        //Vector3 cameraPos = Camera.main.transform.position;
        //float halfScreenHeight = Camera.main.orthographicSize;
        Vector3 parentPos = transform.position;
        int projectileCount = weapons[currentWeaponIndex].ProjCount;

        for (int i = 0; i < projectileCount; i++)
        {
            Vector3 randomPos = Random.insideUnitCircle * (weapons[currentWeaponIndex].OffsetRadius / projectileCount);
            Vector3 shootPos = parentPos + randomPos;

            // Calculate position of the top of the screen
            //Vector3 topOfScreenPos = new Vector2(cameraPos.x, cameraPos.y + halfScreenHeight);

            // Calculate direction from projectile to top of screen
            //Vector3 direction = topOfScreenPos - shootPos;

            GameObject projectile = Instantiate(weapons[currentWeaponIndex].Prefab, shootPos, Quaternion.identity);
            //projectile.transform.up = direction.normalized;
            //projectile.transform.LookAt(parentPos);
            yield return new WaitForSeconds(weapons[currentWeaponIndex].ProjDelay);
        }
    }
}
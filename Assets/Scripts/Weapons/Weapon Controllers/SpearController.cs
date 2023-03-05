using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearController : WeaponController
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        FindNearestEnemy();

        StartCoroutine(SpawnSpears());
    }

    IEnumerator SpawnSpears()
    {

        for (int i = 0; i < weapons[currentWeaponIndex].ProjCount; ++i)
        {
            if (nearestEnemy != null)
            {
                GameObject bullet = Instantiate(weapons[currentWeaponIndex].Prefab, transform.position, Quaternion.identity);
                Vector2 direction = (nearestEnemy.transform.position - transform.position).normalized;
                bullet.GetComponent<Rigidbody2D>().velocity = direction * weapons[currentWeaponIndex].Speed;
                float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) + 135f;
                bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                yield return new WaitForSeconds(weapons[currentWeaponIndex].ProjDelay);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeController : WeaponController
{
    private float offsetRange;
    private Vector2 lastMovedVector;
    
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        lastMovedVector = pm.lastMovedVector; // update the lastMovedVector once when Attack is called
        StartCoroutine(SpawnKnives());
    }

    IEnumerator SpawnKnives()
    {
        offsetRange = weapons[currentWeaponIndex].OffsetRadius;

        for (int i = 0; i < weapons[currentWeaponIndex].ProjCount; ++i)
        {
            GameObject spawnedKnife = Instantiate(weapons[currentWeaponIndex].Prefab);
            Vector2 spawnPosition = (Vector2)transform.position + new Vector2(Random.Range(-offsetRange, offsetRange), Random.Range(-offsetRange, offsetRange));
            spawnedKnife.transform.position = spawnPosition; // Assign the position to be the same as this object which is parented to the player
            spawnedKnife.GetComponent<KnifeBehaviour>().DirectionChecker(lastMovedVector); // Reference and set direction using the stored lastMovedVector

            FindObjectOfType<SFXController>().Play("KnifeWepSFX");

            yield return new WaitForSeconds(weapons[currentWeaponIndex].ProjDelay);
        }
    }

}

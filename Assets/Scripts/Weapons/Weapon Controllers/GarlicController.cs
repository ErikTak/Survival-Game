using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicController : WeaponController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedGarlic = Instantiate(weapons[currentWeaponIndex].Prefab);
        spawnedGarlic.transform.position = transform.position; // Assign the position to be the same as this object which is parented to the player
        spawnedGarlic.transform.parent = transform; // So that it spawns below this object

        FindObjectOfType<SFXController>().Play("GarlicWepSFX");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookBehaviour : ProjectileWeaponBehaviour
{
    
    private void Update()
    {
        // Rotate the book around the parent object
        transform.RotateAround(transform.parent.position, Vector3.forward, weaponData.Speed * Time.deltaTime);
    }
}


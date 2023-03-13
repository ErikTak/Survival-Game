using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarshotBehaviour : ProjectileWeaponBehaviour
{
    public Vector3 projDirection;

    protected override void Start()
    {
        base.Start();
        Debug.Log("Projectile instantiated!");
    }

    void Update()
    {
        transform.position += projDirection * currentSpeed * Time.deltaTime; // Set the movement of the knife
    }
}


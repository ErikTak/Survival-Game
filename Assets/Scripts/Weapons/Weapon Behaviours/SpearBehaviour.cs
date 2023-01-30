using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearBehaviour : ProjectileWeaponBehaviour
{

    private Transform target;

    protected override void Start()
    {
        base.Start();
    }

    public void Initialize(Transform target)
    {
        this.target = target;
    }

}

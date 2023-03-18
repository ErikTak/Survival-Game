using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableProps : MonoBehaviour
{
    public float health;
    DropRateManager drm;

    public void Start()
    {
        drm = GetComponent<DropRateManager>();
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;

        if (health <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        drm.SpawnTheDrop();
        Destroy(gameObject);
    }
}

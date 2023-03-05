using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Pickup, ICollectible
{
    public int heathToRestore;

    public void Collect()
    {

    }

    // increase the player experience once the pickup has reached the player
    private void OnDestroy()
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.RestoreHealth(heathToRestore);
    }
}



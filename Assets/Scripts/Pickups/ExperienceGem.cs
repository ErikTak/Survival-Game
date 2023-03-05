using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceGem : Pickup, ICollectible
{
    public int experienceGranted;

    public void Collect()
    {
        
    }

    // increase the player experience once the pickup has reached the player
    private void OnDestroy()
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.IncreaseExperience(experienceGranted);
    }
}

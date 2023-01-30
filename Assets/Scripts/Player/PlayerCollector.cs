using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        // Check if the gameobject has ICollectible interface
        if (col.gameObject.TryGetComponent(out ICollectible collectible))
        {
            // If it does call collect method
            collectible.Collect();
        }
    }
}

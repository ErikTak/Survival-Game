using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    PlayerStats player;
    CircleCollider2D playerCollector;
    public float pullSpeed;

    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        playerCollector = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        playerCollector.radius = player.currentMagnet;
    }

    private void FixedUpdate()
    {
        // Find all the colliders in the collector radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, playerCollector.radius);

        foreach (Collider2D col in colliders)
        {
            // Check if the gameobject has ICollectible interface
            if (col.gameObject.TryGetComponent(out ICollectible collectible))
            {
                // Move the object towards the player
                Vector3 direction = (transform.position - col.transform.position).normalized;
                float distance = Vector3.Distance(transform.position, col.transform.position);
                float step = pullSpeed * Time.deltaTime;

                if (distance > step)
                {
                    col.transform.Translate(direction * step, Space.World);
                }
                else
                {
                    col.transform.position = transform.position;
                }

                // If it does call collect method
                collectible.Collect();
            }
        }
    }
}

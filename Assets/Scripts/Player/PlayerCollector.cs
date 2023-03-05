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

    private void OnTriggerEnter2D(Collider2D col)
    {
        // Check if the gameobject has ICollectible interface
        if (col.gameObject.TryGetComponent(out ICollectible collectible))
        {
            // gets the rb of the object and applies force * pullSpeed to it in the direction of the player
            Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
            Vector2 forceDirection = (transform.position - col.transform.position).normalized;
            rb.AddForce(forceDirection * pullSpeed);

            // If it does call collect method
            collectible.Collect();
        }
    }
}

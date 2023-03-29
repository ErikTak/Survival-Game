using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : Pickup, ICollectible
{
    public float rotationSpeed = 2.0f; // Determines how fast the potion rotates.
    public float rotationAngle = 30.0f; // Determines the maximum angle the potion can rotate.

    private Quaternion startRotation;

    public void Collect()
    {

    }

    // increase the player experience once the pickup has reached the player
    private void OnDestroy()
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.IncreaseMoveSpeed();
    }

    private void Start()
    {
        startRotation = transform.rotation;
        StartCoroutine(RotateObject());
    }

    private IEnumerator RotateObject()
    {
        while (true)
        {
            float angle = Mathf.PingPong(Time.time * rotationSpeed, rotationAngle * 2) - rotationAngle;
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime);
            yield return null;
        }
    }
}

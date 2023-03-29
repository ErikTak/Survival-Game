using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehaviour : MonoBehaviour
{
    ParticleSystem particles;

    void Start()
    {
        FindObjectOfType<SFXController>().Play("Explosion");

        particles = GetComponent<ParticleSystem>();
        float particleDelay = particles.main.duration;

        StartCoroutine(DelayDestroy(particleDelay));
    }

    private IEnumerator DelayDestroy(float delay)
    {
        // Wait for the delay before destroying the gameObject
        yield return new WaitForSeconds(delay);

        Destroy(gameObject);
    }
}

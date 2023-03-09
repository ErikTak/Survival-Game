using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehaviour : MonoBehaviour
{
    Animator am;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<SFXController>().Play("Explosion");

        am = GetComponent<Animator>();
        float animationDelay = am.GetCurrentAnimatorStateInfo(0).length;
        StartCoroutine(DelayDestroy(animationDelay));
    }
    private IEnumerator DelayDestroy(float delay)
    {
        // Wait for 2 seconds before ending the game
        yield return new WaitForSeconds(delay);

        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpBehaviour : MonoBehaviour
{
    Animator am;

    void Start()
    {
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

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
        StartCoroutine(DelayDestroy());
    }
    private IEnumerator DelayDestroy()
    {
        float timeElapsed = 0f;
        float waitTime = 1f;

        while (timeElapsed < waitTime)
        {
            timeElapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        //yield return new WaitForSeconds(0.9f);

        Destroy(gameObject);
    }
}

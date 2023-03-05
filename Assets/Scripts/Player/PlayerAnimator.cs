using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator am;
    PlayerMove pm;
    SpriteRenderer sr;
    PlayerStats ps;

    private string currentState;

    public bool gameStarted = false;

    const string PLAYER_IDLE = "Idle";
    const string PLAYER_WALK = "PlayerWalk";
    const string PLAYER_DIE = "PlayerDie";
    const string PLAYER_GAMESTART_LIGHTNING = "LightningGameStartAnim";
    const string PLAYER_GAMESTART_WAKEUP = "WakeUpAnim";


    void Start()
    {
        am = GetComponent<Animator>();
        pm = GetComponent<PlayerMove>();
        sr = GetComponent<SpriteRenderer>();
        ps = GetComponent<PlayerStats>();

        // Call the coroutine to play the two animations
        StartCoroutine(PlayGameStartAnimations());
    }

    void Update()
    {
        if (gameStarted)
        {
            if (!ps.isDead)
            {
                if (pm.moveDir.x != 0 || pm.moveDir.y != 0)
                {
                    ChangeAnimationState(PLAYER_WALK);

                    SpriteDirectionChecker();
                }
                else
                {
                    ChangeAnimationState(PLAYER_IDLE);
                }
            }
            else
            {
                ChangeAnimationState(PLAYER_DIE);
            }
        }
    }

    void ChangeAnimationState(string newState)
    {
        // Stop animation from playing again if it's already playing
        if (currentState == newState) return;

        // Play the animation
        am.Play(newState);

        // Reassign the current state
        currentState = newState;
    }

    void SpriteDirectionChecker()
    {
        if (pm.lastHorizontalVector < 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }

    IEnumerator PlayGameStartAnimations()
    {
        // Play the lightning animation
        am.Play(PLAYER_GAMESTART_LIGHTNING);

        // Wait for it to finish
        yield return new WaitForSeconds(am.GetCurrentAnimatorStateInfo(0).length);

        // Play the wakeup animation
        am.Play(PLAYER_GAMESTART_WAKEUP);

        // Wait for it to finish
        yield return new WaitForSeconds(am.GetCurrentAnimatorStateInfo(0).length);

        // Set the gameStarted bool to true
        gameStarted = true;
    }
}

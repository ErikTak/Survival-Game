using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    Animator am;
    EnemyMove pm;
    SpriteRenderer sr;
    PlayerStats ps;

    void Start()
    {
        
    }
    
    /*
    void Update()
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
    */
}

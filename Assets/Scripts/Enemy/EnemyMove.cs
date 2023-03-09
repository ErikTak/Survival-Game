using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Animator am;
    EnemyStats enemy;
    Transform player;
    SpriteRenderer sr;

    float knockbackDistance; // Distance the enemy will be knocked back when hit

    // Decide what pattern the enemy should move in
    public Pattern movePattern;

    private float radius; // Radius of the circle movement
    private float angle; // Starting angle for the movement
    private int clockwiseDirection; // Direction of movement

    // Store the previous move direction for flipping the sprite

    private Vector3 previousPosition;

    public enum Pattern
    {
        Straight,
        Sshape,
        Circle
    }

    // Start is called before the first frame update
    void Start()
    {
        am = GetComponent<Animator>();
        enemy = GetComponent<EnemyStats>();
        player = FindObjectOfType<PlayerMove>().transform;
        sr = GetComponent<SpriteRenderer>();

        previousPosition = transform.position;

        if (movePattern == Pattern.Circle)
        {
            // Calculate the initial radius based on the distance between the enemy and the player
            radius = Vector2.Distance(transform.position, player.position);

            // Decide the direction of movement randomly at spawn
            clockwiseDirection = Random.Range(0, 2) == 0 ? -1 : 1;

            // Calculate the initial angle of the enemy
            Vector2 direction = player.position - transform.position;
            angle = Mathf.Atan2(direction.y, direction.x);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemy.isDead)
        {
            am.SetBool("Alive", true);

            if (movePattern == Pattern.Straight)
            {
                Straight();
            }
            else if (movePattern == Pattern.Sshape)
            {
                Sshape();
            }
            else if (movePattern == Pattern.Circle)
            {
                Circle();
            }


            // SPRITE FLIP
            // Calculate the direction of movement and flip the sprite accordingly
            Vector3 moveDirection = transform.position - previousPosition;
            moveDirection.Normalize();

            if (moveDirection.x < 0)
            {
                sr.flipX = true;
            }
            else
            {
                sr.flipX = false;
            }

            // Store the current position as the previous position for the next frame
            previousPosition = transform.position;
        }
        else
        {
            am.SetBool("Alive", false);
        }
    }

    public void Knockback(float damage)
    {
        knockbackDistance = damage * 0.1f;
        Vector2 knockbackDirection = transform.position - player.transform.position; // Calculate knockback direction
        knockbackDirection.Normalize();
        StartCoroutine(KnockbackCoroutine(knockbackDirection));
    }

    private IEnumerator KnockbackCoroutine(Vector2 knockbackDirection)
    {
        float knockbackDistanceRemaining = knockbackDistance;
        while (knockbackDistanceRemaining > 0f)
        {
            float knockbackThisFrame = Mathf.Min(knockbackDistanceRemaining, 1f);
            transform.position += (Vector3)(knockbackDirection * knockbackThisFrame);
            knockbackDistanceRemaining -= knockbackThisFrame;
            yield return null;
        }
    }

    void Straight()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemy.currentMoveSpeed * Time.deltaTime); // Constantly move towards the players
    }

    void Sshape()
    {
        float amplitude = 20f;   // the amplitude of the S shape
        float frequency = 1.5f;   // the frequency of the S shape
        float offset = 0f;      // the starting offset of the S shape

        // Calculate the movement along the straight line between the player and the enemy
        Vector3 directionToPlayer = player.transform.position - transform.position;
        Vector3 directionAlongLine = Vector3.Project(directionToPlayer, transform.right);
        float x = Mathf.Sin(Time.time * frequency + offset) * amplitude;
        Vector3 movementAlongLine = directionAlongLine.normalized * x;

        // Move towards the player while also moving in an S shape pattern along the straight line
        Vector3 targetPosition = player.transform.position + movementAlongLine;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, enemy.currentMoveSpeed * Time.deltaTime);
    }

    void Circle()
    {
        
        if (Vector2.Distance(transform.position, player.position) <= 5f)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemy.currentMoveSpeed * 10 * Time.deltaTime); // Constantly move towards the players
        }
        else
        {
            // Decrease the radius over time
            radius -= enemy.currentMoveSpeed * Time.deltaTime;

            // Calculate the new position for the enemy based on the angle and radius
            Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0).normalized * radius;
            transform.position = player.position + offset;

            // Move the angle in either a clockwise or anticlockwise direction
            angle += clockwiseDirection * enemy.currentMoveSpeed * Time.deltaTime;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerMove : MonoBehaviour
{

    // Movement
    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public float lastVerticalVector;
    [HideInInspector]
    public Vector2 moveDir;
    [HideInInspector]
    public Vector2 lastMovedVector;

    private float lastRollTime;

    // References
    Animator am;
    Rigidbody2D rb;
    PlayerStats player;

    // Start is called before the first frame update
    void Start()
    {
        am = GetComponent<Animator>();
        player = GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();
        lastMovedVector = new Vector2(1, 0f); // If we didn't move at the start of the game
    }

    // Update is called once per frame
    void Update()
    {
        InputManagement();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void InputManagement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(moveX, moveY).normalized;

        if (moveDir.x != 0)
        {
            lastHorizontalVector = moveDir.x;
            lastMovedVector = new Vector2(lastHorizontalVector, 0f); // Last moved X
        }

        if (moveDir.y != 0)
        {
            lastVerticalVector = moveDir.y;
            lastMovedVector = new Vector2(0f, lastVerticalVector); // Last moved y
        }

        if (moveDir.x != 0 && moveDir.y != 0)
        {
            lastMovedVector = new Vector2(lastHorizontalVector, lastVerticalVector); // While moving
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Roll();
        }
    }

    void Move()
    {
        if (!player.isDead)
        {
            rb.velocity = new Vector2(moveDir.x * player.currentMoveSpeed, moveDir.y * player.currentMoveSpeed);
        }
    }

    public void Roll()
    {
        if (Time.time - lastRollTime >= 2f)
        {
            player.Roll();

            player.isRolling = true;
            Debug.Log("isRolling is: " + player.isRolling);
            lastRollTime = Time.time;

            float rollDistance = 5f;
            Vector2 rollDirection = lastMovedVector.normalized;
            Vector2 targetPosition = rb.position + rollDirection * rollDistance;

            float rollTime = 0.2f;
            float elapsedTime = 0f;

            // Temporarily ignore collisions with enemies
            Collider2D[] enemyColliders = GameObject.FindGameObjectsWithTag("Enemy").Select(enemy => enemy.GetComponent<Collider2D>()).ToArray();
            foreach (Collider2D enemyCollider in enemyColliders)
            {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), enemyCollider, true);
            }


            StartCoroutine(RollCoroutine(targetPosition, rollTime, elapsedTime));
        }
    }

    IEnumerator RollCoroutine(Vector2 targetPosition, float rollTime, float elapsedTime)
    {
        while (elapsedTime < rollTime)
        {
            float t = Mathf.Clamp01(elapsedTime / rollTime); // limit the interpolation factor to the range [0, 1]
            rb.MovePosition(Vector2.Lerp(rb.position, targetPosition, t));
             
            // rb.MovePosition(Vector2.Lerp(rb.position, targetPosition, elapsedTime / rollTime));
            Debug.Log("rb.position:" + rb.position);
            Debug.Log("targetPosition" + targetPosition);
            Debug.Log("elapsedTime: " + elapsedTime);
            Debug.Log("rollTime" + rollTime);
            Debug.Log("elapsedTime / rollTime" + elapsedTime / rollTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Re-enable collisions with enemies
        Collider2D[] enemyColliders = GameObject.FindGameObjectsWithTag("Enemy").Select(enemy => enemy.GetComponent<Collider2D>()).ToArray();
        foreach (Collider2D enemyCollider in enemyColliders)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), enemyCollider, false);
        }


        player.isRolling = false;
        Debug.Log("isRolling is: " + player.isRolling);
    }
}

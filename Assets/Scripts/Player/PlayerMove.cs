using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerMove : MonoBehaviour
{
    public DashBar dashBar;


    // Movement
    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public float lastVerticalVector;
    [HideInInspector]
    public Vector2 moveDir;
    [HideInInspector]
    public Vector2 lastMovedVector;

    private float lastDashTime;

    // References
    Animator am;
    Rigidbody2D rb;
    PlayerStats player;

    public float dashBarFillTime = 2.0f;
    private float currentDashBarFillTime;

    // Start is called before the first frame update
    void Start()
    {
        am = GetComponent<Animator>();
        player = GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();
        lastMovedVector = new Vector2(1, 0f); // If we didn't move at the start of the game
        dashBar.SetMaxEnergy(dashBarFillTime);
        currentDashBarFillTime = dashBarFillTime;
    }

    // Update is called once per frame
    void Update()
    {
        InputManagement();

        // If the player is not dashing, fill up the dash bar over time
        if (!player.isDashing)
        {
            currentDashBarFillTime += Time.deltaTime;
            dashBar.SetEnergy(currentDashBarFillTime);
        }
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
            Dash();
        }
    }

    void Move()
    {
        if (!player.isDead)
        {
            rb.velocity = new Vector2(moveDir.x * player.currentMoveSpeed, moveDir.y * player.currentMoveSpeed);
        }
    }

    public void Dash()
    {
        if (Time.time - lastDashTime >= dashBarFillTime)
        {
            player.Dash();

            player.isDashing = true;
            lastDashTime = Time.time;

            float dashDistance = 5f;
            Vector2 dashDirection = lastMovedVector.normalized;
            Vector2 targetPosition = rb.position + dashDirection * dashDistance;

            float dashTime = 0.2f;
            float elapsedTime = 0f;

            // Temporarily ignore collisions with enemies
            Collider2D[] enemyColliders = GameObject.FindGameObjectsWithTag("Enemy").Select(enemy => enemy.GetComponent<Collider2D>()).ToArray();
            foreach (Collider2D enemyCollider in enemyColliders)
            {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), enemyCollider, true);
            }

            // Set the dash bar to 0
            currentDashBarFillTime = 0.0f;
            dashBar.SetEnergy(currentDashBarFillTime);

            StartCoroutine(DashCoroutine(targetPosition, dashTime, elapsedTime));
        }
    }

    IEnumerator DashCoroutine(Vector2 targetPosition, float dashTime, float elapsedTime)
    {
        while (elapsedTime < dashTime)
        {
            float t = Mathf.Clamp01(elapsedTime / dashTime); // limit the interpolation factor to the range [0, 1]
            rb.MovePosition(Vector2.Lerp(rb.position, targetPosition, t));

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Re-enable collisions with enemies
        Collider2D[] enemyColliders = GameObject.FindGameObjectsWithTag("Enemy").Select(enemy => enemy.GetComponent<Collider2D>()).ToArray();
        foreach (Collider2D enemyCollider in enemyColliders)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), enemyCollider, false);
        }


        player.isDashing = false;
    }
}

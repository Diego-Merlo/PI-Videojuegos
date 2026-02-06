using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 6f;
    public float jumpForce = 12f;

    public int maxJumps = 2;
    private int jumpsRemaining;

    public float dashSpeed = 15f;
    public float dashDuration = 0.15f;
    public float dashCooldown = 0.5f;

    private bool isDashing;
    private float dashTime;
    private float dashCooldownTimer;

    public float wallJumpForceX = 8f;
    public float wallJumpForceY = 12f;

    private bool isOnWall;
    private int wallDirection;

    public float dropDownTime = 0.25f;
    private Collider2D playerCollider;

    private Collider2D currentOneWayPlatform;


    private Rigidbody2D rb;
    private float moveInput;

    private bool canWallJump = true;


    void Start()
    {
        jumpsRemaining = maxJumps;
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();

    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isOnWall && canWallJump)
            {
                rb.linearVelocity = new Vector2(-wallDirection * wallJumpForceX, wallJumpForceY);
                jumpsRemaining = maxJumps - 1;
                canWallJump = false;
            }
            else if (jumpsRemaining > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                jumpsRemaining--;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTimer <= 0 && !isDashing)
        {
            isDashing = true;
            dashTime = dashDuration;
            dashCooldownTimer = dashCooldown;
        }

        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && currentOneWayPlatform != null)
        {
            StartCoroutine(DropThroughPlatform(currentOneWayPlatform));
        }



    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            float dashDirection = Mathf.Sign(moveInput);
            if (dashDirection == 0) dashDirection = transform.localScale.x;

            rb.linearVelocity = new Vector2(dashDirection * dashSpeed, 0);
            dashTime -= Time.fixedDeltaTime;

            if (dashTime <= 0)
            {
                isDashing = false;
            }
        }
        else
        {
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        }

        dashCooldownTimer -= Time.fixedDeltaTime;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpsRemaining = maxJumps;
            canWallJump = true;

            if (collision.gameObject.GetComponent<PlatformEffector2D>())
            {
                currentOneWayPlatform = collision.collider;
            }
        }
    }



    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (Mathf.Abs(contact.normal.x) > 0.5f)
                {
                    isOnWall = true;
                    wallDirection = (int)Mathf.Sign(contact.normal.x);
                    return;
                }
            }
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider == currentOneWayPlatform)
        {
            currentOneWayPlatform = null;
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnWall = false;
            canWallJump = true;
        }
    }

    IEnumerator DropThroughPlatform(Collider2D platform)
    {
        if (platform == null)
            yield break;

        Physics2D.IgnoreCollision(playerCollider, platform, true);

        // Espera a que el jugador esté claramente debajo de la plataforma
        float platformBottom = platform.bounds.min.y;

        while (transform.position.y > platformBottom - 0.1f)
        {
            yield return null;
        }

        Physics2D.IgnoreCollision(playerCollider, platform, false);
    }







}

using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    public Transform[] patrolPoints;
    public float speed = 2f;
    public float chaseSpeed = 3.5f;
    public float waitTime = 1f;
    public float patrolSpeed = 2f;
    private int currentPoint = 0;
    private float waitTimer;
    public float arriveDistance = 0.2f;
    private Transform player;
    private bool chasing = false;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        waitTimer = waitTime;
    }

    void Update()
    {
        if (chasing)
            ChasePlayer();
    }

    void FixedUpdate()
    {
        if (!chasing)
            Patrol();
    }

    // ================= PATROL =================
    void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        Transform target = patrolPoints[currentPoint];

        Vector2 newPos = Vector2.MoveTowards(rb.position, target.position, patrolSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        // flip sprite
        if (target.position.x > transform.position.x)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);

        // SOLO distancia horizontal
        if (Mathf.Abs(transform.position.x - target.position.x) < arriveDistance)
        {
            waitTimer += Time.fixedDeltaTime;

            if (waitTimer >= waitTime)
            {
                currentPoint = (currentPoint + 1) % patrolPoints.Length;
                waitTimer = 0f;
            }
        }
    }

    // ================= CHASE =================
    void ChasePlayer()
    {
        float direction = Mathf.Sign(player.position.x - transform.position.x);
        rb.linearVelocity = new Vector2(direction * chaseSpeed, rb.linearVelocity.y);

        transform.localScale = new Vector3(direction, 1, 1);
    }

    // ================= DETECTION =================
    public void PlayerDetected(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.transform;
            chasing = true;
        }
    }

    public void PlayerLost(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            chasing = false;
            player = null;
        }
    }

}

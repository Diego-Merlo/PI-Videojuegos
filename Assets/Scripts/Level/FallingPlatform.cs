using UnityEngine;
using System.Collections;

public class FallingPlatform : MonoBehaviour
{
    public float timeBeforeFall = 1.0f;
    public float respawnTime = 3f;

    private Rigidbody2D rb;
    private Collider2D col;
    private SpriteRenderer sr;

    private Vector3 startPosition;
    private bool isFalling = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();

        startPosition = transform.position;

        rb.bodyType = RigidbodyType2D.Static;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isFalling && collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Fall());
        }
    }

    IEnumerator Fall()
    {
        isFalling = true;

        for (int i = 0; i < 2; i++)
        {
            sr.enabled = false;
            yield return new WaitForSeconds(0.1f);
            sr.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }


        yield return new WaitForSeconds(timeBeforeFall);

        // cae
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 1;
        col.enabled = false;
        sr.enabled = false;

        yield return new WaitForSeconds(respawnTime);

        // respawn
        rb.bodyType = RigidbodyType2D.Static;
        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0;

        transform.position = startPosition;

        col.enabled = true;
        sr.enabled = true;

        isFalling = false;
    }
}

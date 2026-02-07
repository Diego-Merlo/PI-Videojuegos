using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Vector3 respawnPoint;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position;
    }

    public void Respawn()
    {
        rb.linearVelocity = Vector2.zero;
        transform.position = respawnPoint;
    }
}

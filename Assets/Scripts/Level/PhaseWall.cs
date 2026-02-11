using UnityEngine;

public class PhaseWall : MonoBehaviour
{
    public bool existsInAlternate = true;
    private Collider2D col;
    private SpriteRenderer sr;
    private PlayerState player;

    void Start()
    {
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
        player = FindFirstObjectByType<PlayerState>();
    }

    void Update()
    {
        bool shouldExist = player.alternateState == existsInAlternate;

        col.enabled = shouldExist;
        sr.enabled = shouldExist;
    }
}

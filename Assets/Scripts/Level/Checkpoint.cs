using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool activated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (activated) return;

        if (other.CompareTag("Player"))
        {
            PlayerRespawn respawn = other.GetComponent<PlayerRespawn>();
            if (respawn != null)
            {
                respawn.respawnPoint = transform.position;
                activated = true;
                ActivateVisual();
            }
        }
    }

    void ActivateVisual()
    {
        GetComponent<SpriteRenderer>().color = Color.green;
    }
}

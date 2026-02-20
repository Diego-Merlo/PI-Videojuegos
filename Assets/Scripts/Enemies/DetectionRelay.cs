using UnityEngine;

public class DetectionRelay : MonoBehaviour
{

    private EnemyAI enemy;

    void Start()
    {
        enemy = GetComponentInParent<EnemyAI>();
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (enemy != null)
            enemy.PlayerDetected(c);
    }

    private void OnTriggerExit2D(Collider2D c)
    {
        if (enemy != null)
            enemy.PlayerLost(c);
    }

}

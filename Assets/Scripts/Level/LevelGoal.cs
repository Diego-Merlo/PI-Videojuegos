using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGoal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Nivel completado");
            // Por ahora solo reiniciamos o mostramos log
            // Luego aquí va UI o siguiente nivel
        }
    }

}

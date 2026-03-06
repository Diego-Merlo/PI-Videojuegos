using UnityEngine;

public class BackgroundFollow : MonoBehaviour
{
    private Transform cam;

    void Start()
    {
        cam = Camera.main.transform;
    }

    void LateUpdate()
    {
        // Sigue la cámara pero se queda en el mismo Z
        transform.position = new Vector3(cam.position.x, cam.position.y, transform.position.z);
    }
}
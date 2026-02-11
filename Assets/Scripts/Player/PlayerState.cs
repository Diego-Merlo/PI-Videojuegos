using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public bool alternateState = false;

    public GameObject normalWorld;
    public GameObject alternateWorld;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // tecla para cambiar
        {
            alternateState = !alternateState;
            UpdateWorld();
        }
    }

    void UpdateWorld()
    {
        normalWorld.SetActive(!alternateState);
        alternateWorld.SetActive(alternateState);
    }
}

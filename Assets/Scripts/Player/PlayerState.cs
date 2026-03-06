using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public bool alternateState = false;

    public GameObject normalWorld;
    public GameObject alternateWorld;

    // Referencias separadas para los visuales del player
    public GameObject visualStable;
    public GameObject visualCorrupt;

    void Start()
    {
        UpdateWorld();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            alternateState = !alternateState;
            UpdateWorld();
        }
    }

    void UpdateWorld()
    {
        normalWorld.SetActive(!alternateState);
        alternateWorld.SetActive(alternateState);

        // Swappea los sprites del player independientemente
        visualStable.SetActive(!alternateState);
        visualCorrupt.SetActive(alternateState);
    }
}
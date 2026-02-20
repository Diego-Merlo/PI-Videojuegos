using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    public static CollectibleManager instance;

    public int totalCollectibles = 3;
    private int collected = 0;

    public GameObject finalWall;

    void Awake()
    {
        instance = this;
    }

    public void Collect()
    {
        collected++;
        Debug.Log("Collectibles: " + collected + "/" + totalCollectibles);

        if (collected >= totalCollectibles)
        {
            OpenFinalWall();
        }
    }

    void OpenFinalWall()
    {
        Debug.Log("All collectibles collected! Wall opened.");
        finalWall.SetActive(false);
    }

}

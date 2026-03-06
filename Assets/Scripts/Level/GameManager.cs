using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int maxDeaths = 10;
    private int currentDeaths = 0;
    public GameObject startPanel;
    public TextMeshProUGUI livesText;
    public GameObject gameOverPanel;
    public GameObject winPanel;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        Time.timeScale = 0f;

        startPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);

        UpdateUI();
    }

    public void RegisterDeath()
    {
        currentDeaths++;
        UpdateUI();

        if (currentDeaths >= maxDeaths)
        {
            GameOver();
        }
    }

    void UpdateUI()
    {
        livesText.text = "Lives: " + (maxDeaths - currentDeaths);
    }

    void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f; // pausa el juego
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exit Game"); // Solo se ve en editor
    }

    public void StartGame()
    {
        startPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void WinGame()
    {
        Time.timeScale = 0f;
        winPanel.SetActive(true);
    }

}

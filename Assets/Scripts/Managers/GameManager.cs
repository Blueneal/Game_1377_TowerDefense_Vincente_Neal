using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int nextScene;
    public Health playerHealth;
    public WaveManager waveManager;
    public TowerPlaceManager towerManager;
    public Button ballistaButton;
    public Button cannonButton;
    public Button crystalButton;

    [SerializeField] private GameObject waveButton;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject levelCompleteUI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    /// <summary>
    /// Hides the Wave button and sets the tower buttons to off when the player is ready to start a wave
    /// Also makes sure the game is not frozen
    /// </summary>
    public void StartGame()
    {
        Time.timeScale = 1f;
        waveManager.StartLevel();
        waveButton.SetActive(false);
        ballistaButton.interactable = false;
        cannonButton.interactable = false;
        crystalButton.interactable = false;
    }

    /// <summary>
    /// Unhides the wave button and runs the MoneyCheck funstion
    /// </summary>
    public void EndWave()
    {
        waveButton.SetActive(true);
        MoneyCheck();
    }

    /// <summary>
    /// Turns on the gameover UI
    /// </summary>
    public void GameOver()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }

    /// <summary>
    /// Turns on the level complete UI
    /// </summary>
    public void LevelComplete()
    {
        levelCompleteUI.SetActive(true);
        Time.timeScale = 0f;
    }

    /// <summary>
    /// Restarts the current active scene
    /// </summary>
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Opens the next scene in the build set in the editor
    /// </summary>
    public void NextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nextScene);
    }

    /// <summary>
    /// Returns the player to the main menu
    /// </summary>
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// closes the game
    /// </summary>
    public void CloseGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Checks to see how much money th player has and renables the correct buttons
    /// Also checks to see which scene is active to make sure the correct towers are being renabled
    /// </summary>
    public void MoneyCheck()
    {
        if (towerManager.currentMoney >= 10)
        {
            ballistaButton.interactable = true;
            if (towerManager.currentMoney >= 20 && nextScene == 3)
            {
                cannonButton.interactable = true;
                if (towerManager.currentMoney >= 50 && nextScene == 4)
                {
                    crystalButton.interactable = true;
                }
            }
        }
    }
}

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

    public void StartGame()
    {
        Time.timeScale = 1f;
        waveManager.StartLevel();
        waveButton.SetActive(false);
        ballistaButton.interactable = false;
        cannonButton.interactable = false;
        crystalButton.interactable = false;
    }

    public void EndWave()
    {
        waveButton.SetActive(true);
        MoneyCheck();
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void LevelComplete()
    {
        levelCompleteUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nextScene);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void CloseGame()
    {
        Application.Quit();
    }

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

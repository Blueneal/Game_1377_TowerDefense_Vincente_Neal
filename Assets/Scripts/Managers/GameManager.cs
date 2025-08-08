using System;
using UnityEditor.Rendering.Universal.ShaderGUI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int nextScene;

    [SerializeField] public Health playerHealth;
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private GameObject waveButton;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject levelCompleteUI;
    [SerializeField] private Button ballistaButton;
    [SerializeField] private Button cannonButton;
    [SerializeField] private Button crystalButton;

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
        waveManager.StartLevel();
        waveButton.SetActive(false);
        ballistaButton.interactable = false;
        cannonButton.interactable = false;
        crystalButton.interactable = false;
    }

    public void EndWave()
    {
        waveButton.SetActive(true);
        ballistaButton.interactable = true;
        cannonButton.interactable = true;
        crystalButton.interactable = true;
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
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
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseUI;

    /// <summary>
    /// Waits for when the player hits the escape key to open the pause menu and pausing the game
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    /// <summary>
    /// unpauses the game when the player clicks the close button
    /// </summary>
    public void Unpause()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Takes the player back to the main menu
    /// </summary>
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Closes the game
    /// </summary>
    public void CloseGame()
    {
        Application.Quit();
    }
}

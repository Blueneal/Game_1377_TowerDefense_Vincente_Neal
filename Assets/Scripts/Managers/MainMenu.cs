using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Starts the game, loading the first level
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Closes the game
    /// </summary>
    public void CloseGame()
    {
        Application.Quit();
    }
}

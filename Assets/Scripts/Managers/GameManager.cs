using UnityEditor.Rendering.Universal.ShaderGUI;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool gameStart;

    [SerializeField] public Health playerHealth;
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private GameObject waveButton;
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

        gameStart = false;
    }

    public void StartGame()
    {
        waveManager.StartLevel();
        waveButton.SetActive(false);
        gameStart = true;
        ballistaButton.interactable = false;
        cannonButton.interactable = false;
        crystalButton.interactable = false;
    }
}

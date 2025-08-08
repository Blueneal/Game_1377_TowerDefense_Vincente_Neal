using System;
using UnityEditor.Rendering.Universal.ShaderGUI;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

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
}

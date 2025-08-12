using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using TMPro;
using Unity.VisualScripting;

/// <summary>
/// Creates a universal structure of the spawn data needed for enemies
/// </summary>
[System.Serializable]
public struct SpawnData
{
    public GameObject EnemyToSpawn;
    public float timeBeforeSpawn;
    public Transform SpawnPoint;
    public Transform EndPoint;
}

/// <summary>
/// Creates a universal structure of the needed data for waves
/// </summary>
[System.Serializable]
public struct WaveData
{
    public float TimeBeforeWave;
    public List<SpawnData> enemyData;
}

public class WaveManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private GameManager gameManager;
    private int waveNumber = 0;
    private int currentWave = 1;

    public List<WaveData> LevelWaveData;
    public int enemiesInLevel = 0;
    public int maxWaves;

    private void Start()
    {
        maxWaves = LevelWaveData.Count;
        waveText.text = "Wave: " + currentWave + "/" + LevelWaveData.Count;
    }

    /// <summary>
    /// Checks to make sure that all enemies are off the screen before callling LevelComplete in GameManager
    /// </summary>
    private void Update()
    {
        waveText.text = "Wave: " + currentWave + "/" + LevelWaveData.Count;

        if (currentWave > maxWaves)
        {
            currentWave = maxWaves;
        }

        if (waveNumber == maxWaves && enemiesInLevel == 0)
        {
            gameManager.LevelComplete();
        }
    }

    /// <summary>
    /// Starts the coroutine for spawning enemies
    /// </summary>
    public void StartLevel()
    {
        StartCoroutine(StartWave());
    }

    /// <summary>
    /// spawns every enemy in every individual wave before stopping, pausing and then continuing when the player clicks to start the next wave
    /// </summary>
    /// <returns></returns>
    IEnumerator StartWave()
    {
        foreach (SpawnData currentEnemyToSpawn in LevelWaveData[waveNumber].enemyData)
        {
            yield return new WaitForSeconds(currentEnemyToSpawn.timeBeforeSpawn);
            SpawnEnemy(currentEnemyToSpawn.EnemyToSpawn, currentEnemyToSpawn.SpawnPoint, currentEnemyToSpawn.EndPoint);
        }
        yield return new WaitForSeconds(3);
        waveNumber++;
        currentWave++;
        gameManager.EndWave();
    }

    /// <summary>
    /// Spawns the enemy with the needed information set in the enemy structure
    /// </summary>
    /// <param name="enemyPrefab"></param>
    /// <param name="spawnPoint"></param>
    /// <param name="endPoint"></param>
    public void SpawnEnemy(GameObject enemyPrefab, Transform spawnPoint, Transform endPoint)
    {
        GameObject enemyInstance = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        Enemy enemy = enemyInstance.GetComponent<Enemy>();
        enemy.Initialize(endPoint);
        enemiesInLevel++;
    }
}

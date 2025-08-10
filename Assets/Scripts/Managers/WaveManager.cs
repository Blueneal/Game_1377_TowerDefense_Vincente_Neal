using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using TMPro;
using Unity.VisualScripting;

[System.Serializable]
public struct SpawnData
{
    public GameObject EnemyToSpawn;
    public float timeBeforeSpawn;
    public Transform SpawnPoint;
    public Transform EndPoint;
}

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

    private void Start()
    {
        waveText.text = "Wave: " + currentWave + "/" + LevelWaveData.Count;
    }

    private void Update()
    {
        waveText.text = "Wave: " + currentWave + "/" + LevelWaveData.Count;

        if (currentWave == 4)
        {
            currentWave = 3;
        }

        if (waveNumber == 3)
        {
            gameManager.LevelComplete();
        }
    }

    public void StartLevel()
    {
        StartCoroutine(StartWave());
    }

    IEnumerator StartWave()
    {
        foreach (SpawnData currentEnemyToSpawn in LevelWaveData[waveNumber].enemyData)
        {
            yield return new WaitForSeconds(currentEnemyToSpawn.timeBeforeSpawn);
            SpawnEnemy(currentEnemyToSpawn.EnemyToSpawn, currentEnemyToSpawn.SpawnPoint, currentEnemyToSpawn.EndPoint);
        }
        waveNumber++;
        currentWave++;
        gameManager.EndWave();
    }

    public void SpawnEnemy(GameObject enemyPrefab, Transform spawnPoint, Transform endPoint)
    {
        GameObject enemyInstance = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        Enemy enemy = enemyInstance.GetComponent<Enemy>();
        enemy.Initialize(endPoint);
        enemiesInLevel++;
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    public List<WaveData> LevelWaveData;
    //[SerializeField] private GameObject enemyPrefab;
    //[SerializeField] private Transform spawnPoint;
    //[SerializeField] private Transform endPoint;
    //[SerializeField] private float timeBetweenSpawns = 1f;
    //[SerializeField] private float timeBetweenWaves = 5f;
    //[SerializeField] private int enemyCount = 5;
    //[SerializeField] private int waveCount = 2;
    
    void Start()
    {
        StartLevel();
    }

    void Update()
    {
        
    }

    public void StartLevel()
    {
        StartCoroutine(StartWave());
    }

    IEnumerator StartWave()
    {
        foreach (WaveData currentWave in LevelWaveData)
        {
            foreach (SpawnData currentEnemyToSpawn in currentWave.enemyData)
            {
                yield return new WaitForSeconds(currentEnemyToSpawn.timeBeforeSpawn);
                SpawnEnemy(currentEnemyToSpawn.EnemyToSpawn, currentEnemyToSpawn.SpawnPoint, currentEnemyToSpawn.EndPoint);
            }
        }
    }

    public void SpawnEnemy(GameObject enemyPrefab, Transform spawnPoint, Transform endPoint)
    {
        GameObject enemyInstance = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        Enemy enemy = enemyInstance.GetComponent<Enemy>();
        enemy.Initialize(endPoint);
    }
}

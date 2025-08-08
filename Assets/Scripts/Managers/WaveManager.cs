using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

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
    private GameManager gameManager = GameManager.Instance;

    public List<WaveData> LevelWaveData;

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

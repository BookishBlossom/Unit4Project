using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //variables
    public GameObject enemyPrefab;
    private float spawnRange = 9;
    public int enemyCount;
    public int waveNumber = 1;
    public GameObject powerupPrefab;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNumber);
        Instantiate(powerupPrefab, powerupPrefab.transform.position, powerupPrefab.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<EnemyController>().Length;
        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            Instantiate(powerupPrefab, powerupPrefab.transform.position, powerupPrefab.transform.rotation);
        }
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    private Vector2 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        Vector2 randomPos = new Vector3(spawnPosX, 6);
        return randomPos;
    }
}

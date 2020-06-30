using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public Enemy[] enemies;
        public int count;
        public float timeBetweenSpawns;
    }

    public GameObject boss;
    public GameObject healthBar;
    public Transform bossSpawnPoint;
    public Wave[] waves;
    public Transform[] spawnPoints;
    public float timeBetweenWaves;

    private Wave currentWave;
    private int currentWaveIndex;
    private Transform player;
    private bool finishedSpawning;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(StartNextWave(currentWaveIndex));
    }

    private void Update()
    {
        if (finishedSpawning && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            HandleNextWave();
        }
    }

    private void HandleNextWave()
    {
        finishedSpawning = false;
        currentWaveIndex++;

        if (currentWaveIndex < waves.Length)
        {
            StartCoroutine(StartNextWave(currentWaveIndex));
            return;
        }

        SpawnBoss();
    }

    private void SpawnBoss()
    {
        Instantiate(boss, bossSpawnPoint.position, bossSpawnPoint.rotation);
        healthBar.SetActive(true);
    }

    IEnumerator StartNextWave(int index)
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        StartCoroutine(SpawnWave(index));
    }

    IEnumerator SpawnWave(int index)
    {
        currentWave = waves[index];

        for (int i = 0; i < currentWave.count; i++)
        {
            if (player == null)
            {
                yield break;
            }

            SpawnRandomEnemy(i);

            yield return new WaitForSeconds(currentWave.timeBetweenSpawns);
        }
    }

    private void SpawnRandomEnemy(int index)
    {
        Enemy randomEnemy = currentWave.enemies[Random.Range(0, currentWave.enemies.Length)];
        Transform randomSpot = spawnPoints[Random.Range(0, spawnPoints.Length)];

        Instantiate(randomEnemy, randomSpot.position, randomSpot.rotation);

        finishedSpawning = (index == currentWave.count - 1);
    }
}

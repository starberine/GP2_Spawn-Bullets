using UnityEngine;
using System.Collections;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab of the enemy
    public MeshCollider spawnArea; // The BoxCollider representing the spawn area
    public int enemiesPerWave = 5; // Number of enemies per wave
    public float spawnInterval = 1f; // Time between each enemy spawn

    private int currentWave = 1; // Current wave number
    private int totalWaves = 3; // Total number of waves

    void Start()
    {
        StartCoroutine(SpawnWave());
    }
    private IEnumerator SpawnWave()
    {
        while (currentWave <= totalWaves)
        {
            Debug.Log("Wave " + currentWave + " starting!");

            for (int i = 0; i < enemiesPerWave; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(spawnInterval);
            }

            currentWave++;
            yield return new WaitForSeconds(5f); // Delay before the next wave
        }

        Debug.Log("All waves completed!");
    }

    private void SpawnEnemy()
    {
        // Calculate a random position within the bounds of the BoxCollider
        Vector3 randomPosition = new Vector3(
            Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
            spawnArea.bounds.center.y, // Keep enemies at the center height
            Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z)
        );

        Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
    }
}

using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Assign enemy prefab in the Inspector
    public float spawnInterval = 3.0f; // Time between spawns
    public int maxEnemies = 10; // Max number of enemies allowed
    public Transform spawnPoint; // Spawn position

    private int currentEnemyCount = 0;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (currentEnemyCount < maxEnemies)
            {
                SpawnEnemy();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

        // Get the Enemy script component
        Enemy enemyScript = newEnemy.GetComponent<Enemy>();

        if (enemyScript != null)
        {
            enemyScript.OnDeath += EnemyDied; // Subscribe to the event
        }

        currentEnemyCount++;
    }

    void EnemyDied()
    {
        currentEnemyCount--; // Decrease count when an enemy dies
    }
}

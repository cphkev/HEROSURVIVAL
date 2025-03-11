using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject rangedEnemyPrefab; 
    public GameObject meleeEnemyPrefab;  
    public Transform spawnPoint;         
    public float spawnDelay = 2f;        
    public float spawnRate = 5f;         

    private void Start()
    {
        // Start spawning enemies
        InvokeRepeating("SpawnEnemy", spawnDelay, spawnRate);
    }

    void SpawnEnemy()
    {
        
        GameObject enemyToSpawn;
        if (Random.value > 0.5f)
        {
            enemyToSpawn = rangedEnemyPrefab;
        }
        else
        {
            enemyToSpawn = meleeEnemyPrefab;
        }

        // Instantiate the selected enemy at the spawn point (portal)
        Instantiate(enemyToSpawn, spawnPoint.position, spawnPoint.rotation);
    }
}
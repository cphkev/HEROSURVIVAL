using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject rangedEnemyPrefab; 
    public GameObject meleeEnemyPrefab;  
    public Transform spawnPoint;         
    public float spawnDelay = 2f;        
    public float spawnRate = 5f;
    public int maxEnemies = 5;
    public int currentEnemies = 0;
    public GameObject portal;
    
    private void Start()
    {
        
        // Start spawning enemies
        InvokeRepeating("SpawnEnemy", spawnDelay, spawnRate);
    }

    void SpawnEnemy()
    {
        
        GameObject enemyToSpawn;
        if (Random.value > 0.65f)
        {
            enemyToSpawn = meleeEnemyPrefab;
        }
        else
        {
            enemyToSpawn = rangedEnemyPrefab;
        }

        // Instantiate the selected enemy at the spawn point (portal)
        Instantiate(enemyToSpawn, spawnPoint.position, spawnPoint.rotation);
        //Count +1 when an ememy is spawned
        currentEnemies++;
        
        // If the maximum number of enemies has been reached, stop spawning
        if (currentEnemies >= maxEnemies)
        {
            CancelInvoke("SpawnEnemy");
            
            portal.SetActive(false);
           
        }

    }
}
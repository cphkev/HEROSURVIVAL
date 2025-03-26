using System.Collections.Generic;
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
    
    private List<GameObject> spawnOrder = new List<GameObject>();
    
    
    private void Start()
    {
        portal.SetActive(false);
    }
    
    private void SpawnEnemy()
    {
        
        if (currentEnemies >= maxEnemies)
        {
            CancelInvoke("SpawnEnemy");
            
            portal.SetActive(false);
           
        }
        
        GameObject enemyToSpawn = spawnOrder[currentEnemies];
        
        // Instantiate the selected enemy at the spawn point
        Instantiate(enemyToSpawn, spawnPoint.position, spawnPoint.rotation);
        //Count +1 when an ememy is spawned
        currentEnemies++;
        
    }
    
    public void StartSpawning()
    {
        currentEnemies = 0;
        portal.SetActive(true);
        // Start spawning enemies
        MakeSpawnOrder();
        InvokeRepeating("SpawnEnemy", spawnDelay, spawnRate);
    }

    private void MakeSpawnOrder()
    {
        spawnOrder.Clear();
    
        // Add 4 melee enemies and 1 ranged enemy
        for (int i = 0; i < 4; i++) 
        {
            spawnOrder.Add(meleeEnemyPrefab);
        }
        spawnOrder.Add(rangedEnemyPrefab);
        
        // Shuffle the spawn order
        
        for (int i = spawnOrder.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            (spawnOrder[i], spawnOrder[randomIndex]) = (spawnOrder[randomIndex], spawnOrder[i]);
        }
       
    }
    
}
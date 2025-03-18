using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject rangedEnemyPrefab; 
    public GameObject meleeEnemyPrefab;  
    public Transform spawnPoint;         
    public float spawnDelay = 2f;        
    public float spawnRate = 5f;
    public int maxEnemies = 10;
    public int currentEnemies = 0;

    public GameObject portalObject;
    public Material blackMaterial;
    
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
        //Count +1 when an ememy is spawned
        currentEnemies++;
        
        // If the maximum number of enemies has been reached, stop spawning
        if (currentEnemies >= maxEnemies)
        {
            CancelInvoke("SpawnEnemy");
            Debug.Log("Max enemies reached");

            if (portalObject != null && blackMaterial != null)
            {
                
                Vector3 portalWorldPos = portalObject.transform.position;
                Quaternion portalWorldRot = portalObject.transform.rotation;
                
                GameObject closedPortal = Instantiate(portalObject, portalWorldPos, portalWorldRot);
                closedPortal.transform.position = portalWorldPos;
                
                closedPortal.transform.localScale= new Vector3(5.198751f,8.965336f,1.129626f);
                
                MeshRenderer closedPortalMeshRenderer = closedPortal.GetComponent<MeshRenderer>();
                if (closedPortalMeshRenderer != null)
                {
                    closedPortalMeshRenderer.material = blackMaterial;
                }
            }
            
           
        }

    }
}
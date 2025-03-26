using UnityEngine;

public class ManaOrbSpawner : MonoBehaviour
{
    public GameObject orbPrefab;  // Assign your blue orb prefab
    public Terrain terrain;       // Assign the terrain
    private int spawnAmount = 2;   // How many orbs to spawn
    

    void Start()
    {
        //Wait 20 seconds before spawning orbs
        Invoke("SpawnOrbs", 20f);
        
    }

    void SpawnOrbs()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Vector3 spawnPosition = GetRandomPositionOnTerrain();
            Instantiate(orbPrefab, spawnPosition, Quaternion.identity);
        }
    }

    Vector3 GetRandomPositionOnTerrain()
    {
        float terrainWidth = terrain.terrainData.size.x;
        float terrainLength = terrain.terrainData.size.z;
        float terrainX = terrain.transform.position.x;
        float terrainZ = terrain.transform.position.z;

        // Pick a random X and Z coordinate within terrain bounds
        float randomX = Random.Range(terrainX, terrainX + terrainWidth);
        float randomZ = Random.Range(terrainZ, terrainZ + terrainLength);

        // Get correct Y position using terrain height
        float terrainY = terrain.SampleHeight(new Vector3(randomX, 0, randomZ)) + terrain.transform.position.y;

        return new Vector3(randomX, terrainY + 1f, randomZ); // +1f to avoid clipping into terrain
    }

}
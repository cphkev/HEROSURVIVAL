using Scripts.Interfaces;
using System.Collections.Generic;
using Scripts.CharacterComponents;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance; // Singleton instance
    public bool ReadyToSpawn = false;
    
    void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this; // Set the instance
            DontDestroyOnLoad(gameObject); // Optional: Don't destroy the GameManager on scene changes
        }
        else
        {
            Destroy(gameObject); // Ensure there's only one GameManager in the scene
        }
    }

    void Update()
    {
        
       if(KillCounter.Instance.KillCount % 5 == 0 && ReadyToSpawn)
       {
           ReadyToSpawn = false;
           SpawnWave();
           LevelUp();
       }
       
       if (!ReadyToSpawn && KillCounter.Instance.KillCount % 5 != 0)
       {
           ReadyToSpawn = true;
       }
       
    }

    private void SpawnWave()
    {
        GameObject gate = GameObject.FindGameObjectWithTag("Gate");
        if (gate != null)
        {
            gate.GetComponent<EnemySpawner>().StartSpawning();
        }
    }

    private void LevelUp()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.GetComponent<Stats>().GainStats();
        }
    }
    
}

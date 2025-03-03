using Scripts.Interfaces;
using System.Collections.Generic;
using Scripts.CharacterComponents;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance; // Singleton instance

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    private Stats playerStats;
    private Health playerHealth;
    private Mana playerMana;
    
    private Stats enemyStats;
    private Health enemyHealth;
    private Mana enemyMana;
    

    private GameObject player;
    private GameObject enemy;
    
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

    void Start()
    {
        SpawnCharacters();
        InitializeCharacters();
        Invoke("UpdateUI", 0.5f); // Delayed UI update
    }

    private void SpawnCharacters()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");

        if (player == null && playerPrefab != null)
        {
            player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            player.tag = "Player";
            Debug.Log("Player spawned.");
        }

        if (enemy == null && enemyPrefab != null)
        {
            enemy = Instantiate(enemyPrefab, new Vector3(5, 0, 0), Quaternion.identity);
            enemy.tag = "Enemy";
            Debug.Log("Enemy spawned.");
        }
    }

    private void InitializeCharacters()
    {
        if (player != null)
        {
            playerStats = player.GetComponent<Stats>();
            playerHealth = player.GetComponent<Health>();
            playerMana = player.GetComponent<Mana>();
            
            playerStats.Initialize(55, 5, 3, 2);
            playerHealth.Initialize(200);
            playerMana.Initialize(100);
            
            Debug.Log("Player initialized.");
        }

        if (enemy != null)
        {
            enemyStats = enemy.GetComponent<Stats>();
            enemyHealth = enemy.GetComponent<Health>();
            enemyMana = enemy.GetComponent<Mana>();
            
            enemyStats.Initialize(8, 6, 2, 1);
            enemyHealth.Initialize(100);
            enemyMana.Initialize(50);
            
            Debug.Log("Enemy initialized.");
        }
    }

    private void UpdateUI()
    {
        HPMPDisplay ui = FindFirstObjectByType<HPMPDisplay>();
        if (ui != null)
        {
            Debug.Log("UI Found. Updating Stats.");
            ui.UpdateStatsDisplay();
        }
        else
        {
            Debug.LogWarning("HPDisplay not found! Make sure it exists in the scene.");
        }
    }

   
}

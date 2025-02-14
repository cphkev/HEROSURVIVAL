using Fred.Code.Interfaces;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton instance

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Stats playerStats;
    public Stats enemyStats;

    private GameObject player;
    private GameObject enemy;

    private Character playerCharacter;
    private Character enemyCharacter;

    public List<ISpell> availableSpells = new List<ISpell>();

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
            playerStats = new Stats(55, 5, 3, 2);
            playerCharacter = player.GetComponent<Character>() ?? player.AddComponent<Character>();
            playerCharacter.InitializeCharacter("Player", playerStats);

            availableSpells.Add(new Fireball());
            availableSpells.Add(new ImmolationAura());
            Debug.Log("Player initialized.");
        }

        if (enemy != null)
        {
            enemyStats = new Stats(8, 6, 2, 1);
            enemyCharacter = enemy.GetComponent<Character>() ?? enemy.AddComponent<Character>();
            enemyCharacter.InitializeCharacter("Enemy", enemyStats);
            Debug.Log("Enemy initialized.");
        }
    }

    private void UpdateUI()
    {
        HPDisplay ui = FindObjectOfType<HPDisplay>();
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

    public void PlayerAttack()
    {
        if (playerCharacter != null && enemyCharacter != null)
        {
            playerCharacter.Attack(enemyCharacter);
        }
    }

    public void EnemyAttack()
    {
        if (playerCharacter != null && enemyCharacter != null)
        {
            enemyCharacter.Attack(playerCharacter);
        }
    }
}

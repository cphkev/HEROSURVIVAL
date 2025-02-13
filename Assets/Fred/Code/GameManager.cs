using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public Stats playerStats;
    public Stats enemyStats;

    private GameObject player;
    private GameObject enemy;

    void Start()
    {
        // Try to find existing Player and Enemy, or spawn them if missing
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

        // Initialize Player
        if (player != null)
        {
            playerStats = new Stats(55, 5, 3, 2); // Example Stats
            Character playerCharacter = player.GetComponent<Character>();
            if (playerCharacter != null)
            {
                playerCharacter.InitializeCharacter("Player", playerStats);
                Debug.Log("Player initialized.");
            }

            // Add Immolation Aura to Player
            ImmolationAura aura = player.GetComponent<ImmolationAura>();
            if (aura == null)
            {
                aura = player.AddComponent<ImmolationAura>();
                Debug.Log("Immolation Aura added to Player.");
            }
            playerCharacter.ImmolationAura = aura;
        }

        // Initialize Enemy
        if (enemy != null)
        {
            enemyStats = new Stats(8, 6, 2, 1);
            Character enemyCharacter = enemy.GetComponent<Character>();
            if (enemyCharacter != null)
            {
                enemyCharacter.InitializeCharacter("Enemy", enemyStats);
                Debug.Log("Enemy initialized.");
            }
        }
    }

    // Player Attacks Enemy
    public void PlayerAttack()
    {
        if (player != null && enemy != null)
        {
            Character playerCharacter = player.GetComponent<Character>();
            Character enemyCharacter = enemy.GetComponent<Character>();
            if (playerCharacter != null && enemyCharacter != null)
            {
                playerCharacter.Attack(enemyCharacter);
            }
        }
    }

    // Enemy Attacks Player
    public void EnemyAttack()
    {
        if (player != null && enemy != null)
        {
            Character playerCharacter = player.GetComponent<Character>();
            Character enemyCharacter = enemy.GetComponent<Character>();
            if (playerCharacter != null && enemyCharacter != null)
            {
                enemyCharacter.Attack(playerCharacter);
            }
        }
    }
}
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Stats playerStats;
    public Stats enemyStats;

    private GameObject player;
    private GameObject enemy;

    void Start()
    {
        // Find Player and Enemy in the scene based on tags
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");

        if (player != null)
        {
            // Initialize player stats
            playerStats = new Stats(10, 5, 3, 2);  // Example: Strength = 10, Dexterity = 5, Intelligence = 3, Luck = 2
            Character playerCharacter = player.GetComponent<Character>();
            if (playerCharacter != null)
            {
                playerCharacter.InitializeCharacter("Player", playerStats);
                Debug.Log("Player initialized.");
            }
        }
        else
        {
            Debug.LogWarning("Player not found! Make sure the Player has the correct tag.");
        }

        if (enemy != null)
        {
            // Initialize enemy stats
            enemyStats = new Stats(8, 6, 2, 1);  // Example stats for enemy
            Character enemyCharacter = enemy.GetComponent<Character>();
            if (enemyCharacter != null)
            {
                enemyCharacter.InitializeCharacter("Enemy", enemyStats);
                Debug.Log("Enemy initialized.");
            }
        }
        else
        {
            Debug.LogWarning("Enemy not found! Make sure the Enemy has the correct tag.");
        }
    }

    // Example of player attacking the enemy
    public void PlayerAttack()
    {
        if (enemy != null)
        {
            Character enemyCharacter = enemy.GetComponent<Character>();
            if (enemyCharacter != null)
            {
                player.GetComponent<Character>().Attack(enemyCharacter);
            }
        }
    }

    // Example of enemy attacking the player
    public void EnemyAttack()
    {
        if (player != null)
        {
            Character playerCharacter = player.GetComponent<Character>();
            if (playerCharacter != null)
            {
                enemy.GetComponent<Character>().Attack(playerCharacter);
            }
        }
    }
}

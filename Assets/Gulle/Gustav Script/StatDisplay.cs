using UnityEngine;
using TMPro; // Import TextMeshPro namespace

public class StatDisplay : MonoBehaviour
{
    public TMP_Text strengthText;   // Reference to Strength UI TextMeshPro component
    public TMP_Text dexterityText;  // Reference to Dexterity UI TextMeshPro component
    public TMP_Text intelligenceText; // Reference to Intelligence UI TextMeshPro component
    public TMP_Text luckText;       // Reference to Luck UI TextMeshPro component

    private Character playerCharacter; // Reference to the player character

    void Start()
    {
        // Find the player object and get its Character component
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerCharacter = player.GetComponent<Character>();
        }
    }

    void Update()
    {
        if (playerCharacter != null)
        {
            // Update the UI TextMeshPro components with the current stats of the player
            UpdateStatsDisplay();
        }
    }

    // Method to update the UI texts
    void UpdateStatsDisplay()
    {
        if (strengthText != null)
        {
            strengthText.text = "Strength: " + playerCharacter.stats.strength.ToString();
        }
        if (dexterityText != null)
        {
            dexterityText.text = "Dexterity: " + playerCharacter.stats.dexterity.ToString();
        }
        if (intelligenceText != null)
        {
            intelligenceText.text = "Intelligence: " + playerCharacter.stats.intelligence.ToString();
        }
        if (luckText != null)
        {
            luckText.text = "Luck: " + playerCharacter.stats.luck.ToString();
        }
    }
}

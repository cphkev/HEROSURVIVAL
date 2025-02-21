using UnityEngine;
using TMPro; // Import TextMeshPro namespace

public class StatDisplay : MonoBehaviour
{
    public TMP_Text strengthText;   // Reference to Strength UI TextMeshPro component
    public TMP_Text dexterityText;  // Reference to Dexterity UI TextMeshPro component
    public TMP_Text intelligenceText; // Reference to Intelligence UI TextMeshPro component
    public TMP_Text luckText;       // Reference to Luck UI TextMeshPro component

    private Stats playerStats; // Reference to the player character

    void Start()
    {
        // Find the player object and get its Character component
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerStats = player.GetComponent<Stats>();
        }
    }

    void Update()
    {
        if (playerStats != null)
        {
            // Update the UI TextMeshPro components with the current stats of the player
            UpdateStatsDisplay();
        }
    }

    // Method to update the UI texts
    public void UpdateStatsDisplay()
    {
        if (strengthText != null)
        {
            strengthText.text = "Strength: " + playerStats.Strength.ToString();
        }
        if (dexterityText != null)
        {
            dexterityText.text = "Dexterity: " + playerStats.Dexterity.ToString();
        }
        if (intelligenceText != null)
        {
            intelligenceText.text = "Intelligence: " + playerStats.Intelligence.ToString();
        }
        if (luckText != null)
        {
            luckText.text = "Luck: " + playerStats.Luck.ToString();
        }
    }

}

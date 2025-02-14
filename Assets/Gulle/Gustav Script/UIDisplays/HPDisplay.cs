using UnityEngine;
using TMPro; // Make sure to import TextMeshPro

public class HPDisplay : MonoBehaviour
{
    public TMP_Text hpText;   // Reference to the TextMeshPro component for HP
    public TMP_Text manaText; // Reference to the TextMeshPro component for Mana

    private Character playerCharacter;

    void Start()
    {
        // Find the player object by tag
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        // If the player is found, get the Character component
        if (player != null)
        {
            playerCharacter = player.GetComponent<Character>();
            if (playerCharacter == null)
            {
                Debug.LogWarning("Player does not have a Character component!");
            }
        }
        else
        {
            Debug.LogWarning("Player not found! Make sure the Player has the correct tag.");
        }

        // Initial update of the UI
        UpdateStatsDisplay();
    }

    void Update()
    {
        // If the player character is valid, update the stats display
        if (playerCharacter != null)
        {
            UpdateStatsDisplay();
        }
    }

    // Method to update both HP and Mana UI text
    public void UpdateStatsDisplay()
    {
        if (playerCharacter != null)
        {
            if (hpText != null)
            {
                // Update the HP text
                hpText.text = "HP: " + playerCharacter.CurrentHP.ToString();
            }

            if (manaText != null)
            {
                // Update the Mana text
                manaText.text = "Mana: " + playerCharacter.CurrentMana.ToString();
            }

            // Log the values for debugging
            Debug.Log("Updating HP: " + playerCharacter.CurrentHP + " | Mana: " + playerCharacter.CurrentMana);

            // Force immediate UI update (if necessary)
            Canvas.ForceUpdateCanvases();
        }
    }
}

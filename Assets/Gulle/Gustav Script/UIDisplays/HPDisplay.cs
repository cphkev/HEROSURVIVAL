using Fred.Code.CharacterComponents;
using UnityEngine;
using TMPro; // Make sure to import TextMeshPro

public class HPDisplay : MonoBehaviour
{
    public TMP_Text hpText;   // Reference to the TextMeshPro component for HP
    public TMP_Text manaText; // Reference to the TextMeshPro component for Mana

    private Health playerHealth;
    private Mana playerMana;

    void Start()
    {
        // Find the player object by tag
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        // If the player is found, get the Character component
        if (player != null)
        {
            playerHealth = player.GetComponent<Health>();
            playerMana = player.GetComponent<Mana>();
            if (playerHealth == null)
            {
                Debug.LogWarning("Player does not have a Health component!");
            }
            if (playerMana == null)
            {
                Debug.LogWarning("Player does not have a Mana component!");
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
        if (playerHealth != null && playerMana != null)
        {
            UpdateStatsDisplay();
        }
    }

    // Method to update both HP and Mana UI text
    public void UpdateStatsDisplay()
    {
       
            if (hpText != null)
            {
                // Update the HP text
                hpText.text = "HP: " + playerHealth.CurrentHP.ToString();
            }

            if (manaText != null)
            {
                // Update the Mana text
                manaText.text = "Mana: " + playerMana.CurrentMana.ToString();
            }

            // Log the values for debugging
           // Debug.Log("Updating HP: " + playerHealth.CurrentHP + " | Mana: " + playerMana.CurrentMana);

            // Force immediate UI update (if necessary)
            Canvas.ForceUpdateCanvases();
        
    }
}

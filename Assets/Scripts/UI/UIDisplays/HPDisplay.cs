using UnityEngine;
using Scripts.CharacterComponents;
using TMPro; // Make sure to import TextMeshPro

public class HPMPDisplay : MonoBehaviour
{
    public TMP_Text hpText;   // Reference to the TextMeshPro component for HP
    public TMP_Text manaText; // Reference to the TextMeshPro component for Mana

    private Health playerHealth;
    private Mana playerMana;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            playerHealth = player.GetComponent<Health>();
            playerMana = player.GetComponent<Mana>();
            if (playerHealth == null) Debug.LogWarning("Player does not have a Health component!");
            if (playerMana == null) Debug.LogWarning("Player does not have a Mana component!");
        }
        else
        {
            Debug.LogWarning("Player not found! Make sure the Player has the correct tag.");
        }

        UpdateStatsDisplay();
    }

    void Update()
    {
        if (playerHealth != null && playerMana != null)
        {
            UpdateStatsDisplay();
        }
    }

    public void UpdateStatsDisplay()
    {
        if (hpText != null) hpText.text = playerHealth.CurrentHP.ToString();
        if (manaText != null) manaText.text = playerMana.CurrentMana.ToString();
        Canvas.ForceUpdateCanvases();
    }

    
    public int GetCurrentMana() => playerMana != null ? (int)playerMana.CurrentMana : 0;
    public int GetMaxMana() => playerMana != null ? (int)playerMana.MaxMana : 100;

    public int GetCurrentHP() => playerHealth != null ? (int)playerHealth.CurrentHP : 0;
    public int GetMaxHP() => playerHealth != null ? (int)playerHealth.MaxHP : 100;

}

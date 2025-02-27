using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using Scripts.Interfaces;
using Scripts.CharacterComponents.PlayerOnly;
using Scripts.Spells;

public class ShopManager : MonoBehaviour
{
    public GameObject player; // Reference to the player
    public GameObject shopUI; // Reference to the Shop UI
    public List<Button> ShopSlots; // List of Shop Slot buttons

    private List<ISpell> allSpells;


    private void Start()
    {
        // Ensure player is assigned
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        // Ensure shop UI is assigned
        if (shopUI == null)
        {
            Debug.LogError("ShopUI not assigned in the Inspector!");
            return;
        }

        // Get all spells from Spellbook
        allSpells = Spellbook.GetAllSpells();

        if (GameManager.Instance == null || allSpells == null || allSpells.Count == 0)
        {
            Debug.LogError("No available spells assigned to the shop!");
            return;
        }

        // Initialize shop with available spells from GameManager
        InitializeShop();
    }

    private void InitializeShop()
    {

        // Initialize buttons dynamically with spell names and images
        for (int i = 0; i < ShopSlots.Count; i++)
        {
            if (allSpells[i] != null)
            {
                ISpell spell = allSpells[i];

                // Update button text
                TextMeshProUGUI buttonText = ShopSlots[i].GetComponentInChildren<TextMeshProUGUI>();
                if (buttonText != null)
                {
                    buttonText.text = spell.SpellName;
                }

                // Update button image
                Image buttonImage = ShopSlots[i].transform.Find("Spellicon")?.GetComponent<Image>();
                if (buttonImage != null && spell.SpellIcon != null)
                {
                    buttonImage.sprite = spell.SpellIcon;
                    buttonImage.enabled = true; // Ensure it's visible
                }
                else
                {
                    Debug.LogWarning($"No Image component found in 'Spellicon' for button {i} or missing icon.");
                }
            }
            else
            {
                Debug.LogWarning($"No spell assigned for slot {i}");
            }
        }
    }

    public void BuySpell(int slotIndex)
    {

        PlayerSpells playerSpells = player.GetComponent<PlayerSpells>();

        if (allSpells[slotIndex] != null)
        {
            ISpell spellToBuy = allSpells[slotIndex];
            playerSpells.EquipSpell(spellToBuy);
            Debug.Log($"Player bought {spellToBuy.SpellName}!");
        }
        else
        {
            Debug.LogWarning($"Could not buy spell for slot {slotIndex}. Invalid slot or missing player.");
        }
    }
}
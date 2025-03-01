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
            if (i < allSpells.Count && allSpells[i] != null)
            {
                ISpell spell = allSpells[i];


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
    // Check if the spell list is valid and if the slotIndex is valid
    if (allSpells == null || allSpells.Count == 0 || slotIndex < 0 || slotIndex >= allSpells.Count)
    {
        Debug.LogWarning($"Invalid spell list or slot index: {slotIndex}. Cannot buy spell.");
        return;
    }

    // Ensure the player exists and has the PlayerSpells component
    PlayerSpells playerSpells = player?.GetComponent<PlayerSpells>();
    if (playerSpells == null)
    {
        Debug.LogWarning("Player or PlayerSpells component not found!");
        return;
    }

    // If the spell exists at the given index, equip it
    ISpell spellToBuy = allSpells[slotIndex];
    if (spellToBuy != null)
    {
        playerSpells.EquipSpell(spellToBuy, slotIndex); // Equip the spell in the right slot
        Debug.Log($"Player bought {spellToBuy.SpellName}!");
    }
    else
    {
        Debug.LogWarning($"Spell is null for slot {slotIndex}. Cannot buy spell.");
    }
}



    
}
using Fred.Code.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    public GameObject player;  // Reference to the player
    public GameObject shopUI;  // Reference to the Shop UI
    public List<Button> ShopSlots; // List of Shop Slot buttons

    private Dictionary<int, ISpell> spellInventory = new Dictionary<int, ISpell>();

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

        // Check if the availableSpells list from GameManager is assigned
        if (GameManager.Instance == null || GameManager.Instance.availableSpells == null || GameManager.Instance.availableSpells.Count == 0)
        {
            Debug.LogError("No available spells assigned to the shop!");
            return;
        }

        // Initialize shop with available spells from GameManager
        InitializeShop();
    }

    private void InitializeShop()
    {
        // Populate the spell inventory from availableSpells in GameManager
        List<ISpell> availableSpells = GameManager.Instance.availableSpells;

        for (int i = 0; i < availableSpells.Count; i++)
        {
            spellInventory[i + 1] = availableSpells[i];
        }

        // Initialize buttons dynamically with spell names and actions
        for (int i = 0; i < ShopSlots.Count; i++)
        {
            int index = i + 1; // 1-based index
            if (spellInventory.ContainsKey(index))
            {
                // Update button text
                TextMeshProUGUI buttonText = ShopSlots[i].GetComponentInChildren<TextMeshProUGUI>();
                if (buttonText != null)
                {
                    buttonText.text = spellInventory[index].SpellName;
                }
                else
                {
                    Debug.LogWarning($"Button {i} doesn't have a TextMeshProUGUI component.");
                }

                // Assign BuySpell method to button click
                ShopSlots[i].onClick.AddListener(() => BuySpell(index));
            }
            else
            {
                Debug.LogWarning($"No spell assigned for slot {index}");
            }
        }
    }

    public void BuySpell(int slotIndex)
    {
        Character playerCharacter = player.GetComponent<Character>();

        if (playerCharacter != null && spellInventory.ContainsKey(slotIndex))
        {
            ISpell spellToBuy = spellInventory[slotIndex];
            playerCharacter.LearnSpell(spellToBuy);
            Debug.Log($"Player bought {spellToBuy.SpellName}!");
        }
        else
        {
            Debug.LogWarning($"Could not buy spell for slot {slotIndex}. Invalid slot or missing player.");
        }
    }
}

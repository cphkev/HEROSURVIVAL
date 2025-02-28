using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Scripts.Interfaces;
using Scripts.CharacterComponents;
using UnityEngine.InputSystem;

namespace Scripts.CharacterComponents.PlayerOnly
{
    public class PlayerSpells : MonoBehaviour
    {
        private GameObject player;

        // List of buttons for the spell slots in the UI
        public List<Button> SpellSlots; // Make sure these buttons are assigned in Unity Inspector

        // List to hold the equipped spells
        private List<ISpell> equippedSpells = new List<ISpell>();

        private PlayerInputActions playerInputActions;

        
        

        private void Awake()
        {
            playerInputActions = new PlayerInputActions();
        }

        private void OnEnable()
        {
            playerInputActions.Player.Spell1.performed += ctx => CastSpellAtIndex(0);
            playerInputActions.Player.Spell2.performed += ctx => CastSpellAtIndex(1);
            playerInputActions.Player.Spell3.performed += ctx => CastSpellAtIndex(2);
            playerInputActions.Player.Spell4.performed += ctx => CastSpellAtIndex(3);

            playerInputActions.Enable();
        }

        private void OnDisable()
        {
            playerInputActions.Disable();
        }

        private void Start()
        {
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }

        }

        // Method to equip a spell
        public void EquipSpell(ISpell spell, int slotIndex)
{
        if (equippedSpells.Count >= 4)
    {
        Debug.LogWarning("Cannot equip more than 4 spells.");
        return;
    }
        if (equippedSpells.Count <= slotIndex)
    {
        equippedSpells.Add(spell);
    }
         else
    {
        equippedSpells[slotIndex] = spell;
    }
    Debug.Log($"Equipped {spell.SpellName} in slot {slotIndex + 1}.");
    UpdateSpellSlotUI(); 
}

        


        private void UpdateSpellSlotUI()
        {
            for (int i = 0; i < equippedSpells.Count; i++)
            {
                if (SpellSlots[i] != null && equippedSpells[i] != null)
                {
                    // Update spell icon (Image component)
                    Image spellIcon = SpellSlots[i].transform.Find("SpellIcon")?.GetComponent<Image>(); 
                    if (spellIcon != null)
                    {
                        spellIcon.sprite = equippedSpells[i].SpellIcon; // Assuming each spell has an icon
                        spellIcon.enabled = true; // Ensure the image is visible
                    }
                }
            }
        }



        // Cast the spell at the index
        private void CastSpellAtIndex(int index)
        {
            if (index >= equippedSpells.Count)
            {
                Debug.LogWarning("No spell equipped in this slot.");
                return;
            }

            CastSpell(equippedSpells[index]);
        }

        // Method to cast a spell
        private void CastSpell(ISpell spell)
        {
            if (player == null) return;

            Mana playerMana = player.GetComponent<Mana>();

            if (spell == null)
            {
                Debug.LogWarning("Spell is null.");
                return;
            }

            if (spell.CanCast(playerMana.CurrentMana))
            {
                int damage = spell.CastSpell();
                playerMana.CurrentMana -= spell.ManaCost;
                Debug.Log($"Player cast {spell.SpellName}, dealing {damage} damage.");
            }
            else
            {
                Debug.Log($"Not enough mana to cast {spell.SpellName}.");
            }
        }
    }
    
}

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Fred.Code.Interfaces;

namespace Fred.Code
{
    public class PlayerSpells : MonoBehaviour
    {
        private GameObject player; // Reference to the player
        private List<Button> SpellSlots; // List of Spell Slot buttons
        private Character playerCharacter;

        private List<ISpell> equippedSpells = new List<ISpell>(); // List of learned spells

        private void Start()
        {
            
            // Ensure player is assigned
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }
            
        }

        private void Update()
        {
            ActivateSpell();
        }

        private void ActivateSpell()
        {
            if (Input.anyKeyDown)
            {
                switch (Input.inputString)
                {
                    case "q":
                        CastSpell(equippedSpells[0]);
                        Debug.Log("Q key pressed");
                        break;
                    case "e":
                        CastSpell(equippedSpells[1]);
                        Debug.Log("E key pressed");
                        break;
                    case "r":
                        CastSpell(equippedSpells[2]);
                        Debug.Log("R key pressed");
                        break;
                    case "f":
                        CastSpell(equippedSpells[3]);
                        Debug.Log("F key pressed");
                        break;
                    case "t":
                        CastSpell(equippedSpells[4]);
                        Debug.Log("T key pressed");
                        break;
                    default:
                        // Code to execute when any other key is pressed
                        break;
                }
            }
        }

        public void EquipSpell(ISpell spell)
        {
            
            //Mangler begræning på hvor mange spells man kan have equiped
            if (!equippedSpells.Contains(spell))
            {
                equippedSpells.Add(spell);
                /*
                Image buttonImage = SpellSlots[equippedSpells.Count].transform.Find("Spellicon")
                    ?.GetComponent<Image>();
                if (buttonImage != null && spell.SpellIcon != null)
                {
                    buttonImage.sprite = spell.SpellIcon;
                    buttonImage.enabled = true;
                    Debug.Log($"{playerCharacter.CharacterName} equipped {spell.SpellName}.");
                    */
            }
            else
            {
                Debug.Log($"{playerCharacter.CharacterName} already equipped {spell.SpellName}.");
            }
        }


        // Method to cast a spell
        private void CastSpell(ISpell spell)
        {
            Character playerCharacter = player.GetComponent<Character>();
            
            if (spell != null)
            {
                
                if (spell.CanCast(playerCharacter.CurrentMana))
                {
                    int damage = spell.CastSpell();
                    playerCharacter.CurrentMana -= spell.ManaCost;
                    Debug.Log($"{playerCharacter.CharacterName} cast {spell.SpellName}, dealing {damage} damage.");
                }
                else
                {
                    Debug.Log($"Not enough mana to cast {spell.SpellName}.");
                }
            }
            else
            {
                Debug.Log($"{playerCharacter.CharacterName} has not learned {spell.SpellName}.");
            }
            
        }
        
    }
}
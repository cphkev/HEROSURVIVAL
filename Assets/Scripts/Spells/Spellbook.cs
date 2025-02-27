using Scripts.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Spells
{
    public class Spellbook : MonoBehaviour
    {
        private static List<ISpell> allSpells;
        
        private Fireball fireBall;

        private void Start()
        {
            allSpells = new List<ISpell>();
            InitializeSpells();
        }

        private void InitializeSpells()
        {

            Fireball fireball = GetComponent<Fireball>();
            if (fireball != null)
            {
                allSpells.Add(fireball);
            }
            else
            {
                Debug.LogError("Fireball prefab is missing a Fireball component!");
            }

            allSpells.Add(new ImmolationAura());
            //allSpells.Add(new Regeneration());
            Debug.Log("Spells initialized.");
        }

        public static List<ISpell> GetAllSpells()
        {
            Debug.Log("Getting all spells.");

            return allSpells;
        }



    }
}
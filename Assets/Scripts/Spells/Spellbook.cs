using Scripts.Interfaces;
using System.Collections.Generic;
using UnityEngine;

public class Spellbook: MonoBehaviour
{
    private static List<ISpell> availableSpells;
    
    private void Start()
    {
        availableSpells = new List<ISpell>();
        InitializeSpells();
    }
    
    private void InitializeSpells()
    {
        
        Fireball fireball = FindObjectOfType<Fireball>();
        if (fireball != null)
        {
            availableSpells.Add(fireball);
        }
        else
        {
            Debug.LogError("Fireball prefab is missing a Fireball component!");
        }
        
        availableSpells.Add(new ImmolationAura());
        availableSpells.Add(new Regeneration());
        Debug.Log("Spells initialized.");
    }
    
    public static List<ISpell> GetAvailableSpells()
    {
        Debug.Log("Getting available spells.");
        
        return availableSpells;
    }
    
    
    
}
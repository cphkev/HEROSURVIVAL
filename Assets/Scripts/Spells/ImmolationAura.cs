using UnityEngine;
using Scripts.Interfaces;
using System;

public class ImmolationAura : MonoBehaviour, ISpell
{
    public event Action OnCastImmolationAura;
    private string spellName => "ImmolationAura";  // The spell name
    private int manaCost => 10;
    private int spellDamage => 20;

    private Sprite spellIcon;
    
    public string SpellName => spellName;
    public int ManaCost => manaCost;
    public int SpellDamage => spellDamage;

    public Sprite SpellIcon
    {
        get
        {
            if (spellIcon == null)
            {
                // Log the exact path we're trying to load
                string path = $"Sprites/{spellName}"; 
                Debug.Log($"Attempting to load sprite from path: {path}");

                // Load sprite from Resources folder
                spellIcon = Resources.Load<Sprite>(path);

                if (spellIcon == null)
                {
                    Debug.LogWarning($"Failed to load sprite for {spellName} from path: {path}");
                }
            }
            return spellIcon;
        }
    }

    public bool CanCast(float currentMana)
    {
        return currentMana >= manaCost;
    }

    public int CastSpell()
    {
        OnCastImmolationAura?.Invoke();
        
        return spellDamage;
    }
}

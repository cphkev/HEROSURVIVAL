using UnityEngine;
using Scripts.Interfaces;
using System;

public class Fireball : MonoBehaviour, ISpell
{
    public event Action OnCastFireball;
    private string spellName => "Fireball";  // The spell name
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
                string path = $"Sprites/{spellName}";  // This will be "Sprites/Fireball"
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
        OnCastFireball?.Invoke();
        
        return spellDamage;
    }
}

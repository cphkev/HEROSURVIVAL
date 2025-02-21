using UnityEngine;
using Scripts.Interfaces;

public class Fireball : ISpell
{
    public string SpellName => "Fireball";  // The spell name
    public int ManaCost => 10;
    public int SpellDamage => 20;

    private Sprite spellIcon;

    public Sprite SpellIcon
    {
        get
        {
            if (spellIcon == null)
            {
                // Log the exact path we're trying to load
                string path = $"Sprites/{SpellName}";  // This will be "Sprites/Fireball"
                Debug.Log($"Attempting to load sprite from path: {path}");

                // Load sprite from Resources folder
                spellIcon = Resources.Load<Sprite>(path);

                if (spellIcon == null)
                {
                    Debug.LogWarning($"Failed to load sprite for {SpellName} from path: {path}");
                }
            }
            return spellIcon;
        }
    }

    public bool CanCast(float currentMana)
    {
        return currentMana >= ManaCost;
    }

    public int CastSpell()
    {
        return SpellDamage;
    }
}

using System.Collections.Generic;
using UnityEngine;
using Fred.Code.Interfaces;

public class Character : MonoBehaviour, IDamageable, IEntity
{
    private string characterName; // Name of the character (Player, Enemy, etc.)
    private Stats stats;          // The character's stats (e.g., Strength, Dexterity, etc.)
    private int currentHP;        // The character's current health
    private float currentMana;    // The character's current mana

    private List<ISpell> learnedSpells = new List<ISpell>(); // List of learned spells

    public int Strength => stats.Strength;
    public int Dexterity => stats.Dexterity;
    public int Intelligence => stats.Intelligence;
    public int Luck => stats.Luck;

    // Properties for HP and Mana
    public int CurrentHP
    {
        get => currentHP;
        set => currentHP = Mathf.Clamp(value, 0, MaxHP);
    }

    public int MaxHP => stats.MaxHP;

    public float CurrentMana
    {
        get => currentMana;
        set => currentMana = Mathf.Clamp(value, 0, MaxMana);
    }

    public float MaxMana => stats.MaxMana;

    // Constructor
    public Character(string name, Stats initialStats)
    {
        InitializeCharacter(name, initialStats);
    }

    // Initialize the character with stats
    public void InitializeCharacter(string name, Stats initialStats)
    {
        characterName = name;
        stats = initialStats;
        CurrentHP = MaxHP;
        CurrentMana = MaxMana;
    }

    // Method to take damage (from IDamageable interface)
    public void TakeDamage(int damage)
    {
        CurrentHP -= damage;

        if (CurrentHP == 0)
        {
            Die();
        }

        Debug.Log($"{characterName} took {damage} damage. Current HP: {CurrentHP}");
    }

    // Method to heal the character
    public void Heal(int amount)
    {
        CurrentHP += amount;
        Debug.Log($"{characterName} healed by {amount}. Current HP: {CurrentHP}");
    }

    // Placeholder for Die() from IDamageable interface
    public void Die()
    {
        Debug.Log($"{characterName} has died.");
    }

    // Placeholder for UpdateHPUIDisplay() from IDamageable interface
    public void UpdateHPUIDisplay()
    {
        Debug.Log($"Updating HP UI: {CurrentHP}/{MaxHP}");
    }

    // Method for performing a basic attack
    public void Attack(IDamageable target)
    {
        int damage = stats.BaseDamage;
        target.TakeDamage(damage);
        Debug.Log($"{characterName} attacks target for {damage} damage.");
    }

    // Method to learn a new spell
    public void LearnSpell(ISpell spell)
    {
        if (!learnedSpells.Contains(spell))
        {
            learnedSpells.Add(spell);
            Debug.Log($"{characterName} learned {spell.SpellName}.");
        }
        else
        {
            Debug.Log($"{characterName} already knows {spell.SpellName}.");
        }
    }

    // Method to cast a spell
    public void CastSpell(string spellName)
    {
        ISpell spell = learnedSpells.Find(s => s.SpellName == spellName);

        if (spell != null)
        {
            if (spell.CanCast(CurrentMana))
            {
                int damage = spell.CastSpell();
                CurrentMana -= spell.ManaCost;
                Debug.Log($"{characterName} cast {spellName}, dealing {damage} damage.");
            }
            else
            {
                Debug.Log($"Not enough mana to cast {spellName}.");
            }
        }
        else
        {
            Debug.Log($"{characterName} has not learned {spellName}.");
        }
    }
}

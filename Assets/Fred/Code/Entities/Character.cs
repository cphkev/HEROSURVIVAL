using Fred.Code.Interfaces;
using UnityEngine;

public class Character : MonoBehaviour, IDamageable, IEntity
{
    private string characterName;  // Name of the character (Player, Enemy, etc.)
    private Stats stats;          // The character's stats (e.g. Strength, Dexterity, etc.)
    private int currentHP;      // The character's current health
    private float currentMana;    // The character's current mana

    public int CurrentHP
    {
        get { return currentHP; }
        set { currentHP = Mathf.Clamp(value, 0, MaxHP); }
    }

    public int MaxHP
    {
        get { return stats.maxHP; }
        set { stats.maxHP = value; }
    }

    // Initialize the character with stats
    public void InitializeCharacter(string name, Stats initialStats)
    {
        characterName = name;
        stats = initialStats;
        CurrentHP = MaxHP;    // Set the starting HP
        currentMana = 100;    // Set mana to 100 initially (can be adjusted later)
    }

    // Method to take damage (from IDamageable interface)
    public void TakeDamage(int damage)
    {
        CurrentHP -= damage;

        if (CurrentHP == 0)
        {
           // Die();
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
        int damage = stats.baseDamage;
        target.TakeDamage(damage); // Apply damage to the target
        Debug.Log($"{characterName} attacks target for {damage} damage.");
    }
}

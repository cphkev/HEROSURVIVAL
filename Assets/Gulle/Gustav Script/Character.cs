using UnityEngine;

public class Character : MonoBehaviour, IDamageable
{
    public string characterName;  // Name of the character (Player, Enemy, etc.)
    public Stats stats;          // The character's stats (e.g. Strength, Dexterity, etc.)
    public float currentHP;      // The character's current health
    public float currentMana;    // The character's current mana

    // Initialize the character with stats
    public void InitializeCharacter(string name, Stats initialStats)
    {
        characterName = name;
        stats = initialStats;
        currentHP = stats.maxHP;    // Set the starting HP based on the Strength stat
        currentMana = 100;          // Set mana to 100 initially (can be adjusted later)
    }

    // Method to take damage (from IDamageable interface)
    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        if (currentHP < 0)
        {
            currentHP = 0; // Prevent health from going below zero
        }
        Debug.Log($"{characterName} took {damage} damage. Current HP: {currentHP}");
    }

    // Method to heal the character (optional, if you want healing to be part of the interface)
    public void Heal(float amount)
    {
        currentHP += amount;
        if (currentHP > stats.maxHP)
        {
            currentHP = stats.maxHP; // Cap HP at maximum
        }
        Debug.Log($"{characterName} healed by {amount}. Current HP: {currentHP}");
    }

    // Method to regenerate mana (could be called in Update)
    public void RegenerateMana()
    {
        currentMana += stats.manaRegen * Time.deltaTime; // Regenerate mana over time
        if (currentMana > 100)
        {
            currentMana = 100; // Cap mana at 100
        }
        Debug.Log($"{characterName} regenerates mana. Current Mana: {currentMana}");
    }

    // Method for performing a basic attack
    public void Attack(IDamageable target)
    {
        float damage = stats.baseDamage; // Start with base damage
        
        // Check for critical hit
        if (Random.value < stats.critChance)
        {
            damage *= 1 + stats.critDamage; // Apply critical hit damage multiplier
            Debug.Log($"{characterName} landed a critical hit!");
        }

        target.TakeDamage(damage); // Apply damage to the target
        Debug.Log($"{characterName} attacks target for {damage} damage.");
    }

    // Method to check for evading an attack
    public bool EvadeAttack()
    {
        return Random.value < stats.dodgeChance; // Chance to dodge based on Luck
    }
}

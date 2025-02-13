using UnityEngine;
[System.Serializable] // Makes it visible in the Unity Inspector
public class Stats
{
    // Base stats for the character
    public float strength;
    public float dexterity;
    public float intelligence;
    public float luck;

    // Derived stats (can be modified based on base stats)
    public float baseDamage;
    public float attackSpeed;
    public float critChance;
    public float critDamage;
    public float manaRegen;
    public float dodgeChance;
    public float maxHP;

    // Constructor to initialize stats
    public Stats(float strength, float dexterity, float intelligence, float luck)
    {
        this.strength = strength;
        this.dexterity = dexterity;
        this.intelligence = intelligence;
        this.luck = luck;

        // Derive stats based on base values
        CalculateDerivedStats();
    }

    // Method to calculate derived stats (based on base stats)
    private void CalculateDerivedStats()
    {
        // Derived stats formulas (you can adjust them to fit your game design)
        baseDamage = strength * 2;              // Base damage scales with Strength
        maxHP = strength * 10;                  // HP scales with Strength
        attackSpeed = 1 + (dexterity * 0.05f);  // Attack speed based on Dexterity
        critChance = dexterity * 0.01f;         // Crit chance based on Dexterity
        critDamage = 1.5f + (dexterity * 0.02f); // Crit damage multiplier based on Dexterity
        manaRegen = intelligence * 0.1f;        // Mana regeneration based on Intelligence
        dodgeChance = luck * 0.01f;             // Dodge chance based on Luck
    }

    // Method to update the stats (if needed during the game)
    public void UpdateStats()
    {
        // Recalculate derived stats in case base stats are updated
        CalculateDerivedStats();
    }

    // For debugging purposes
    public void PrintStats()
    {
        Debug.Log($"Strength: {strength}, Dexterity: {dexterity}, Intelligence: {intelligence}, Luck: {luck}");
        Debug.Log($"Base Damage: {baseDamage}, Attack Speed: {attackSpeed}, Crit Chance: {critChance}, Crit Damage: {critDamage}");
        Debug.Log($"Mana Regen: {manaRegen}, Dodge Chance: {dodgeChance}, Max HP: {maxHP}");
    }
}

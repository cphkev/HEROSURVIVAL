using UnityEngine;

[System.Serializable] // Makes it visible in the Unity Inspector
public class Stats
{
    // Base stats for the character
    private int strength;
    private int dexterity; 
    private int intelligence;
    private int luck;

    // Derived stats (recalculated when base stats change)
    private int baseDamage;
    private float attackSpeed;
    private float critChance;
    private float critDamage;
    private float manaRegen;
    private float dodgeChance;
    private int maxHP;
    private float maxMana;

    // Public properties for controlled access
    public int Strength
    {
        get => strength;
        set { strength = value; CalculateDerivedStats(); }
    }

    public int Dexterity
    {
        get => dexterity;
        set { dexterity = value; CalculateDerivedStats(); }
    }

    public int Intelligence
    {
        get => intelligence;
        set { intelligence = value; CalculateDerivedStats(); }
    }

    public int Luck
    {
        get => luck;
        set { luck = value; CalculateDerivedStats(); }
    }

    public int BaseDamage => baseDamage;
    public int MaxHP => maxHP;
    public float AttackSpeed => attackSpeed;
    public float CritChance => critChance;
    public float CritDamage => critDamage;
    public float ManaRegen => manaRegen;
    public float DodgeChance => dodgeChance;
    public float MaxMana => maxMana;

    // Constructor to initialize stats
    public Stats(int strength, int dexterity, int intelligence, int luck)
    {
        this.strength = strength;
        this.dexterity = dexterity;
        this.intelligence = intelligence;
        this.luck = luck;

        CalculateDerivedStats(); // Initialize derived stats
    }

    // Private method to calculate derived stats
    private void CalculateDerivedStats()
    {
        baseDamage = strength * 2;
        maxHP = strength * 10;
        attackSpeed = 1 + (dexterity * 0.05f);
        critChance = dexterity * 0.01f;
        critDamage = 1.5f + (dexterity * 0.02f);
        manaRegen = intelligence * 0.1f;
        dodgeChance = luck * 0.01f;
        maxMana = 100;
    }

    // Method to update stats dynamically
    public void UpdateStats()
    {
        CalculateDerivedStats();
    }

    // For debugging
    public void PrintStats()
    {
        Debug.Log($"Strength: {Strength}, Dexterity: {Dexterity}, Intelligence: {Intelligence}, Luck: {Luck}");
        Debug.Log($"Base Damage: {BaseDamage}, Attack Speed: {AttackSpeed}, Crit Chance: {CritChance}, Crit Damage: {CritDamage}");
        Debug.Log($"Mana Regen: {ManaRegen}, Dodge Chance: {DodgeChance}, Max HP: {MaxHP}");
    }
}

using Fred.Code.Interfaces;
using UnityEngine;

public class Character : MonoBehaviour, IDamageable, IEntity
{
    private string characterName; // Name of the character (Player, Enemy, etc.)
    private Stats stats;          // The character's stats (e.g., Strength, Dexterity, etc.)
    private int currentHP;        // The character's current health
    private float currentMana;    // The character's current mana
    public int Strength => stats.Strength;
    public int Dexterity => stats.Dexterity;
    public int Intelligence => stats.Intelligence;
    public int Luck => stats.Luck;

    private ImmolationAura immolationAura;
    private bool isAuraActive = false; // Flag to track if Immolation Aura is active

    // Properties for HP and Mana
    public int CurrentHP
    {
        get => currentHP;
        set => currentHP = Mathf.Clamp(value, 0, MaxHP);
    }

    public int MaxHP => stats.MaxHP; // Derived from Stats

    public float CurrentMana
    {
        get => currentMana;
        set => currentMana = Mathf.Clamp(value, 0, MaxMana);
    }

    public float MaxMana => stats.MaxMana; // Derived from Stats

    // Constructor to initialize the character with stats
    public Character(string name, Stats initialStats)
    {
        InitializeCharacter(name, initialStats);
    }

    // Initialize the character with stats
    public void InitializeCharacter(string name, Stats initialStats)
    {
        characterName = name;
        stats = initialStats;
        CurrentHP = MaxHP;  // Ensure HP is set correctly
        CurrentMana = MaxMana; // Ensure Mana is set correctly
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
        target.TakeDamage(damage); // Apply damage to the target
        Debug.Log($"{characterName} attacks target for {damage} damage.");
    }

 // Method to activate Immolation Aura
    public void ActivateImmolationAura(ImmolationAura aura)
    {
        immolationAura = aura;

        if (immolationAura.CanCast(CurrentMana))
        {
            immolationAura.CastSpell();  // Activate aura
            isAuraActive = true;
            Debug.Log("Immolation Aura activated.");
        }
        else
        {
            Debug.Log("Not enough mana to activate Immolation Aura.");
        }
    }

    // Method to deactivate Immolation Aura
    public void DeactivateImmolationAura()
    {
        if (isAuraActive)
        {
            immolationAura.StopAura();
            isAuraActive = false;
            Debug.Log("Immolation Aura deactivated.");
        }
    }

    // Method to update mana (e.g., regen or use)
    public void UpdateMana(float amount)
    {
        CurrentMana += amount;
        Debug.Log($"Mana updated. Current Mana: {CurrentMana}/{MaxMana}");
    }

    // Update method to continuously drain mana when aura is active
    void Update()
    {
        if (isAuraActive && CurrentMana > 0)
        {
            // Drain mana over time
            CurrentMana -= immolationAura.ManaCost * Time.deltaTime;
            if (CurrentMana <= 0)
            {
                DeactivateImmolationAura();
                Debug.Log("Out of mana. Immolation Aura deactivated.");
            }
        }
    }
}

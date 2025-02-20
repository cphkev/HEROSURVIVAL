using UnityEngine;

[System.Serializable] // Makes it visible in the Unity Inspector
public class Stats: MonoBehaviour
{
    // Base stats for the character
    private int strength;
    private int dexterity; 
    private int intelligence;
    private int luck;

    // Public properties for controlled access
    public int Strength
    {
        get => strength;
        set { strength = value;}
    }

    public int Dexterity
    {
        get => dexterity;
        set { dexterity = value; }
    }

    public int Intelligence
    {
        get => intelligence;
        set { intelligence = value; }
    }

    public int Luck
    {
        get => luck;
        set { luck = value; }
    }
    

    // Constructor to initialize stats
    public Stats(int strength, int dexterity, int intelligence, int luck)
    {
        this.strength = strength;
        this.dexterity = dexterity;
        this.intelligence = intelligence;
        this.luck = luck;
    }
    
}

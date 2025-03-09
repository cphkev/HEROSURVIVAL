using UnityEngine;

[CreateAssetMenu(fileName = "New Spell", menuName = "Spells")]
public class SpellScriptableObject : ScriptableObject
{
    public int DamageAmount = 10;
    public float ManaCost = 5f;
    public float Lifetime = 2f;
    public float Speed = 15f;
    public float SpellRadius = 0.5f;
    public float Cooldown;
    public float CastTime;

    //Status effects
    //Iconer /sprite
    //Time between casts

}

using UnityEngine;

[CreateAssetMenu(fileName = "New Spell", menuName = "Spells")]
public class SpellScriptableObject : ScriptableObject
{
    public string SpellName;
    public Sprite SpellIcon;
    public int DamageAmount = 10;
    public int HealAmount = 0;
    public float ManaCost = 5f;
    public float Lifetime = 2f;
    public float Speed = 15f;
    public float SpellRadius = 0.5f;
    public float Cooldown;
    public float CastTime;
    public AudioClip CastSound;
    public AudioClip ImpactSound;
    public GameObject ImpactEffect;
    public bool TargetSelf = false;
    public bool TargetGate = false;
    public float Force = 0;
}

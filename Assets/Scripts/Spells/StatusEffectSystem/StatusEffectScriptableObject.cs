using UnityEngine;
[CreateAssetMenu(fileName = "New Status Effect", menuName = "Status Effects")]
public class StatusEffectScriptableObject : ScriptableObject
{
    public int DOTAmount;
    public float TickSpeed;
    public float MovementPenalty;
    public float Lifetime;
    
    public GameObject EffectParticles;
}

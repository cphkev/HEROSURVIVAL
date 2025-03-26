using UnityEngine;
[CreateAssetMenu(fileName = "New Status Effect", menuName = "Status Effects")]
public class StatusEffectScriptableObject : ScriptableObject
{
    public int DOTAmount = 0;
    public int HOTAmount = 0;
    public float TickSpeed = 1;
    public float MovementPenalty = -1f;
    public float Lifetime = 1;
    
    public GameObject EffectParticles;
}

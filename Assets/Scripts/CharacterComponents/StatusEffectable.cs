using UnityEngine;
using Scripts.CharacterComponents;

public class StatusEffectable : MonoBehaviour
{
    private StatusEffectScriptableObject currentEffect;
    
    private float currentEffectTime = 0f;
    private float nextTickTime = 0f;
    
    void Update()
    {
        if (currentEffect != null)
        {
            HandleEffect();
        }
    }
    
    public void ApplyEffect(StatusEffectScriptableObject effect)
    {
        this.currentEffect=effect;
    }

    public void RemoveEffect()
    {
        this.currentEffect=null;
        currentEffectTime = 0;
        nextTickTime = 0;
    }
    
    public void HandleEffect()
    {
        if (currentEffect != null)
        {
            currentEffectTime += Time.deltaTime;
            if (currentEffectTime >= currentEffect.Lifetime)
            {
                RemoveEffect();
            }
            if (currentEffect == null) return;
            
            if(currentEffect.DOTAmount!=0 && currentEffectTime > nextTickTime)
            {
                nextTickTime += currentEffect.TickSpeed;
                gameObject.GetComponent<Health>().TakeDamage(currentEffect.DOTAmount);
            }
        }
    }
    
}

using UnityEngine;
using Scripts.CharacterComponents;
using UnityEngine.AI;


public class StatusEffectable : MonoBehaviour
{
    private StatusEffectScriptableObject currentEffect;
    
    [SerializeField] private Transform effectPoint;
    
    private float currentEffectTime = 0f;
    private float nextTickTime = 0f;
    private float originalSpeed;
    private bool slowed = false;
    private GameObject effectInstance;
    
    void Update()
    {
        if (currentEffect != null)
        {
            HandleEffect();
        }
        
        if (effectInstance != null)
        {
            effectInstance.transform.position = effectPoint.position;
        }
    }
    
    public void ApplyEffect(StatusEffectScriptableObject effect)
    {
        if(slowed) RemoveSlow();
        if(effectInstance!=null) Destroy(effectInstance);
        this.currentEffect=effect;
        this.currentEffectTime = 0f;
        this.nextTickTime = currentEffect.TickSpeed;

        SpawnEffect(currentEffect.EffectParticles, currentEffect.Lifetime);
        ApplySlow();
    }

    private void RemoveEffect()
    {
        if(slowed) RemoveSlow();
        this.currentEffect=null;
        currentEffectTime = 0;
        nextTickTime = 0;
    }
    
    private void HandleEffect()
    {
        if (currentEffect != null)
        {
            currentEffectTime += Time.deltaTime;
            if (currentEffectTime >= currentEffect.Lifetime)
            {
                RemoveEffect();
            }
            if (currentEffect == null) return;
            
            HandleDot();
            HandleHot();
            
        }
    }
    
    private void HandleDot()
    {
        if(currentEffect.DOTAmount!=0 && currentEffectTime > nextTickTime)
        {
            nextTickTime += currentEffect.TickSpeed;
            gameObject.GetComponent<Health>().TakeDamage(currentEffect.DOTAmount);
        }
    }
    
    private void HandleHot()
    {
        if(currentEffect.HOTAmount!=0 && currentEffectTime > nextTickTime)
        {
            nextTickTime += currentEffect.TickSpeed;
            gameObject.GetComponent<Health>().Heal(currentEffect.HOTAmount);
        }
    }
    
    private void ApplySlow()
    {
        if (currentEffect.MovementPenalty != -1)
        {
            NavMeshAgent agent = gameObject.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                originalSpeed = agent.speed;
                agent.speed *= currentEffect.MovementPenalty;
                slowed = true;
            }
        }
    }
    
    private void RemoveSlow()
    {
        if (currentEffect.MovementPenalty != -1)
        {
            NavMeshAgent agent = gameObject.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.speed = originalSpeed;
                slowed = false;
            }
        }
    }
    
    private void SpawnEffect(GameObject effectPrefab, float lifetime)
    {
        if (effectPrefab != null)
        {
            effectInstance = Instantiate(effectPrefab, effectPoint.position, effectPoint.rotation);
            Destroy(effectInstance, lifetime);
        }
    }
    
}

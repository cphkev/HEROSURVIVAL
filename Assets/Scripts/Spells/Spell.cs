using System;
using UnityEngine;
using Scripts.CharacterComponents;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Spell : MonoBehaviour
{
   public SpellScriptableObject SpellToCast;
   public StatusEffectScriptableObject StatusEffect;
   
   private SphereCollider myCollider;
   private Rigidbody myRigidbody;
   
   private string targetTag;
   
   public void Initialize(string target)
   {
       targetTag = target;
   }
    private void Awake()
    {
         myCollider = GetComponent<SphereCollider>();
         myCollider.isTrigger = true;
         myCollider.radius = SpellToCast.SpellRadius;
         
         myRigidbody = GetComponent<Rigidbody>();
         myRigidbody.isKinematic = true;
         
         
         Destroy(this.gameObject, SpellToCast.Lifetime);
    }
   private void Update()
   {
       if (SpellToCast.Speed > 0)
       {
           transform.Translate(Vector3.forward * (SpellToCast.Speed * Time.deltaTime));
       }
   }
   
   private void OnTriggerEnter(Collider other)
   {
       //Debug.Log("Spell collided with " + other.name);
       if (SpellToCast.TargetGate)
       {
           hitTarget(other, "Gate", 1f);
       }
       else if(targetTag == "Enemy")
       {
           hitTarget(other, "Enemy", 1f);
       }
       else if (targetTag == "Player")
       {
           hitTarget(other, "Player", 0.3f);
       }
   }

   private void hitTarget(Collider other, string target, float vol)
   {
       if (other.CompareTag(target))
       {
           SpawnEffect(SpellToCast.ImpactEffect, transform.position, Quaternion.identity, 4f);
           if(SpellToCast.ImpactSound!=null) SoundFXManager.Instance.PlaySoundFX(SpellToCast.ImpactSound, transform, vol);
           Destroy(this.gameObject);
           Health targetHealth = other.GetComponentInParent<Health>();
           targetHealth.Heal(SpellToCast.HealAmount);
           targetHealth.TakeDamage(SpellToCast.DamageAmount);
           if (StatusEffect != null)
           {
               StatusEffectable targetStatus =  other.GetComponentInParent<StatusEffectable>();
               targetStatus.ApplyEffect(StatusEffect);
           }
       }
   }
   
   private void SpawnEffect(GameObject effectPrefab, Vector3 position, Quaternion rotation, float lifetime)
   {
       if (effectPrefab != null)
       {
           GameObject effectInstance = Instantiate(effectPrefab, position, rotation);
           Destroy(effectInstance, lifetime);
       }
   }
   
   
}

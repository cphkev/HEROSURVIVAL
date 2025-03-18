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
       if (other.CompareTag("Enemy"))
       {
           
           Destroy(this.gameObject);
           Health enemyHealth = other.GetComponentInParent<Health>();
           enemyHealth.TakeDamage(SpellToCast.DamageAmount);
           if (StatusEffect != null)
           {
               StatusEffectable enemyStatus =  other.GetComponentInParent<StatusEffectable>();
               enemyStatus.ApplyEffect(StatusEffect);
           }
           SoundFXManager.Instance.PlaySoundFX(SpellToCast.ImpactSound, transform, 1f);
           

       }
   }
   
   
}

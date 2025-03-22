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
         
         Debug.Log(targetTag);
         
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
       if(targetTag == "Enemy")
       {
           hitEnemy(other);
       }
       else if (targetTag == "Player")
       {
           hitPlayer(other);
       }
   }

   private void hitEnemy(Collider other)
   {
       if (other.CompareTag("Enemy"))
       {
           SpawnEffect(SpellToCast.ImpactEffect, transform.position, Quaternion.identity, 4f);
           SoundFXManager.Instance.PlaySoundFX(SpellToCast.ImpactSound, transform, 1f);
           Destroy(this.gameObject);
           Health enemyHealth = other.GetComponentInParent<Health>();
           enemyHealth.TakeDamage(SpellToCast.DamageAmount);
           if (StatusEffect != null)
           {
               StatusEffectable enemyStatus =  other.GetComponentInParent<StatusEffectable>();
               enemyStatus.ApplyEffect(StatusEffect);
           }
       }
   }
   
   private void hitPlayer(Collider other)
   {
       if (other.CompareTag("Player"))
       {
           SpawnEffect(SpellToCast.ImpactEffect, transform.position, Quaternion.identity, 4f);
           SoundFXManager.Instance.PlaySoundFX(SpellToCast.ImpactSound, transform, 0.3f);
           Destroy(this.gameObject);
           Health enemyHealth = other.GetComponentInParent<Health>();
           enemyHealth.TakeDamage(SpellToCast.DamageAmount);
           if (StatusEffect != null)
           {
               StatusEffectable enemyStatus =  other.GetComponentInParent<StatusEffectable>();
               enemyStatus.ApplyEffect(StatusEffect);
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

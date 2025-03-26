using System;
using UnityEngine;
using Scripts.CharacterComponents;
using System.Collections;
using UnityEngine.AI;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Spell : MonoBehaviour
{
   public SpellScriptableObject SpellToCast;
   public StatusEffectScriptableObject StatusEffect;
   
   private SphereCollider myCollider;
   private Rigidbody myRigidbody;
   
   private string targetTag;
   private bool alreadyHit = false;
   
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
       if (other.CompareTag(target) && !alreadyHit)
       {
           if(SpellToCast.Force == 0) alreadyHit = true;
           SpawnEffect(SpellToCast.ImpactEffect, transform.position, Quaternion.identity, 4f);
           if (SpellToCast.ImpactSound != null) SoundFXManager.Instance.PlaySoundFX(SpellToCast.ImpactSound, transform, vol);
           

           if (SpellToCast.Force != 0)
           {
               Destroy(this.gameObject, 0.6f);
               AddForce(other);
           }else
           {
               Destroy(this.gameObject);
           }
           
           Health targetHealth = other.GetComponentInParent<Health>();
           targetHealth?.Heal(SpellToCast.HealAmount);
           targetHealth?.TakeDamage(SpellToCast.DamageAmount);

           
           if (StatusEffect != null)
           {
               StatusEffectable targetStatus = other.GetComponentInParent<StatusEffectable>();
               targetStatus?.ApplyEffect(StatusEffect);
           }

           
       }
   }

   private void AddForce(Collider other)
   {
       NavMeshAgent agent = other.GetComponentInParent<NavMeshAgent>();
       if (agent != null)
       {
           Vector3 forceDirection = (other.transform.position - transform.position).normalized;
           StartCoroutine(PushTarget(agent, forceDirection, SpellToCast.Force));
       }
   }

   private IEnumerator PushTarget(NavMeshAgent agent, Vector3 direction, float force)
   {
       int pushCount = 10; // Number of times to apply force
       float pushInterval = 0.5f / pushCount; // 10 pushes over 0.5s → 0.05s per push
       
       for (int i = 0; i < pushCount; i++)
       {
           agent.Move(direction * (force / 10));
           yield return new WaitForSeconds(pushInterval); // Wait before next push
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

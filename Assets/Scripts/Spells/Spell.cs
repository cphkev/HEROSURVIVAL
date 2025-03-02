using System;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Spell : MonoBehaviour
{
   public SpellScriptableObject SpellToCast;

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
       if (other.CompareTag("EnemyHitbox"))
       {
           
           Destroy(this.gameObject);
           /*
           Debug.Log("Projectile collided with enemy.");
           hasCollided = true;
           Destroy(gameObject);

           Health enemyHealth = other.GetComponentInParent<Health>();

           if (enemyHealth != null)
           {
               enemyHealth.TakeDamage(fireball.SpellDamage);
           }
           else
           {
               Debug.LogWarning("Enemy does not have a Health component.");
           }
           */
       }
   }
   
   
}

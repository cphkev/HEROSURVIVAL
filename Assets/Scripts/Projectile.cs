using System;
using Scripts.CharacterComponents;
using UnityEngine;

public class Projectile : MonoBehaviour
{
   [SerializeField] private float speed = 10;
   Fireball fireball = new Fireball();
   private void Awake()
   {
      
      fireball.OnCastFireball += () =>
      {
        Shoot();
      };
      
   }
    private void Shoot()
    {
        Destroy(gameObject, 3);
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(-transform.forward * this.speed, ForceMode.Impulse);
        
    }
    private bool hasCollided = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyHitbox") && !hasCollided)
        {
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
        }
    }

}

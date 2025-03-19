using UnityEngine;
using System.Collections;

public class EnemyRangedAttack : MonoBehaviour
{
    private Transform player;  
    public GameObject fireballPrefab; 
    public Transform firePoint;  
    public float attackRange = 10f;
    public float attackCooldown = 2f;
    public SpellScriptableObject enemyFireballSpellData;  // Assign the enemy fireball spell data in the inspector
    
    private bool canAttack = true;

    private void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange && canAttack)
        {
            StartCoroutine(ShootFireball());
        }
    }

    private IEnumerator ShootFireball()
    {
        canAttack = false;

        
        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);
        Spell spell = fireball.GetComponent<Spell>();

        if (spell != null)
        {
            
            spell.SpellToCast = enemyFireballSpellData;
        }

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

}
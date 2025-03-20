
using UnityEngine;
using System.Collections;

public class EnemyRangedAttack : MonoBehaviour
{
    private Transform player;
    public GameObject fireballPrefab;
    public Transform firePoint;
    public float attackRange = 20f;
    public float attackCooldown = 2f;
    public SpellScriptableObject enemyFireballSpellData; 
    private bool canAttack = true;
    private Animator animator;
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        animator = GetComponent<Animator>();
    }
    
    private void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (animator != null)
        {
            animator.SetBool("IsCasting", distanceToPlayer <= attackRange);
        }

        if (distanceToPlayer <= attackRange && canAttack)
        {
            StartCoroutine(ShootFireball());
        }
    }

    private IEnumerator ShootFireball()
    {
        canAttack = false;

        // Trigger attack animation
        if (animator != null)
        {
            animator.SetBool("IsCasting", true);
        }

        yield return new WaitForSeconds(0.5f); // Delay before spawning fireball (adjust based on animation)

        // Instantiate fireball
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
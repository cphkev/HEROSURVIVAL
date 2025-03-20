using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    public float attackRange = 5f;
    private Transform player;
    private Animator animator;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        animator = GetComponent<Animator>(); // Get Animator attached to the enemy
    }

    void Update()
    {
        CheckPlayerDistance();
    }

    void CheckPlayerDistance()
    {
        if (player == null || animator == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        // Set the "IsAttacking" parameter in the Animator
        animator.SetBool("IsAttacking", distance <= attackRange);
    }
}

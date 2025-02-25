using UnityEngine;

public class EnemyAttackState : EnemyState
{
    private float attackCooldown = 1.5f;  // Cooldown time between attacks
    private float attackTimer = 0f;  // Timer to track cooldown
    private bool isAttacking = false;  // Whether the enemy is currently attacking

    public EnemyAttackState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        attackTimer = 0f;  // Reset the attack timer when entering the attack state
        isAttacking = false;
        Debug.Log("Enemy entered Attack State");
    }

    public override void ExitState()
    {
        base.ExitState();
        isAttacking = false;  // Stop attacking when exiting the attack state
        Debug.Log("Enemy exited Attack State");
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        Debug.Log("EmemyAttackState triggered in FrameUpdate");
        // Track the cooldown timer
        attackTimer += Time.deltaTime;

        // Check if player is in range to attack
        if (Vector3.Distance(enemy.transform.position, enemy.Player.position) <= enemy.strikeDistance)
        {
            // If not attacking, trigger the attack
            if (!isAttacking && attackTimer >= attackCooldown)
            {
                isAttacking = true;
                attackTimer = 0f;  // Reset attack timer
                Debug.Log("Enemy is attacking the player!");  // Debug log when attack starts
                TriggerAttackAnimation();  // Trigger attack animation
                ApplyDamageToPlayer();  // Apply damage to player
            }
        }
        else
        {
            // Player is out of range, so go back to chasing or idle
            enemy.stateMachine.ChangeState(enemy.chaseState);
            Debug.Log("Player is out of attack range. Switching to Chase State.");
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    // Function to trigger attack animation
    private void TriggerAttackAnimation()
    {
        // This can be an Animator trigger or a custom animation logic
        // Example:
        enemy.AnimationTriggerEvent(Enemy.AnimationTriggerType.EnemyDamaged);
        Debug.Log("Attack animation triggered.");
    }

    // Function to apply damage to the player
    private void ApplyDamageToPlayer()
    {
        // Make sure player is within attack range before applying damage
        if (Vector3.Distance(enemy.transform.position, enemy.Player.position) <= enemy.strikeDistance)
        {
            // Assuming the player implements IDamageable
            IDamageable playerDamageable = enemy.Player.GetComponent<IDamageable>();
            if (playerDamageable != null)
            {
                // Since the player has no HP, it won't do any damage, but we'll log the action
                playerDamageable.Damage(10f); // Apply some damage, adjust as needed
                Debug.Log("Player has been attacked! Damage applied: 10");  // Log the damage application
            }
            else
            {
                Debug.LogWarning("Player does not implement IDamageable. No damage applied.");
            }
        }
        else
        {
            Debug.Log("Player is out of attack range. No damage applied.");
        }
    }
}

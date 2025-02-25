using UnityEngine;

public class EnemyChaseState : EnemyState
{
    private Transform player;
    private float chaseSpeed = 3f; // Adjust as needed

    public EnemyChaseState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (player == null) return;

        // Calculate direction to player
        Vector3 direction = (player.position - enemy.transform.position).normalized;

        // Move the enemy toward the player
        enemy.MoveEnemy(direction * chaseSpeed);

        // If the enemy gets close enough, transition to Attack state
        if (Vector3.Distance(enemy.transform.position, player.position) <= enemy.strikeDistance)
        {
            enemy.stateMachine.ChangeState(enemy.attackState);
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

    

}

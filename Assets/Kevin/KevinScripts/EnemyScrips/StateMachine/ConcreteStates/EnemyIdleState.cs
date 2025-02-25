using UnityEngine;

public class EnemyIdleState : EnemyState
{
    private Vector3 targetPosition;
    private Vector3 direction;
    private bool isMoving = false;

    public EnemyIdleState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        targetPosition = enemy.GetRandomPointInCircle();
        isMoving = true; // Start moving towards the target position
    }

    public override void ExitState()
    {
        base.ExitState();
        isMoving = false; // Stop moving when exiting the idle state
    }

    public override void FrameUpdate()
{
    base.FrameUpdate();

    // Only move the enemy if it's not already too close to the target position
    if (isMoving)
    {
        direction = targetPosition - enemy.transform.position;

        // Smooth movement toward target
        Vector3 movement = Vector3.MoveTowards(enemy.transform.position, targetPosition, enemy.RandomMovementSpeed * Time.deltaTime);
        enemy.MoveEnemy(movement - enemy.transform.position); // Move the enemy

        // Rotate the enemy to face the movement direction (only on the Y-axis)
        enemy.RotateEnemy(direction);

        if (Vector3.Distance(enemy.transform.position, targetPosition) < 0.1f)
        {
            // Set a new random point when the enemy is close to the target position
            targetPosition = enemy.GetRandomPointInCircle();
        }
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

using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable, IEnemyMoveable
{
    [field: SerializeField] public float MaxHealth { get; set; } = 100f;
    public float CurrentHealth { get; set; }
    public Rigidbody rb { get; set; }

    [Header("Aggro Settings")]
    [SerializeField] private float aggroRadius = 10f;
    [SerializeField] public float strikeDistance { get; set; } = 2f;

    private Transform player;
    public Transform Player { get { return player; } }

    private bool isAggro = false;

    // References to the colliders
    private SphereCollider aggroCollider;
    private SphereCollider strikeCollider;

    #region State Machine variables
    public EnemyStateMachine stateMachine { get; set; }
    public EnemyIdleState idleState { get; set; }
    public EnemyChaseState chaseState { get; set; }
    public EnemyAttackState attackState { get; set; }
    #endregion

    #region Idle Variables
    public float RandomMovementRange = 5f;
    public float RandomMovementSpeed = 2f;
    #endregion

    private void Awake()
    {
        stateMachine = new EnemyStateMachine();
        idleState = new EnemyIdleState(this, stateMachine);
        chaseState = new EnemyChaseState(this, stateMachine);
        attackState = new EnemyAttackState(this, stateMachine);
    }

    private void Start()
    {
        CurrentHealth = MaxHealth;
        rb = GetComponent<Rigidbody>();

        // Find player in scene
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Set up Aggro Sphere Collider (for detecting when player enters aggro range)
        aggroCollider = gameObject.AddComponent<SphereCollider>();
        aggroCollider.isTrigger = true;
        aggroCollider.radius = aggroRadius;

        // Set up Striking Sphere Collider (for detecting when player enters striking distance)
        strikeCollider = gameObject.AddComponent<SphereCollider>();
        strikeCollider.isTrigger = true;
        strikeCollider.radius = strikeDistance;

        // Initialize State Machine
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        stateMachine.currentState.FrameUpdate();

        if (isAggro)
        {
            float distance = Vector3.Distance(transform.position, player.position);

            if (distance <= strikeDistance)
            {
                // Switch to attack state when within striking distance
                stateMachine.ChangeState(attackState);
            }
            else
            {
                // Switch to chase state if player is still in aggro but not in strike range
                stateMachine.ChangeState(chaseState);
            }
        }
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    #region IDamageable
    public void Damage(float damageAmount)
    {
        CurrentHealth -= damageAmount;
        if (CurrentHealth <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
    #endregion

    #region IEnemyMoveable
    public void MoveEnemy(Vector3 direction)
    {
        rb.MovePosition(rb.position + direction * Time.fixedDeltaTime);
    }

    public void RotateEnemy(Vector3 direction)
    {
        direction.y = 0; // Prevent enemy from tilting up or down

        if (direction.sqrMagnitude > 0.1f);
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            rb.MoveRotation(targetRotation);
        }
    }
    #endregion

    #region Animation Triggers
    public void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        stateMachine.currentState.AnimationTriggerEvent(triggerType);
    }

    public enum AnimationTriggerType
    {
        EnemyDamaged,
        FootstepSound
    }
    #endregion

    #region Aggro System


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isAggro = false;
            stateMachine.ChangeState(idleState); // Return to idle when player leaves
            Debug.Log("Player left aggro radius");
        }
    }
    #endregion

    #region Strike System
    private void OnTriggerEnter(Collider other)
{
    // Check if the collider is the player
    if (other.CompareTag("Player"))
    {
        // If the player enters the aggro radius
        if (other.gameObject.CompareTag("Player"))
        {
            isAggro = true;
            Debug.Log("Player entered aggro radius");
        }
        
        // If the player enters the strike range
        if (other.gameObject.CompareTag("Player"))
        {
            // This will trigger the attack state when the player enters strike range
            Debug.Log("Player is within strike range!");
            stateMachine.ChangeState(attackState); // Trigger attack state immediately
        }
    }
}

    #endregion

    #region Random Movement (Now 3D)
    public Vector3 GetRandomPointInCircle()
    {
        Vector3 randomPoint = Random.insideUnitSphere * RandomMovementRange;
        randomPoint.y = transform.position.y; // Keep it on the same Y level
        return transform.position + randomPoint;
    }
    #endregion
}

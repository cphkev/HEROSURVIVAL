using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "EnemyNavigate", story: "Agent sets [Target] and navigates to [Target]", category: "Action", id: "6b1d91a38d9d6e135596e3b7da25f75d")]
public partial class EnemyNavigateAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    private NavMeshAgent _agent;
    private GameObject _self;
    
    [SerializeField] private float moveSpeed = 10f;
    protected override Status OnStart()
    {
        _self = GameObject; // Get the enemy GameObject
        if (_self == null)
        {
            Debug.LogError("Enemy GameObject not found!");
            return Status.Failure;
        }

        _agent = _self.GetComponent<NavMeshAgent>();
        if (_agent == null)
        {
       //     Debug.LogError("NavMeshAgent component missing!");
            return Status.Failure;
        }

        // Automatically find the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("No GameObject with tag 'Player' found!");
            return Status.Failure;
        }

        Target.Value = player;
        
       // _agent.speed = moveSpeed;
        
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (Target == null || Target.Value == null)
        {
            Debug.LogWarning("Target is null");
            return Status.Failure;
        }
        float distance = Vector3.Distance(_self.transform.position, Target.Value.transform.position);
        float stopDistance = _agent.stoppingDistance > 0 ? _agent.stoppingDistance : 1.5f; // Default stopping distance
//        Debug.Log($"Distance to target: {distance}, Stopping Distance: {stopDistance}");
        
        if (distance <= stopDistance)
        {
          //  Debug.Log("Enemy reached stopping distance. Transitioning to attack.");
            _agent.ResetPath();
            return Status.Success;
        }

        if (_agent.isStopped)
            _agent.isStopped = false;
        
        _agent.SetDestination(Target.Value.transform.position);
        return Status.Running;
    }

    protected override void OnEnd()
    {
        if (_agent != null)
        {
            _agent.ResetPath();
        }
    }
}
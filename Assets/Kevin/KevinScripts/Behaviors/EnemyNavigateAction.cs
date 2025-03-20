using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Unity.VisualScripting;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "EnemyNavigate", story: "Agent sets [Target] and navigates to [Target]", category: "Action", id: "6b1d91a38d9d6e135596e3b7da25f75d")]
public partial class EnemyNavigateAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    private NavMeshAgent _agent;
    private GameObject _self;
    [SerializeField] public BlackboardVariable<bool> IsRanged;
    [SerializeField] private float moveSpeed = 10f;
    
    
    protected override Status OnStart()
    {
        _self = GameObject; // Get the enemy GameObject
        if (_self == null)
        {
            return Status.Failure;
        }

        _agent = _self.GetComponent<NavMeshAgent>();
        if (_agent == null)
        {
            return Status.Failure;
        }
        
        
        if (IsRanged == null)
        {
            return Status.Failure;
        }


        bool isRanged = _self.GetComponent<EnemyRangedAttack>();
        if (isRanged == true)
        {
            IsRanged.Value = true;
        }
        else
        {
            IsRanged.Value = false;
        }
        
      
        
        // Automatically find the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            return Status.Failure;
        }
        

        Target.Value = player;
        
       // _agent.speed = moveSpeed;
       _agent.stoppingDistance = IsRanged.Value ? 15f : 3.5f;
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

        if (distance <= _agent.stoppingDistance)
        {
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
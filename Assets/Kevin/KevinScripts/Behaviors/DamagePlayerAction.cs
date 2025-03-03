using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Scripts.CharacterComponents;


[Serializable, GeneratePropertyBag]
[NodeDescription(name: "DamagePlayer", story: "Agent attacks [Target] and apply Damage", category: "Action", id: "6f7add199aa7fa8b92c2a7639950d5d6")]
public partial class DamagePlayerAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    public int damageAmount=10;
    protected override Status OnStart()
    {

        if(Target == null || Target.Value == null)
        {
            Debug.LogError("Target is null");
            return Status.Failure;
        }

        return Status.Running;
    }

    protected override Status OnUpdate()
    {

        Health playerHealth = Target.Value.GetComponent<Health>();

        if(playerHealth != null){

            Debug.Log("Player Health before damage: " + playerHealth.CurrentHP);
            //Apply Damage
            playerHealth.TakeDamage(damageAmount);
            
            Debug.Log("Player Health after damage: " + playerHealth.CurrentHP);

 return Status.Success;

        }else{

            Debug.LogError("Target does not have Health component");

            return Status.Failure;
        }

       return Status.Failure;
    }

    protected override void OnEnd()
    {
    }
}


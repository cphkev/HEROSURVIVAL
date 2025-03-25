using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IsRanged", story: "Check if the [Self] [isRanged]", category: "Conditions", id: "9a95c4037000d6887df3af00ed340a4b")]
public partial class IsRangedCondition : Condition
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<bool> IsRanged;

    public override bool IsTrue()
    {
        if(Self == null || Self.Value == null)
        {
            Debug.LogError("Self is null");
            return false;
        }

        if(IsRanged == null)
        {
            Debug.LogError("IsRanged is null");
            return false;
        }

        //Check if the Self has the EnemyRangedAttack component
        if(Self.Value.GetComponent<EnemyRangedAttack>() != null)
        {
            IsRanged.Value = true;
            return true;
        }   
        
        IsRanged.Value = false;
        return false;
        
    }

  
}

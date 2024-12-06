using GGM.Enemies;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CheckTargetInRadius", story: "[entity] check [target] in [radius]", category: "Action", id: "291b7e223cceee59b47263f9935ce494")]
public partial class CheckTargetInRadiusAction : Action
{
    [SerializeReference] public BlackboardVariable<BTEnemy> Entity;
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<float> Radius;

    protected override Status OnStart()
    {
        Target.Value = Entity.Value.CheckPlayerInRadius(Radius.Value);
        if (Target.Value != null)
        {
            return Status.Failure;
        }
        return Status.Success;
    }

}


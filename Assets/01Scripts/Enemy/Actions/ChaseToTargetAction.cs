using GGM.Entities;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ChaseToTarget", story: "[mover] chase to [target] with [renderer]", category: "Action", id: "b20c2112caee7aa244f51bd6629367e3")]
public partial class ChaseToTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<EntityMover> Mover;
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<EntityRenderer> Renderer;

    protected override Status OnUpdate()
    {
        Mover.Value.SetMovement(GetXDirection());
        CheckFlip();
        return Status.Running;
    }

    private void CheckFlip()
    {
        Renderer.Value.FlipController(GetXDirection());
    }

    private float GetXDirection()
    {
        Vector3 myPosition = Renderer.Value.transform.position;
        Vector3 targetPosition = Target.Value.position;

        return Mathf.Sign( (targetPosition - myPosition).x);
    }
}


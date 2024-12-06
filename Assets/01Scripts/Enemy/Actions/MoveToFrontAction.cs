using GGM.Entities;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MoveToFront", story: "entity move [renderer] front with [mover]", category: "Action", id: "38e0c4a1b509d496c320f6a0afbb06e2")]
public partial class MoveToFrontAction : Action
{
    [SerializeReference] public BlackboardVariable<EntityRenderer> Renderer;
    [SerializeReference] public BlackboardVariable<EntityMover> Mover;

    protected override Status OnStart()
    {
        float xDirection = Renderer.Value.FacingDirection;
        Mover.Value.SetMovement(xDirection);
        return Status.Success;
    }

}


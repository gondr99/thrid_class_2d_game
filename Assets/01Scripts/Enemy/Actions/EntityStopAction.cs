using GGM.Entities;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "EntityStop", story: "Stop with [mover]", category: "Action", id: "2b57e8ece34e2e57c1ff111ce57e249d")]
public partial class EntityStopAction : Action
{
    [SerializeReference] public BlackboardVariable<EntityMover> Mover;

    protected override Status OnStart()
    {
        Mover.Value.StopImmediately(false);
        return Status.Success;
    }

}


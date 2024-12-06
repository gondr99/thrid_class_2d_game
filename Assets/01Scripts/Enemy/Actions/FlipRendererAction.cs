using GGM.Entities;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "FlipRenderer", story: "flip [renderer]", category: "Action", id: "d8f701a9fc5b7d8b3a42015e579f905d")]
public partial class FlipRendererAction : Action
{
    [SerializeReference] public BlackboardVariable<EntityRenderer> Renderer;

    protected override Status OnStart()
    {
        Renderer.Value.Flip();
        return Status.Success;
    }

}


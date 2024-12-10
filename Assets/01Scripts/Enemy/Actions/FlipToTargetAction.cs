using GGM.Entities;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "FlipToTarget", story: "[renderer] flip to [target]", category: "Action", id: "286b500afbd954bf9c17a826fe92d708")]
public partial class FlipToTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<EntityRenderer> Renderer;
    [SerializeReference] public BlackboardVariable<Transform> Target;

    protected override Status OnStart()
    {
        Vector3 myPosition = Renderer.Value.transform.position;
        Vector3 targetPosition = Target.Value.position;

        float xDirection = Mathf.Sign( (targetPosition - myPosition).x);
        
        Renderer.Value.FlipController(xDirection);
        
        return Status.Success;
    }

    
}


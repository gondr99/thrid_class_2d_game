using GGM.Enemies;
using System;
using GGM.Entities;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "GetCompoFromEntity", story: "get compo from [entity]", category: "Action", id: "fd4313dca85a86a75b962acf9643da99")]
public partial class GetCompoFromEntityAction : Action
{
    [SerializeReference] public BlackboardVariable<BTEnemy> Entity;

    protected override Status OnStart()
    {
        var moverBlackboard = Entity.Value.GetVariable<EntityMover>("Mover");
        moverBlackboard.Value = Entity.Value.GetCompo<EntityMover>();
        Debug.Assert(moverBlackboard.Value != null, "EntityMover is null");
        
        var rendererBlackboard = Entity.Value.GetVariable<EntityRenderer>("Renderer");
        rendererBlackboard.Value = Entity.Value.GetCompo<EntityRenderer>();
        Debug.Assert(rendererBlackboard.Value != null, "EntityRenderer is null");
        
        var animatorBlackboard = Entity.Value.GetVariable<EntityAnimatorTrigger>("AnimatorTrigger");
        animatorBlackboard.Value = Entity.Value.GetCompo<EntityAnimatorTrigger>();
        Debug.Assert(animatorBlackboard.Value != null, "EntityAnimatorTrigger is null");
        
        
        return Status.Success;
    }

}


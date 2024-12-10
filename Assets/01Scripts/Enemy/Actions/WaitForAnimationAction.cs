using GGM.Entities;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "WaitForAnimation", story: "wait for [trigger] end", category: "Action", id: "858ab96b5448e563ff1bc99dbfc7cfcd")]
public partial class WaitForAnimationAction : Action
{
    [SerializeReference] public BlackboardVariable<EntityAnimatorTrigger> Trigger;

    private bool _isEndTrigger;
    protected override Status OnStart()
    {
        _isEndTrigger = false;
        Trigger.Value.OnAnimationEnd += HandleAnimationEnd;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return _isEndTrigger ? Status.Success : Status.Running;
    }

    protected override void OnEnd()
    {
        Trigger.Value.OnAnimationEnd -= HandleAnimationEnd;
    }

    private void HandleAnimationEnd()
        => _isEndTrigger = true;
}


using GGM.Enemies.Goblin;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "EnumToString", story: "[stateEnum] to [stateName]", category: "Action", id: "6c83d605420f2a932c916c0f44c3f903")]
public partial class EnumToStringAction : Action
{
    [SerializeReference] public BlackboardVariable<GoblinEnum> StateEnum;
    [SerializeReference] public BlackboardVariable<string> StateName;

    protected override Status OnStart()
    {
        StateName.Value = StateEnum.Value.ToString();
        return Status.Success;
    }

}


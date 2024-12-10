using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "NullCheck", story: "[target] is null [status]", category: "Conditions", id: "05e157d35c013726e68efeba6d5e303a")]
public partial class NullCheckCondition : Condition
{
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<bool> Status;

    public override bool IsTrue()
    {
        return Status.Value ? Target.Value == null : Target.Value != null;
    }
}

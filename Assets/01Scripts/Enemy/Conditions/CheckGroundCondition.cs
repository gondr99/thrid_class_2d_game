using GGM.Entities;
using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "CheckGround", story: "[mover] check ground is [status]", category: "Conditions", id: "0a45573b7534d8203a1410d25ac243d9")]
public partial class CheckGroundCondition : Condition
{
    [SerializeReference] public BlackboardVariable<EntityMover> Mover;
    [SerializeReference] public BlackboardVariable<bool> Status;

    public override bool IsTrue()
    {
        return Mover.Value.IsGroundDetected() == Status.Value;
    }

}
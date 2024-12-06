using GGM.Enemies.Goblin;
using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/GoblinStateChange")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "GoblinStateChange", message: "goblin [state] change", category: "Events", id: "acd2294422f65747d9723732e8f85626")]
public partial class GoblinStateChange : EventChannelBase
{
    public delegate void GoblinStateChangeEventHandler(GoblinEnum state);
    public event GoblinStateChangeEventHandler Event; 

    public void SendEventMessage(GoblinEnum state)
    {
        Event?.Invoke(state);
    }

    public override void SendEventMessage(BlackboardVariable[] messageData)
    {
        BlackboardVariable<GoblinEnum> stateBlackboardVariable = messageData[0] as BlackboardVariable<GoblinEnum>;
        var state = stateBlackboardVariable != null ? stateBlackboardVariable.Value : default(GoblinEnum);

        Event?.Invoke(state);
    }

    public override Delegate CreateEventHandler(BlackboardVariable[] vars, System.Action callback)
    {
        GoblinStateChangeEventHandler del = (state) =>
        {
            BlackboardVariable<GoblinEnum> var0 = vars[0] as BlackboardVariable<GoblinEnum>;
            if(var0 != null)
                var0.Value = state;

            callback();
        };
        return del;
    }

    public override void RegisterListener(Delegate del)
    {
        Event += del as GoblinStateChangeEventHandler;
    }

    public override void UnregisterListener(Delegate del)
    {
        Event -= del as GoblinStateChangeEventHandler;
    }
}


using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/StartPatrol")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "StartPatrol", message: "start patrol", category: "Events", id: "beb6ee7900f7aec2b86b0b2d4bf948bd")]
public partial class StartPatrol : EventChannelBase
{
    public delegate void StartPatrolEventHandler();
    public event StartPatrolEventHandler Event; 

    public void SendEventMessage()
    {
        Event?.Invoke();
    }

    public override void SendEventMessage(BlackboardVariable[] messageData)
    {
        Event?.Invoke();
    }

    public override Delegate CreateEventHandler(BlackboardVariable[] vars, System.Action callback)
    {
        StartPatrolEventHandler del = () =>
        {
            callback();
        };
        return del;
    }

    public override void RegisterListener(Delegate del)
    {
        Event += del as StartPatrolEventHandler;
    }

    public override void UnregisterListener(Delegate del)
    {
        Event -= del as StartPatrolEventHandler;
    }
}


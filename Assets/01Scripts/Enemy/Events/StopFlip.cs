using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/StopFlip")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "StopFlip", message: "stop flip", category: "Events", id: "fcbefc2e5af596223c9787f8f76389e0")]
public partial class StopFlip : EventChannelBase
{
    public delegate void StopFlipEventHandler();
    public event StopFlipEventHandler Event; 

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
        StopFlipEventHandler del = () =>
        {
            callback();
        };
        return del;
    }

    public override void RegisterListener(Delegate del)
    {
        Event += del as StopFlipEventHandler;
    }

    public override void UnregisterListener(Delegate del)
    {
        Event -= del as StopFlipEventHandler;
    }
}


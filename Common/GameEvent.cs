using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Use like this
// public enum EN { Ex }
// GameEvent<EN>.Subscribe(EN.Ex, () => { SpawnRange(null); });

public class GameEvent<T>
{
    private static readonly IDictionary<T, UnityEvent>
    Events = new Dictionary<T, UnityEvent>();

    public static void Subscribe(T eventType, UnityAction listener)
    {
        UnityEvent thisEvent;

        if (Events.TryGetValue(eventType, out thisEvent))
            thisEvent.AddListener(listener);
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            Events.Add(eventType, thisEvent);
        }
    }

    public static void UnSubscribe(T eventType, UnityAction listener)
    {
        UnityEvent thisEvent;

        if (Events.TryGetValue(eventType, out thisEvent))
            thisEvent.RemoveListener(listener);
    }

    public static void Excute(T eventType)
    {
        UnityEvent thisEvent;
//#if UNITY_EDITOR
        foreach (KeyValuePair<T, UnityEvent> data in Events)
        {
            Debug.Log(data.Key + "'s damage is " + data.Value);
        }
//#endif
        if (Events.TryGetValue(eventType, out thisEvent))
            thisEvent.Invoke();
    }
}
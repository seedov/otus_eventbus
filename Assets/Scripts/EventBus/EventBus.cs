using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public interface IEventBus
{
    void RaiseEvent<T>(T evt);
    void Subsctibe<T>(Action<T> callback);
    void Unsubsctibe<T>(Action<T> callback);
}
public class ZenjectEventBus : IEventBus
{
    private SignalBus _signalBus;

    public ZenjectEventBus(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    public void RaiseEvent<T>(T evt)
    {
        _signalBus.Fire(evt);
    }

    public void Subsctibe<T>(Action<T> callback)
    {
        _signalBus.Subscribe(callback);
    }

    public void Unsubsctibe<T>(Action<T> callback)
    {
        _signalBus.Unsubscribe(callback);
    }
}
public class EventBus : IEventBus
{
    private Dictionary<Type, List<Delegate>> _handlers = new();
    public void RaiseEvent<T>(T evt)
    {
        var type = evt.GetType();
        if (_handlers.ContainsKey(type))
        {
            foreach(var handler in _handlers[type])
            {
                var action = handler as Action<T>;
                action.Invoke(evt);
            }
        }
    }

    public void Subsctibe<T>(Action<T> callback)
    {
        var type = typeof(T);
        if (!_handlers.ContainsKey(type))
        {
            _handlers[type] = new List<Delegate>();
        }
        _handlers[type].Add(callback);
    }

    public void Unsubsctibe<T>(Action<T> callback)
    {
        var type = typeof(T);
        if (_handlers.ContainsKey(type))
        {
            _handlers[type].Remove(callback);
        }
    }
}

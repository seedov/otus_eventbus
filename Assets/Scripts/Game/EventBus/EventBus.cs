using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public interface IEventBus
{
    void Subscribe<T>(Action<T> handler);
    void Unubscribe<T>(Action<T> handler);
    void RaiseEvent<T>(T evt);
}

public class ZenjectEventBus : IEventBus
{
    private readonly SignalBus _signalBus;

    public ZenjectEventBus(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    public void RaiseEvent<T>(T evt)
    {
        _signalBus.Fire(evt);
    }

    public void Subscribe<T>(Action<T> handler)
    {
        _signalBus.Subscribe(handler);
    }

    public void Unubscribe<T>(Action<T> handler)
    {
        _signalBus.Unsubscribe(handler);
    }
}

public class EventBus : IEventBus
{
    private Dictionary<Type, List<Delegate>> _handlers = new();
    public void Subscribe<T>(Action<T> handler)
    {
        var type = typeof(T);
        if (!_handlers.ContainsKey(type))
        {
            _handlers[type] = new List<Delegate>();
        }
        _handlers[type].Add(handler);
    }

    public void Unubscribe<T>(Action<T> handler)
    {
        var type = typeof(T);
        if (_handlers.ContainsKey(type))
        {
            _handlers[type].Remove(handler);
        }
    }
    public void RaiseEvent<T>(T evt)
    {
        var type = evt.GetType();
        if (_handlers.ContainsKey(type))
        {

            var actions = _handlers[type];
            foreach (var action in actions)
            {
                var method = action as Action<T>;
                method.Invoke(evt);
            }
        }
    }
}

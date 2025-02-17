using System;
using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;
using Zenject;
public interface IEventBus
{
    void RaiseEvent<T>(T evt);
    void Subscribe<T>(Action<T> handler);
    void Unsubscribe<T>(Action<T> handler);
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

    public void Subscribe<T>(Action<T> handler)
    {
        _signalBus.Subscribe(handler);
    }

    public void Unsubscribe<T>(Action<T> handler)
    {
        _signalBus.TryUnsubscribe(handler);
    }
}
public class EventBus : IEventBus
{
    private readonly Dictionary<Type, List< Delegate>> _handlers = new();
    public void RaiseEvent<T>(T evt)
    {
        var type = typeof(T);
        foreach (var handler in _handlers[type])
        {
            var action = handler as Action<T>;
            action.Invoke(evt);
        }
    }

    public void Subscribe<T>(Action<T> handler)
    {
        var type = typeof(T);
        if(!_handlers.ContainsKey(type)) 
            _handlers[type] = new List< Delegate >();
        _handlers[type].Add(handler);
    }

    public void Unsubscribe<T>(Action<T> handler)
    {
        var type = typeof(T);
        if (_handlers.ContainsKey(type))
        {
            _handlers[type].Remove(handler);
        }
    }
}

public class ApplyDirectionHandler
{
    public readonly IEntity Player;
    public readonly Vector2Int Direction;

    public ApplyDirectionHandler(IEntity player, Vector2Int direction)
    {
        Player = player;
        Direction = direction;
    }
}

public class MoveHandler
{
    public readonly IEntity Player;
    public readonly Vector2Int Direction;

    public MoveHandler(IEntity player, Vector2Int direction)
    {
        Player = player;
        Direction = direction;
    }
}

public class AttackHandler
{
    public readonly IEntity Player;
    public readonly IEntity Target;

    public AttackHandler(IEntity player, IEntity target)
    {
        Player = player;
        Target = target;
    }
}
public class DealDamageHandler
{
    public readonly IEntity Player;
    public readonly int Damage;

    public DealDamageHandler(IEntity player, int damage)
    {
        Player = player;
        Damage = damage;    
    }
}
public class DestroyHandler
{
    public readonly IEntity Player;

    public DestroyHandler(IEntity player)
    {
        Player = player;
    }
}

public class StrengthHandler
{
    public readonly IEntity Player;
    public readonly int DeltaStrenght;

    public StrengthHandler(IEntity player, int deltaStrenght)
    {
        Player = player;
        DeltaStrenght = deltaStrenght;
    }
}


using System;
using Entities;
using UnityEditor;
using Zenject;

namespace Lessons.Lesson19_EventBus
{
    public sealed class DealDamageController : IInitializable, IDisposable
    {
        private IEventBus _eventBus;
        
        public DealDamageController(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }
        public void Dispose()
        {
            _eventBus.Unsubsctibe<DealDamageEvent>(DealDamage);
        }

        public void Initialize()
        {
            _eventBus.Subsctibe<DealDamageEvent>(DealDamage);
        }

        private void DealDamage(DealDamageEvent e)
        {
            DealDamage(e.Target, e.Strength);
        }

        private void DealDamage(IEntity entity, int damage)
        {
            if (!entity.TryGet(out HitPointsComponent hitPoints))
            {
                return;
            }
            
            hitPoints.Value -= damage;

            if (hitPoints.Value <= 0)
            {
                _eventBus.RaiseEvent(new DestroyEvent(entity));
            }
        }
    }

    public class DestroyEvent
    {
        public readonly IEntity Entity;

        public DestroyEvent(IEntity entity)
        {
            Entity = entity;
        }
    }
}
using System;
using Entities;
using Zenject;

namespace Lessons.Lesson19_EventBus
{
    public sealed class DealDamageController:IInitializable, IDisposable
    {
        private IEventBus _eventBus;
        public DealDamageController(IEventBus eventBus)
        {
            _eventBus = eventBus;
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
                _eventBus.RaiseEvent(new DestroyHandler(entity));
            }
        }

        void IDisposable.Dispose()
        {
            _eventBus.Unsubscribe<DealDamageHandler>(DealDamage);
        }

        private void DealDamage(DealDamageHandler handler)
        {
            DealDamage(handler.Player, handler.Damage);
        }

        void IInitializable.Initialize()
        {
            _eventBus.Subscribe<DealDamageHandler>(DealDamage);
        }
    }
}
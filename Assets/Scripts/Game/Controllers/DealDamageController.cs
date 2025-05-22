using System;
using Entities;

namespace Lessons.Lesson19_EventBus
{
    public sealed class DealDamageController : IDisposable
    {

        private readonly IEventBus _eventBus;

        public DealDamageController(IEventBus eventBus)
        {
            _eventBus = eventBus;
            _eventBus.Subscribe<DealDamageEvent>(ProcessDealDamageEvent);
        }
        public void Dispose()
        {
            _eventBus.Unubscribe<DealDamageEvent>(ProcessDealDamageEvent);
        }

        private void ProcessDealDamageEvent(DealDamageEvent evt)
        {
            DealDamage(evt.Entity, evt.Damage);
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
}
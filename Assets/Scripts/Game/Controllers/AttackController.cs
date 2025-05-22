using System;
using Entities;

namespace Lessons.Lesson19_EventBus
{
    public sealed class AttackController : IDisposable
    {
        private readonly IEventBus _eventBus;

        public AttackController( IEventBus eventBus)
        {
            _eventBus = eventBus;
            _eventBus.Subscribe<AttackEvent>(Attack);
        }

        public void Dispose()
        {
            _eventBus.Unubscribe<AttackEvent>(Attack);
        }

        private void Attack(AttackEvent evt)
        {
            Attack(evt.Attacker, evt.Target);
        }

        private void Attack(IEntity entity, IEntity target)
        {
            if (entity.TryGet(out StatsComponent stats))
            {
                var damage = stats.Strength;

                _eventBus.RaiseEvent(new DealDamageEvent(target, damage));
            }
        }
    }
}
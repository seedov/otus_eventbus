using System;
using Entities;
using Zenject;

namespace Lessons.Lesson19_EventBus
{
    public sealed class AttackController:IInitializable, IDisposable
    {
        private readonly IEventBus _eventBus;
        public AttackController(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void Dispose()
        {
            _eventBus.Unsubsctibe<AttackEvent>(Attack);
        }

        public void Initialize()
        {
            _eventBus.Subsctibe<AttackEvent>(Attack);
        }

        private void Attack(AttackEvent evt)
        {
            Attack(evt.Attacker, evt.Target);
        }

        private void Attack(IEntity entity, IEntity target)
        {
            if (entity.TryGet(out StatsComponent stats))
            {
                _eventBus.RaiseEvent(new DealDamageEvent(target, stats.Strength));
            }
        }
    }

    public class DealDamageEvent
    {
        public readonly IEntity Target;
        public int Strength;

        public DealDamageEvent(IEntity target, int strength)
        {
            Target = target;
            Strength = strength;
        }
    }
}
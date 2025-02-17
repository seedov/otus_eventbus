using System;
using Entities;
using Zenject;

namespace Lessons.Lesson19_EventBus
{
    public sealed class AttackController :IInitializable, IDisposable
    {
        private IEventBus _eventBus;

        public AttackController( IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void Attack(AttackHandler handler)
        {
            Attack(handler.Player, handler.Target);
        }

        private void Attack(IEntity entity, IEntity target)
        {
            if (entity.TryGet(out StatsComponent stats))
            {
                _eventBus.RaiseEvent(new DealDamageHandler(target, stats.Strength));
                _eventBus.RaiseEvent(new StrengthHandler(entity, -1));
            }
        }

        void IInitializable.Initialize()
        {
            _eventBus.Subscribe<AttackHandler>(Attack);
        }

        void IDisposable.Dispose()
        {
            _eventBus.Unsubscribe<AttackHandler>(Attack);
        }
    }
}
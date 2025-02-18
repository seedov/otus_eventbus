using System;
using Entities;
using Zenject;

namespace Lessons.Lesson19_EventBus
{
    public sealed class  PushController : IInitializable, IDisposable
    {
        private readonly LevelMap _levelMap;
        private readonly IEventBus _eventBus;

        public PushController(LevelMap levelMap, IEventBus evetBus)
        {
            _levelMap = levelMap;
            _eventBus = evetBus;
        }
        public void Dispose()
        {
            _eventBus.Unsubsctibe<AttackEvent>(Push);
        }

        public void Initialize()
        {
           _eventBus.Subsctibe<AttackEvent>(Push);
        }

        private void Push(AttackEvent e)
        {
            Push(e.Attacker, e.Target);
        }



        private void Push(IEntity player, IEntity entity) 
        { 
        
            var sourceCoordinates = player.Get<CoordinatesComponent>().Value;
            var targetCoordinates = entity.Get<CoordinatesComponent>().Value;
            var direction = targetCoordinates - sourceCoordinates;
            var targetPosition = targetCoordinates + direction;

            if (_levelMap.Entities.HasEntity(targetPosition))
            {
                _levelMap.Entities.TryGetEntity(targetPosition, out var entity2);
                _eventBus.RaiseEvent(new DealDamageEvent(entity2, 1));
                _eventBus.RaiseEvent(new DealDamageEvent(entity, 1));
                return;
            }

            _eventBus.RaiseEvent(new MoveEvent(entity, direction));
        }
    }

    public class PushEvent
    {
        public readonly IEntity Player;
        public readonly IEntity Enemy;

        public PushEvent(IEntity player, IEntity enemy)
        {
            Player = player;
            Enemy = enemy;
        }
    }
}
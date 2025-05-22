using System;
using System.Threading;
using Entities;

namespace Lessons.Lesson19_EventBus
{
    public sealed class PushController
    {
        private LevelMap _levelMap;
        private IEventBus _eventBus;

        public PushController(LevelMap levelMap, IEventBus eventBus)
        {
            _levelMap = levelMap;
            _eventBus = eventBus;
            _eventBus.Subscribe<AttackEvent>(ProcessAttackEvent);
        }

        private void ProcessAttackEvent(AttackEvent evt)
        {
            Push(evt.Attacker, evt.Target);
        }

        private void Push(IEntity pusher, IEntity target)
        {

            var sourceCoordinates = pusher.Get<CoordinatesComponent>().Value;
            var targetCoordinates = target.Get<CoordinatesComponent>().Value;
            var direction = targetCoordinates - sourceCoordinates;
            var nextTargetPosition = targetCoordinates + direction;

            if (!_levelMap.Tiles.IsWalkable(nextTargetPosition)) 
            {
                _eventBus.RaiseEvent(new DestroyEvent(target));
            }

            else if(_levelMap.Entities.TryGetEntity(nextTargetPosition, out var entity))
            {
                _eventBus.RaiseEvent(new DealDamageEvent(entity, 1));
                _eventBus.RaiseEvent(new DealDamageEvent(target, 1));
            }
            else
            {
                _eventBus.RaiseEvent(new MoveEvent(target, direction));
            }


        }
    }
}
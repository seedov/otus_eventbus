using Entities;
using UnityEngine;
using UnityEngine.UIElements;

namespace Lessons.Lesson19_EventBus
{
    public sealed class ApplyDirectionController
    {
        private readonly LevelMap _levelMap;
        private readonly IEventBus _eventBus;
        

        public ApplyDirectionController(
            LevelMap levelMap,
            IEventBus eventBus)
        {
            _levelMap = levelMap;
            _eventBus = eventBus;
        }

        public void ApplyDirection(IEntity entity, Vector2Int direction)
        {
            var coordinates = entity.Get<CoordinatesComponent>();
            var targetCoordinates = coordinates.Value + direction;

            if (_levelMap.Entities.HasEntity(targetCoordinates))
            {
                var attacker = entity;
                var target = _levelMap.Entities.GetEntity(targetCoordinates);

                _eventBus.RaiseEvent(new AttackEvent(attacker, target));

                
                return;
            }
            
            if (_levelMap.Tiles.IsWalkable(targetCoordinates))
            {
                _eventBus.RaiseEvent(new MoveEvent(entity, direction));
            }
        }
    }
}
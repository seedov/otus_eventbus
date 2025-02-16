using Entities;
using UnityEngine;

namespace Lessons.Lesson19_EventBus
{
    public sealed class MoveController
    {
        private readonly LevelMap _levelMap;

        public MoveController(LevelMap levelMap)
        {
            _levelMap = levelMap;
        }
        
        public void Move(IEntity entity, Vector2Int direction)
        {
            var coordinates = entity.Get<CoordinatesComponent>();
            var targetCoordinates = coordinates.Value + direction;
            
            _levelMap.Entities.RemoveEntity(coordinates.Value);
            _levelMap.Entities.SetEntity(targetCoordinates, entity);
            coordinates.Value = targetCoordinates;

            var position = entity.Get<PositionComponent>();
            position.Value = _levelMap.Tiles.CoordinatesToPosition(targetCoordinates);
        }
    }
}
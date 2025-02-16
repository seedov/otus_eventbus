using Entities;
using UnityEngine;

namespace Lessons.Lesson19_EventBus
{
    public sealed class ApplyDirectionController
    {
        private readonly LevelMap _levelMap;
        
        private readonly AttackController _attackController;
        private readonly MoveController _moveController;

        public ApplyDirectionController(
            LevelMap levelMap, 
            AttackController attackController, 
            MoveController moveController)
        {
            _levelMap = levelMap;

            _attackController = attackController;
            _moveController = moveController;
        }
        
        public void ApplyDirection(IEntity entity, Vector2Int direction)
        {
            var coordinates = entity.Get<CoordinatesComponent>();
            var targetCoordinates = coordinates.Value + direction;

            if (_levelMap.Entities.HasEntity(targetCoordinates))
            {
                _attackController.Attack(entity, _levelMap.Entities.GetEntity(targetCoordinates));
                return;
            }
            
            if (_levelMap.Tiles.IsWalkable(targetCoordinates))
            {
                _moveController.Move(entity, direction);
            }
        }
    }
}
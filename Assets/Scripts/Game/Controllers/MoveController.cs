using System;
using Entities;
using UnityEngine;

namespace Lessons.Lesson19_EventBus
{
    public sealed class MoveController
    {
        private readonly LevelMap _levelMap;
        private readonly IEventBus _eventBus;

        public MoveController(LevelMap levelMap, IEventBus eventBus)
        {
            _levelMap = levelMap;
            _eventBus = eventBus;
            _eventBus.Subscribe<MoveEvent>(ProcessMoveEvent);
        }

        private void ProcessMoveEvent(MoveEvent evt)
        {
            Move(evt.Entity, evt.Direction);
        }

        private void Move(IEntity entity, Vector2Int direction)
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
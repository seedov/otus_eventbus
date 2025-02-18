using System;
using Entities;
using UnityEngine;
using Zenject;

namespace Lessons.Lesson19_EventBus
{
    public sealed class MoveController : IInitializable, IDisposable
    {
        private readonly LevelMap _levelMap;
        private readonly IEventBus _eventBus;

        public MoveController(LevelMap levelMap, IEventBus eventBus)
        {
            _eventBus = eventBus;
            _levelMap = levelMap;
        }

        public void Dispose()
        {
            _eventBus.Unsubsctibe<MoveEvent>(Move);
        }

        public void Initialize()
        {
            _eventBus.Subsctibe<MoveEvent>(Move);
        }

        private void Move(MoveEvent evt)
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
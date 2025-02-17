using System;
using Entities;
using UnityEngine;
using Zenject;

namespace Lessons.Lesson19_EventBus
{
    public sealed class MoveController: IInitializable, IDisposable
    {
        private readonly LevelMap _levelMap;
        private readonly IEventBus _eventBus;
        public MoveController(LevelMap levelMap, IEventBus eventBus)
        {
            _levelMap = levelMap;
            _eventBus = eventBus;
        }

        void IDisposable.Dispose()
        {
            _eventBus.Unsubscribe<MoveHandler>(Move);
        }

        void IInitializable.Initialize()
        {
            _eventBus.Subscribe<MoveHandler>(Move);
        }

        private void Move(MoveHandler handler)
        {
            Move(handler.Player, handler.Direction);
            _eventBus.RaiseEvent(new StrengthHandler(handler.Player, 1));
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
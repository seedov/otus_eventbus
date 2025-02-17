using System;
using Entities;
using UnityEngine;
using Zenject;

namespace Lessons.Lesson19_EventBus
{
    public sealed class ApplyDirectionController:IInitializable, IDisposable
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

        void IInitializable.Initialize()
        {
            _eventBus.Subscribe<ApplyDirectionHandler>(ApplyDirection);

        }
        void IDisposable.Dispose()
        {
            _eventBus.Unsubscribe<ApplyDirectionHandler>(ApplyDirection);
        }

        private void ApplyDirection(ApplyDirectionHandler handler)
        {
            ApplyDirection(handler.Player, handler.Direction);
        }

        private void ApplyDirection(IEntity entity, Vector2Int direction)
        {
            var coordinates = entity.Get<CoordinatesComponent>();
            var targetCoordinates = coordinates.Value + direction;

            if (_levelMap.Entities.HasEntity(targetCoordinates))
            {
                _eventBus.RaiseEvent(new AttackHandler(entity, _levelMap.Entities.GetEntity(targetCoordinates)));
                return;
            }
            
            if (_levelMap.Tiles.IsWalkable(targetCoordinates))
            {
                _eventBus.RaiseEvent(new MoveHandler(entity, direction));
            }
        }


    }
}
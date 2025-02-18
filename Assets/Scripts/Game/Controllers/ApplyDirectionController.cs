using System;
using Entities;
using UnityEngine;
using Zenject;

namespace Lessons.Lesson19_EventBus
{
    public sealed class ApplyDirectionController : IInitializable, IDisposable
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

        public void Dispose()
        {
            _eventBus.Unsubsctibe<ApplyDirectionEvent>(DirectionChanged);
        }

        public void Initialize()
        {
            _eventBus.Subsctibe<ApplyDirectionEvent>(DirectionChanged);
        }

        private void DirectionChanged(ApplyDirectionEvent evt)
        {
            ApplyDirection(evt.Enity, evt.Direction);
        }

        private void ApplyDirection(IEntity entity, Vector2Int direction)
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

    public class AttackEvent
    {
        public readonly IEntity Attacker;
        public readonly IEntity Target;

        public AttackEvent(IEntity attacker, IEntity target)
        {
            Attacker = attacker;
            Target = target;
        }
    }

    public class MoveEvent
    {
        public readonly IEntity Entity;
        public readonly Vector2Int Direction;

        public MoveEvent(IEntity entity, Vector2Int direction)
        {
            Entity = entity;
            Direction = direction;
        }
    }
}
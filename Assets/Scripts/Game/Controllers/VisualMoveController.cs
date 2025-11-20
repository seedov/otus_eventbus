using Entities;
using UnityEngine;

namespace Lessons.Lesson19_EventBus
{
    public sealed class VisualMoveController
    {
        private readonly LevelMap _levelMap;
        private readonly VisualPipeline _visualPipeline;
        private readonly IEventBus _eventBus;
        public VisualMoveController(LevelMap levelMap, VisualPipeline visualPipeline, IEventBus eventBus)
        {
            _levelMap = levelMap;
            _eventBus = eventBus;
            _eventBus.Subscribe<MoveEvent>(ProcessMoveEvent);
            _visualPipeline = visualPipeline;

        }
        private void ProcessMoveEvent(MoveEvent evt)
        {
            Move(evt.Entity, evt.Direction);
        }

        private void Move(IEntity entity, Vector2Int direction)
        {
            var coordinates = entity.Get<CoordinatesComponent>().Value;
            var targetPosition = _levelMap.Tiles.CoordinatesToPosition(coordinates);

            _visualPipeline.AddTask(new MoveEntityVisualTask(entity, targetPosition));

;


        }
    }
}
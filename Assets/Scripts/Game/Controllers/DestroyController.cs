using System;
using Entities;

namespace Lessons.Lesson19_EventBus
{
    public sealed class DestroyController
    {
        private readonly LevelMap _levelMap;
        private readonly IEventBus _eventBus;

        public DestroyController(LevelMap levelMap, IEventBus eventBus)
        {
            _levelMap = levelMap;
            _eventBus = eventBus;
            _eventBus.Subscribe<DestroyEvent>(ProcessDestroyEvent);
        }

        private void ProcessDestroyEvent(DestroyEvent evt)
        {
            Destroy(evt.Entity);
        }

        private void Destroy(IEntity entity)
        {
            if (entity.TryGet(out DeathComponent deathComponent))
            {
                deathComponent.Die();
            }

            var coordinates = entity.Get<CoordinatesComponent>();
            _levelMap.Entities.RemoveEntity(coordinates.Value);
            
            if (entity.TryGet(out DestroyComponent destroyComponent))
            {
                destroyComponent.Destroy();
            }
        }
    }
}
using System;
using Entities;
using Zenject;

namespace Lessons.Lesson19_EventBus
{
    public sealed class DestroyController : IInitializable, IDisposable
    {
        private readonly LevelMap _levelMap;
        private readonly IEventBus _eventBus;

        public DestroyController(LevelMap levelMap, IEventBus eventBus)
        {
            _levelMap = levelMap;
            _eventBus = eventBus;
        }

        public void Dispose()
        {
            _eventBus.Unsubsctibe<DestroyEvent>(Destroy);
        }

        public void Initialize()
        {
            _eventBus.Subsctibe<DestroyEvent>(Destroy);
        }

        private void Destroy(DestroyEvent e)
        {
            Destroy(e.Entity);
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
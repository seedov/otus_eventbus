using System;
using Entities;
using Zenject;

namespace Lessons.Lesson19_EventBus
{
    public sealed class DestroyController: IInitializable, IDisposable
    {
        private readonly LevelMap _levelMap;
        private readonly IEventBus _eventBus;

        public DestroyController(LevelMap levelMap, IEventBus eventBus)
        {
            _levelMap = levelMap;
            _eventBus = eventBus;
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

        void IDisposable.Dispose()
        {
            _eventBus.Unsubscribe<DestroyHandler>(Destroy);
        }

        void IInitializable.Initialize()
        {
            _eventBus.Subscribe<DestroyHandler>(Destroy);
        }

        private void Destroy(DestroyHandler handler)
        {
            Destroy(handler.Player);
        }
    }
}
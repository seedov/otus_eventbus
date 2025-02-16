using Entities;

namespace Lessons.Lesson19_EventBus
{
    public sealed class DestroyController
    {
        private readonly LevelMap _levelMap;

        public DestroyController(LevelMap levelMap)
        {
            _levelMap = levelMap;
        }
        
        public void Destroy(IEntity entity)
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
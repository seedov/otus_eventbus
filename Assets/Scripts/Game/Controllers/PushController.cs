using Entities;

namespace Lessons.Lesson19_EventBus
{
    public sealed class  PushController
    {
        private readonly LevelMap _levelMap;

        public PushController(LevelMap levelMap)
        {
            _levelMap = levelMap;
        }

        private void Push(IEntity player, IEntity entity) 
        { 
        
            var sourceCoordinates = player.Get<CoordinatesComponent>().Value;
            var targetCoordinates = entity.Get<CoordinatesComponent>().Value;
            var direction = targetCoordinates - sourceCoordinates;
            var targetPosition = targetCoordinates + direction;

            if (_levelMap.Entities.HasEntity(targetPosition))
            {
                _levelMap.Entities.TryGetEntity(targetPosition, out var entity2);
                //EventBus.RaiseEvent(new DealDamageEvent(entity2, 1));
                //EventBus.RaiseEvent(new DealDamageEvent(entity, 1));
                return;
            }

            //EventBus.RaiseEvent(new MoveEvent(entity, direction));
        }
    }

    public class PushEvent
    {
        public readonly IEntity Player;
        public readonly IEntity Enemy;

        public PushEvent(IEntity player, IEntity enemy)
        {
            Player = player;
            Enemy = enemy;
        }
    }
}
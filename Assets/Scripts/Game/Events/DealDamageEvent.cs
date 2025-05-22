using Entities;

namespace Lessons.Lesson19_EventBus
{
    public class DealDamageEvent
    {
        public readonly IEntity Entity;
        public readonly int Damage;

        public DealDamageEvent(IEntity entity, int damage)
        {
            Entity = entity;
            Damage = damage;
        }
    }    
}
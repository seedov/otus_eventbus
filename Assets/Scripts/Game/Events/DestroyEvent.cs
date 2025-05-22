using Entities;

namespace Lessons.Lesson19_EventBus
{
    public class DestroyEvent
    {
        public readonly IEntity Entity;

        public DestroyEvent(IEntity entity)
        {
            Entity = entity;
        }
    }
}
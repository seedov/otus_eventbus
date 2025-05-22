using Entities;
using UnityEngine;

namespace Lessons.Lesson19_EventBus
{
    public class MoveEvent
    {
        public readonly IEntity Entity;
        public readonly Vector2Int Direction;

        public MoveEvent(IEntity entity, Vector2Int direction)
        {
            this.Entity = entity;
            this.Direction = direction;
        }
    }
}
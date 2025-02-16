using UnityEngine;

namespace Lessons.Lesson19_EventBus
{
    public sealed class TransformComponent
    {
        public Transform Value { get; }

        public TransformComponent(Transform transform)
        {
            Value = transform;
        }
    }
}
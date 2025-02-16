using Atomic.Elements;
using UnityEngine;

namespace Lessons.Lesson19_EventBus
{
    public sealed class CoordinatesComponent
    {
        public Vector2Int Value
        {
            get => _coordinates.Value;
            set => _coordinates.Value = value;
        }
        
        private readonly ReactiveVariable<Vector2Int> _coordinates;

        public CoordinatesComponent(ReactiveVariable<Vector2Int> coordinates)
        {
            _coordinates = coordinates;
        }
    }
}
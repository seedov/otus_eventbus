using System;
using Atomic.Elements;
using UnityEngine;

namespace Lessons.Lesson19_EventBus
{
    [Serializable]
    public sealed class Position
    {
        public Transform transform;
        
        public ReactiveVariable<Vector2Int> coordinates;   
    }
}
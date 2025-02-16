using System;
using Atomic.Elements;

namespace Lessons.Lesson19_EventBus
{
    [Serializable]
    public sealed class Life    
    {
        public ReactiveVariable<bool> isDead;

        public ReactiveVariable<int> hitPoints = new(1);
        public ReactiveVariable<int> maxHitPoints = new(1);
    }
}
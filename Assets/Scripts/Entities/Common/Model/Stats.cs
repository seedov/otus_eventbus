using System;
using Atomic.Elements;

namespace Lessons.Lesson19_EventBus
{
    [Serializable]
    public sealed class Stats
    {
        public ReactiveVariable<int> strength = new(1);
    }
}
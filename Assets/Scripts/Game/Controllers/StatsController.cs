using System;
using Zenject;

namespace Lessons.Lesson19_EventBus
{
    public sealed class StatsController : IInitializable, IDisposable
    {
        private readonly LevelMap _levelMap;
        private readonly IEventBus _eventBus;
        public StatsController(LevelMap levelMap, IEventBus eventBus)
        {
            _levelMap = levelMap;
            _eventBus = eventBus;
        }

        void IDisposable.Dispose()
        {
            _eventBus.Unsubscribe<StrengthHandler>(ApplyStrength);
        }

        private void ApplyStrength(StrengthHandler handler)
        {
            var stats = handler.Player.Get<StatsComponent>();
            stats.Strength += handler.DeltaStrenght;
        }

        void IInitializable.Initialize()
        {
            _eventBus.Subscribe<StrengthHandler>(ApplyStrength);
        }
    }
}
namespace Lessons.Lesson19_EventBus
{
    public sealed class StatsComponent
    {
        public int Strength {get => _stats.strength.Value; set { _stats.strength.Value = value; } }
        
        private readonly Stats _stats;
        
        public StatsComponent(Stats stats)
        {
            _stats = stats;
        }
    }
}
using Declarative;

namespace Lessons.Lesson19_EventBus
{
    public sealed class EnemyModel : DeclarativeModel
    {
        [Section]
        public Position position;

        [Section]
        public Life life;
    }
}
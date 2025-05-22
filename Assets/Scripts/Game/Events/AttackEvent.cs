using Entities;

namespace Lessons.Lesson19_EventBus
{

    public class AttackEvent
    {
        public readonly IEntity Attacker;
        public readonly IEntity Target;

        public AttackEvent(IEntity attacker, IEntity target)
        {
            this.Attacker = attacker;
            this.Target = target;
        }
    }
}
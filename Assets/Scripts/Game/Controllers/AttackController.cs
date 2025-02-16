using Entities;

namespace Lessons.Lesson19_EventBus
{
    public sealed class AttackController
    {
        private readonly DealDamageController _dealDamageController;

        public AttackController(DealDamageController dealDamageController)
        {
            _dealDamageController = dealDamageController;
        }
        
        public void Attack(IEntity entity, IEntity target)
        {
            if (entity.TryGet(out StatsComponent stats))
            {
                _dealDamageController.DealDamage(target, stats.Strength);
            }
        }
    }
}
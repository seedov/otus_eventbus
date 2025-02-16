using Entities;

namespace Lessons.Lesson19_EventBus
{
    public sealed class DealDamageController
    {
        private readonly DestroyController _destroyController;
        
        public DealDamageController(DestroyController destroyController)
        {
            _destroyController = destroyController;
        }
        
        public void DealDamage(IEntity entity, int damage)
        {
            if (!entity.TryGet(out HitPointsComponent hitPoints))
            {
                return;
            }
            
            hitPoints.Value -= damage;

            if (hitPoints.Value <= 0)
            {
                _destroyController.Destroy(entity);
            }
        }
    }
}
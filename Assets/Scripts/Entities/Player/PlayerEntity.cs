using Entities;
using UnityEngine;

namespace Lessons.Lesson19_EventBus
{
    [RequireComponent(typeof(PlayerModel))]
    [DefaultExecutionOrder(-100)]
    public sealed class PlayerEntity : MonoEntityBase
    {
        private void Awake()
        {
            PlayerModel model = GetComponent<PlayerModel>();
            Add(new PositionComponent(model.position.transform));
            Add(new CoordinatesComponent(model.position.coordinates));
            Add(new StatsComponent(model.stats));
            Add(new TransformComponent(transform));
        }
    }
}
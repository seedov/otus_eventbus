using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Entities;
using Lessons.Lesson19_EventBus;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.EventTrigger;

public class MoveEntityVisualTask : BaseTask
{
    private readonly IEntity _entity;
    private readonly Vector3 _position;

    public MoveEntityVisualTask(IEntity entity, Vector3 position)
    {
        _entity = entity;
        _position = position;
    }

    public override async Task Run()
    {
        var position = _entity.Get<PositionComponent>();
        await DOTween.To(() => position.Value, x => position.Value = x, _position, 0.3f);

    }
}

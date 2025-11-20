using System.Threading.Tasks;
using Entities;
using Lessons.Lesson19_EventBus;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class DestroyEntityVisualTask : BaseTask
{
    private readonly IEntity _entity;


    public DestroyEntityVisualTask(IEntity entity)
    {
        _entity = entity;

    }



    public async override Task Run()
    {
        var transform = _entity.Get<TransformComponent>().Value;
        await transform.DOScale(Vector3.zero, .3f);
        var destroyComponent = _entity.Get<DestroyComponent>();
        destroyComponent.Destroy();
    }


}

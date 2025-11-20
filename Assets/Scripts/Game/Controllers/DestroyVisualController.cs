using System;
using Entities;

namespace Lessons.Lesson19_EventBus
{
    public sealed class DestroyVisualController : IDisposable
    {

        private readonly IEventBus _eventBus;
        private readonly VisualPipeline _visualPipeline;

        public DestroyVisualController(IEventBus eventBus, VisualPipeline visualPipeline)
        {

            _eventBus = eventBus;
            _eventBus.Subscribe<DestroyEvent>(ProcessDestroyEvent);
            _visualPipeline = visualPipeline;
        }

        private void ProcessDestroyEvent(DestroyEvent evt)
        {
            Destroy(evt.Entity);
        }

        private void Destroy(IEntity entity)
        {
            _visualPipeline.AddTask(new DestroyEntityVisualTask(entity));
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<DestroyEvent>(ProcessDestroyEvent);
        }
    }
}
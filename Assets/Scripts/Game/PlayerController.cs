using System;
using Entities;
using UnityEngine;
using Zenject;

namespace Lessons.Lesson19_EventBus
{
    public sealed class PlayerController : IInitializable, IDisposable
    {
        private readonly KeyboardInput _input;
        private readonly IEntity _player;
        private IEventBus _eventBus;


        public PlayerController(
            KeyboardInput input, 
            IEventBus eventBus,
            PlayerService playerService)
        {
            _input = input;
            _player = playerService.Player;
            _eventBus = eventBus;
        }
        
        void IInitializable.Initialize()
        {
            _input.MovePerformed += OnMovePreformed;
        }

        void IDisposable.Dispose()
        {
            _input.MovePerformed -= OnMovePreformed;
        }

        private void OnMovePreformed(Vector2Int direction)
        {
            _eventBus.RaiseEvent(new ApplyDirectionEvent(_player, direction));
        }
    }

    internal class ApplyDirectionEvent
    {
        public readonly IEntity Enity;
        public readonly Vector2Int Direction;

        public ApplyDirectionEvent(IEntity enity, Vector2Int direction)
        {
            Enity = enity;
            Direction = direction;
        }
    }
}
using System;
using Entities;
using UnityEngine;
using UnityEngine.Windows;
using Zenject;

namespace Lessons.Lesson19_EventBus
{
    public sealed class PlayerController : IInitializable, IDisposable
    {
        private readonly IEventBus _eventBus;
        private readonly IEntity _player;

        private readonly KeyboardInput _input;

        public PlayerController(
            IEventBus eventBus, 
            KeyboardInput input, 
            PlayerService playerService)
        {
            _eventBus = eventBus;
            _player = playerService.Player;

            _input = input;
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
}
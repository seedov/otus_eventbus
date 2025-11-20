using System.Threading.Tasks;
using Lessons.Lesson19_EventBus;
using UnityEngine;
using System;
using Atomic.Elements;
public class WaitForInputTask:BaseTask
{
    private KeyboardInput _keyboardInput;
    private TaskCompletionSource<bool> _taskCompletionSource;
    private IEventBus _eventBus;
    private PlayerService _playerService;
    public WaitForInputTask(KeyboardInput keyboardInput, IEventBus eventBus, PlayerService playerService)
    {
        _keyboardInput = keyboardInput;
        _eventBus = eventBus;
        _playerService = playerService;
    }

    public override async Task Run()
    {
        if(_taskCompletionSource != null)
            await _taskCompletionSource.Task;
        _keyboardInput.MovePerformed += KeyboardInput_MovePerformed;
        _taskCompletionSource = new TaskCompletionSource<bool>();
        await _taskCompletionSource.Task;
        _taskCompletionSource = null;

        _keyboardInput.MovePerformed -= KeyboardInput_MovePerformed;
    }

    private void KeyboardInput_MovePerformed(Vector2Int direction)
    {
        _eventBus.RaiseEvent(new ApplyDirectionEvent(_playerService.Player, direction));
        _taskCompletionSource.SetResult(true);
    }
}
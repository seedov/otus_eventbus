using UnityEngine;
using Zenject;

public class HelperTools : MonoBehaviour
{
    [Inject]
    private TurnPipeline _pipeline;

    [Inject]
    private WaitForInputTask _waitForUserInputTask;

    [ContextMenu("Run turn pipeline")]
    private void RunTurnPipeline()
    {
        RunPipeline();
    }

    private async void RunPipeline()
    {
        await _pipeline.Run();
    }

    private void Start()
    {
        _pipeline.AddTask(_waitForUserInputTask);
    }
}

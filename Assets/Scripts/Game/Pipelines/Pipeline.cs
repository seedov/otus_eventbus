using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;


public class Pipeline
{
    private List<BaseTask> _tasks = new();
    public event Action Completed;

    public void AddTask(BaseTask task)
    {
        _tasks.Add(task);
    }
    public virtual async Task Run()
    {
        OnStarted();
        foreach (var task in _tasks)
        {
            await task.Run();
        }
        OnCompleted();
    }

    public void Clear()
    {
        _tasks.Clear();
    }

    protected virtual void OnStarted()
    {
        Debug.Log($"Pipeline {this.GetType().Name} started");
    }
    protected virtual void OnCompleted()
    {
        Debug.Log($"Pipeline {this.GetType().Name} completed");
        Completed?.Invoke();
    }
}

// TaskManager.cs - Manages the tasks and their lifecycle
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WPFTaskManager
{
    public class TaskManager
    {
        private Dictionary<int, TaskInfo> _tasks = new Dictionary<int, TaskInfo>();
        private int _taskCounter = 0;

        public IReadOnlyDictionary<int, TaskInfo> Tasks => _tasks;

        public TaskInfo StartNewTask()
        {
            int taskId = ++_taskCounter;
            var cts = new CancellationTokenSource();
            var taskInfo = new TaskInfo
            {
                TaskId = taskId,
                TaskName = $"Task {taskId}",
                Progress = 0,
                CancellationTokenSource = cts
            };

            _tasks[taskId] = taskInfo;
            RunTask(taskInfo);
            return taskInfo;
        }

        private async void RunTask(TaskInfo taskInfo)
        {
            try
            {
                for (int i = 0; i <= 100; i++)
                {
                    if (taskInfo.CancellationTokenSource.Token.IsCancellationRequested)
                    {
                        break;
                    }
                    taskInfo.Progress = i;
                    await Task.Delay(100);
                }
            }
            catch (Exception)
            {
                // Handle any unexpected exceptions
            }
        }

        public void StopTask(int taskId)
        {
            if (_tasks.ContainsKey(taskId))
            {
                _tasks[taskId].CancellationTokenSource.Cancel();
                _tasks.Remove(taskId);
            }
        }

        public void StopAllTasks()
        {
            foreach (var task in _tasks.Values)
            {
                task.CancellationTokenSource.Cancel();
            }
            _tasks.Clear();
        }
    }
}

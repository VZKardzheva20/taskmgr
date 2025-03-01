using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFTaskManager
{
    public partial class MainWindow : Window
    {
        private TaskManager _taskManager = new TaskManager();

        public MainWindow()
        {
            InitializeComponent();
            TaskList.ItemsSource = _taskManager.Tasks.Values.ToList();
            TaskList.SelectionChanged += TaskList_SelectionChanged;
        }

        private void StartNewTask(object sender, RoutedEventArgs e)
        {
            var taskInfo = _taskManager.StartNewTask();
            RefreshTaskList();
            Task.Run(async () => await RunTask(taskInfo)); 
        }

        private async Task RunTask(TaskInfo taskInfo)
        {
            try
            {
                for (int i = 0; i <= 100; i++)
                {
                    if (taskInfo.CancellationTokenSource.Token.IsCancellationRequested)
                    {
                        break;
                    }
                    while (taskInfo.IsPaused)
                    {
                        await Task.Delay(100); // Pause
                    }
                    taskInfo.Progress = i;
                    await Task.Delay(100);

                    if (i % 10 == 0) //Update on 10%
                    {
                        Dispatcher.BeginInvoke((Action)(() => UpdateTaskItem(taskInfo)));
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Task {taskInfo.TaskId} error: {ex.Message}");
            }
        }

        private void ListTasks(object sender, RoutedEventArgs e)
        {
            string taskDetails = string.Join("\n", _taskManager.Tasks.Values.Select(t => $"{t.TaskName} - {t.Progress}%"));

            MessageBox.Show(taskDetails == "" ? "No running tasks." : taskDetails, "Running Tasks", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void StopTask(object sender, RoutedEventArgs e)
        {
            if (TaskList.SelectedItem is TaskInfo selectedTask)
            {
                _taskManager.StopTask(selectedTask.TaskId);
                RefreshTaskList();
            }
        }

        private void PauseTask(object sender, RoutedEventArgs e)
        {
            if (TaskList.SelectedItem is TaskInfo selectedTask)
            {
                selectedTask.IsPaused = true;
                RefreshTaskList();
            }
        }

        private void ResumeTask(object sender, RoutedEventArgs e)
        {
            if (TaskList.SelectedItem is TaskInfo selectedTask)
            {
                selectedTask.IsPaused = false;
                RefreshTaskList();
            }
        }

        private void ChangeTaskPriority(object sender, SelectionChangedEventArgs e)
        {
            if (TaskList.SelectedItem is TaskInfo selectedTask)
            {
                if (sender is ComboBox comboBox && comboBox.SelectedItem is string selectedPriority)
                {
                    if (Enum.TryParse(selectedPriority, out TaskPriority priority))
                    {
                        selectedTask.Priority = priority;
                        RefreshTaskList();
                    }
                }
            }
        }

        private void ExitApplication(object sender, RoutedEventArgs e)
        {
            _taskManager.StopAllTasks();
            Application.Current.Shutdown();
        }

        private void RefreshTaskList()
        {
            TaskList.ItemsSource = null;
            TaskList.ItemsSource = _taskManager.Tasks.Values.OrderByDescending(t => t.Priority).ToList(); // Sort priority
        }

        private void TaskList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Dispatcher.BeginInvoke((Action)(() =>
            {
                foreach (var item in TaskList.Items)
                {
                    if (TaskList.ItemContainerGenerator.ContainerFromItem(item) is ListBoxItem listBoxItem)
                    {
                        listBoxItem.Background = Brushes.Transparent;
                    }
                }

                if (TaskList.SelectedItem is TaskInfo selectedTask)
                {
                    if (TaskList.ItemContainerGenerator.ContainerFromItem(selectedTask) is ListBoxItem selectedListBoxItem)
                    {
                        selectedListBoxItem.Background = Brushes.LightBlue; 
                    }
                }
            }));
        }

        private void UpdateTaskItem(TaskInfo taskInfo)
        {
            if (TaskList.Items.Contains(taskInfo))
            {
                TaskList.Items.Refresh(); // refresh singl task
            }
        }
    }
}

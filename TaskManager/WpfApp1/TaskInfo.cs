using System;
using System.ComponentModel;
using System.Threading;

namespace WPFTaskManager
{
    public enum TaskPriority { Low, Normal, High }

    public class TaskInfo : INotifyPropertyChanged
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }

        private TaskPriority _priority;
        public TaskPriority Priority
        {
            get => _priority;
            set { _priority = value; OnPropertyChanged("Priority"); }
        }

        private int _progress;
        public int Progress
        {
            get => _progress;
            set
            {
                _progress = value;
                OnPropertyChanged("Progress");
                if (_progress == 100)
                {
                    TaskCompleted = true;
                    CompletionTime = DateTime.Now;
                }
            }
        }

        private bool _isPaused;
        public bool IsPaused
        {
            get => _isPaused;
            set { _isPaused = value; OnPropertyChanged("IsPaused"); }
        }

        public bool TaskCompleted { get; private set; } = false;
        public DateTime? CompletionTime { get; private set; }
        public DateTime StartTime { get; } = DateTime.Now;

        public CancellationTokenSource CancellationTokenSource { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
using System.ComponentModel;
using Microsoft.Maui.Controls;

namespace PlannerApp;

// Represents a single planner task item
public class TaskItem : INotifyPropertyChanged
{
    private bool _isDone;

    public string Description { get; set; } = string.Empty;
    public string Timestamp { get; set; } = string.Empty;

    public bool IsDone
    {
        get => _isDone;
        set
        {
            if (_isDone != value)
            {
                _isDone = value;
                OnPropertyChanged(nameof(IsDone));
                OnPropertyChanged(nameof(BackgroundColor)); // UI updates dynamically
            }
        }
    }

    // Combine task text with timestamp
    public string DisplayText => $"{Description} ({Timestamp})";

    // Change background color based on completion state
    public Color BackgroundColor =>
        IsDone ? Color.FromArgb("#DFDFDF") : Color.FromArgb("#FFFFFFFF");

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}

using System.ComponentModel;
using Microsoft.Maui.Controls;

namespace PlannerApp;
// MODEL CLASS
// Demonstrates Auto-Formatting, Dynamic Color Binding, and PropertyChanged updates (Data Binding)
// Represents a single planner task item
public class TaskItem : INotifyPropertyChanged
{
    private bool _isDone;

    public string Description { get; set; } = string.Empty;
    public string Timestamp { get; set; } = string.Empty;

    //  Tesler’s Law: Only necessary complexity (task done state)
    public bool IsDone
    {
        get => _isDone;
        set
        {
            if (_isDone != value)
            {
                _isDone = value;
                OnPropertyChanged(nameof(IsDone));  // Notifies UI about checkbox changes
                OnPropertyChanged(nameof(BackgroundColor)); // UI updates background dynamically
            }
        }
    }
    // Auto formatting combine task text with timestamp
    public string DisplayText => $"{Description} ({Timestamp})";

    // Change background color based on completion state (Aesthetic–Usability Effect)
    public Color BackgroundColor =>
        IsDone ? Color.FromArgb("#DFDFDF") : Color.FromArgb("#FFFFFFFF");

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}

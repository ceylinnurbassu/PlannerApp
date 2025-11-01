using System.Collections.ObjectModel;
using System.Globalization;
namespace PlannerApp;

public partial class MainPage : ContentPage
{
    public ObservableCollection<TaskItem> Tasks { get; set; } = new();

    public MainPage()
    {
        InitializeComponent();
        TaskList.ItemsSource = Tasks;

        // Tarihi ata
        DateLabel.Text = DateTime.Now.ToString("dddd, MMM dd yyyy", new CultureInfo("en-US"));

    }

    private void OnAddTaskClicked(object sender, EventArgs e)
    {
        string text = TaskEntry.Text?.Trim();
        if (string.IsNullOrEmpty(text))
        {
            DisplayAlert("Empty Task", "Please enter a task before adding.", "OK");
            return;
        }

        var task = new TaskItem
        {
            Description = text,
            Timestamp = DateTime.Now.ToString("HH:mm")
        };

        Tasks.Add(task);
        TaskEntry.Text = string.Empty;
    }

    private void OnDeleteClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is TaskItem task)
        {
            Tasks.Remove(task);
        }
    }

    private void OnClearAllClicked(object sender, EventArgs e)
    {
        bool confirm = DisplayAlert("Clear All", "Are you sure you want to delete all tasks?", "Yes", "No").Result;
        if (confirm)
        {
            Tasks.Clear();
        }
    }
}

public class TaskItem
{
    public string Description { get; set; } = string.Empty;
    public string Timestamp { get; set; } = string.Empty;
    public bool IsDone { get; set; } = false;

    public string DisplayText => $"{Description} ({Timestamp})";
    public TextDecorations TextDecoration => IsDone ? TextDecorations.Strikethrough : TextDecorations.None;
}

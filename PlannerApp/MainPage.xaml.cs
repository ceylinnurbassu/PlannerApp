using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PlannerApp;

public partial class MainPage : ContentPage
{
    public ObservableCollection<TaskItem> Tasks { get; set; } = new();
    public ICommand ClearCommand { get; }

    public MainPage()
    {
        InitializeComponent();
        ClearCommand = new Command(OnClearTasks);
        TaskList.ItemsSource = Tasks;
        BindingContext = this; // ICommand binding için
    }

    private void OnAddTaskClicked(object sender, EventArgs e)
    {
        string text = TaskEntry.Text?.Trim();

        if (string.IsNullOrEmpty(text))
        {
            DisplayAlert("Empty Task", "Please enter a task.", "OK");
            return;
        }

        var task = new TaskItem
        {
            Description = text,
            CreatedAt = DateTime.Now
        };

        Tasks.Add(task);
        TaskEntry.Text = string.Empty;
    }

    private void OnDeleteClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.CommandParameter is TaskItem task)
            Tasks.Remove(task);
    }

    private void OnClearTasks()
    {
        if (Tasks.Count > 0)
            Tasks.Clear();
    }
}

public class TaskItem
{
    public string Description { get; set; } = string.Empty;
    public bool IsDone { get; set; }
    public DateTime CreatedAt { get; set; }

    public string DisplayText => $"{Description} ({CreatedAt:HH:mm})";
    public TextDecorations TextDecoration => IsDone ? TextDecorations.Strikethrough : TextDecorations.None;
}

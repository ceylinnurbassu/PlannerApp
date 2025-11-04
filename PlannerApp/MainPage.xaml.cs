using System.Collections.ObjectModel;
using System.Globalization;

namespace PlannerApp;

public partial class MainPage : ContentPage
{
    // Dynamic List Display — ObservableCollection updates UI automatically
    public ObservableCollection<TaskItem> Tasks { get; set; } = new();

    public MainPage()
    {
        InitializeComponent();

        TaskList.ItemsSource = Tasks;

        // Auto-formatting — show English date format
        DateLabel.Text = DateTime.Now.ToString("dddd, MMM dd yyyy", new CultureInfo("en-US"));
    }

    // ✅ Add new task
    private async void OnAddTaskClicked(object sender, EventArgs e)
    {
        string text = TaskEntry.Text?.Trim();
        if (string.IsNullOrEmpty(text))
        {
            await DisplayAlert("Empty Task", "Please enter a task before adding.", "OK");
            return;
        }

        var task = new TaskItem
        {
            Description = text,
            Timestamp = DateTime.Now.ToString("HH:mm", new CultureInfo("en-US")),
            IsDone = false
        };

        // Newest tasks appear at the top
        Tasks.Insert(0, task);

        TaskEntry.Text = string.Empty;
    }

    // ✅ Delete single task with confirmation popup
    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is TaskItem task)
        {
            bool confirm = await DisplayAlert(
                "Delete Task",
                $"Are you sure you want to delete \"{task.Description}\"?",
                "Yes",
                "No"
            );

            if (confirm)
                Tasks.Remove(task);
        }
    }

    // ✅ Clear all tasks with confirmation popup
    private async void OnClearAllClicked(object sender, EventArgs e)
    {
        if (Tasks.Count == 0)
        {
            await DisplayAlert("No Tasks", "There are no tasks to clear.", "OK");
            return;
        }

        bool confirm = await DisplayAlert(
            "Clear All",
            "Are you sure you want to delete all tasks?",
            "Yes",
            "No"
        );

        if (confirm)
            Tasks.Clear();
    }
}

using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tk.App.ViewModels;
using Tk.Database;
using Tk.Models;
using Tk.Models.Database;
using Tk.Api;

namespace Tk.App.Pages;

using Logger = Microsoft.Extensions.Logging.ILogger<TaskListPage>;

public partial class TaskListPage : ContentPage {

    public TaskListPage(
        TkDbContext db,
        Logger      logger

    ) {
        InitializeComponent();
        this.db     = db;
        this.logger = logger;

        BindingContext = Tasks;
        
    }

    TkDbContext db     { get; set; }
    Logger      logger { get; set; }


    public ObservableCollection<TaskListViewModel> Tasks { get; set; } = [];


    protected override async void OnNavigatedTo(NavigatedToEventArgs e) {
        base.OnNavigatedTo(e);

        var tasks = await db.Tasks.AsQueryable().ToListAsync();

        MainThread.BeginInvokeOnMainThread(() => {
            Tasks.Clear();
            foreach (var t in tasks) {
                Tasks.Add(new () {
                    Name     = t.Name,
                    DueDate  = t.DueDateDisplay(),
                    Complete = t.CompletionEvents.Any()
                });
            }
        });

    }

    private async void NewTask(object sender, EventArgs e) {
        await Shell.Current.GoToAsync($"NewItemPage", true);
    }

    private async void Notification(object sender, EventArgs e) {
        await Task.CompletedTask;

        Notifications.TestNotification();
    }


    private async void TasksSelectionChanged(object sender, SelectionChangedEventArgs e) {
        await Task.CompletedTask;
    }

    private void Log(Exception ex) {
        logger.LogError("{ex}", ex);
    }

}


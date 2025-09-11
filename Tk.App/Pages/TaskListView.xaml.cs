using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using Tk.App.ViewModels;
using Tk.Database;
using Tk.Models;
using Tk.Models.Database;

namespace Tk.App.Pages;

public partial class TaskListView : ContentPage {

    public TaskListView(
        TkDbContext db
    ) {
        InitializeComponent();
        this.db = db;

        BindingContext = Tasks;
    }

    TkDbContext db { get; set; }


    public ObservableCollection<TaskListViewModel> Tasks { get; set; } = [];


    protected override async void OnNavigatedTo(NavigatedToEventArgs e) {
        base.OnNavigatedTo(e);

        await Task.CompletedTask;
        // var tasks = await db.Tasks.AsQueryable().ToListAsync();
        var tasks = TestData.Tasks;

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

    private async void TasksSelectionChanged(object sender, SelectionChangedEventArgs e) {
        await Task.CompletedTask;
    }

}


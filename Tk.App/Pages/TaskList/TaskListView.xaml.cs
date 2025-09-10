using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using Tk.Database;
using Tk.Models;
using Tk.Models.Database;

namespace Tk.App.Pages.TaskList;

public partial class TaskListView : ContentPage {

    public TaskListView(
        TkDbContext db
    ) {
        InitializeComponent();
        this.db = db;

        BindingContext = Tasks;
    }

    TkDbContext db { get; set; }


    public ObservableCollection<TaskModel> Tasks { get; set; } = [];


    protected override async void OnNavigatedTo(NavigatedToEventArgs e) {
        base.OnNavigatedTo(e);

        var tasks = await db.Tasks.AsQueryable().ToListAsync();

        MainThread.BeginInvokeOnMainThread(() => {
            Tasks.Clear();
            foreach (var t in tasks) {
                Tasks.Add(t);
            }
        });

    
    }

    private async void TasksSelectionChanged(object sender, SelectionChangedEventArgs e) {
        
    }

}


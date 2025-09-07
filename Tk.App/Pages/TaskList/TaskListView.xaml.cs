using Microsoft.EntityFrameworkCore;
using Tk.Database;

namespace Tk.App.Pages.TaskList;

public partial class TaskListView : ContentPage {
    public TaskListView(
        TkDbContext db
    ) {
        InitializeComponent();
        this.db = db;
    }

    TkDbContext db { get; set; }


}


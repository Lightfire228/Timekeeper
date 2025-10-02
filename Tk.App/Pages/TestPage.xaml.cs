using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tk.App.ViewModels;
using Tk.Database;
using Tk.Models;
using Tk.Models.Database;
using Tk.Api;
using Microsoft.Maui.Platform;

namespace Tk.App.Pages;

using Logger = Microsoft.Extensions.Logging.ILogger<TestPage>;

public partial class TestPage : ContentPage {

    public TestPage(
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


    // protected override async void OnNavigatedTo(NavigatedToEventArgs e) {
    //     base.OnNavigatedTo(e);
    // }

    private async void Notification(object sender, EventArgs e) {
        await Task.CompletedTask;

        logger.LogInformation("test notification button pressed");

        // Microsoft.Maui.ApplicationModel.Platform.ac

        // Notifications.TestNotification(Resource.Drawable.dotnet_bot, typeof(TestPage), logger);

        // Works
        var manager = NotificationManagerService.GetInstance();
        manager.SendNotification("test", "test with manager", new() {
            MainActivity = typeof(MainActivity),
            SmallIcon    = Resource.Drawable.dotnet_bot,
            LargeIcon    = Resource.Drawable.dotnet_bot,
        });
    }

    // Bork
    private async void Reminder(object sender, EventArgs e) {
        await Task.CompletedTask;

        logger.LogInformation("test reminder button pressed");

        // Notifications.TestReminder(Resource.Drawable.dotnet_bot, typeof(TestPage), logger);

        var manager = NotificationManagerService.GetInstance();
        manager.SendNotification("test", "test 1m reminder", new() {
            MainActivity = typeof(MainActivity),
            SmallIcon    = Resource.Drawable.dotnet_bot,
            LargeIcon    = Resource.Drawable.dotnet_bot,
        }, DateTime.Now.AddSeconds(10));
    }


}


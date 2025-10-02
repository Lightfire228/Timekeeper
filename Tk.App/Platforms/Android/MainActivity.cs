using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Microsoft.Extensions.Logging;
using Serilog;
using Tk.Api;

namespace Tk.App;

using ILogger = Microsoft.Extensions.Logging.ILogger;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity
    : MauiAppCompatActivity
{

    public MainActivity() {
        Logger = GetLogger();
    }

    private readonly ILogger Logger;

    protected override void OnCreate(Bundle? savedInstanceState) {
        Logger.LogInformation("Main activity on create");
        base.OnCreate(savedInstanceState);

        TryWithLogging(Logger, () => CreateNotificationFromIntent(Intent));
    }

    protected override void OnNewIntent(Intent? intent) {
        Logger.LogInformation("Main activity on new intent");
        base.OnNewIntent(intent);


        TryWithLogging(Logger, () => CreateNotificationFromIntent(Intent));
    }



    void CreateNotificationFromIntent(Intent? intent) {
        if (intent?.Extras == null) {
            return;
        }

        Logger.LogInformation("notif started");

        string title   = intent.GetStringExtra(NotificationManagerService.TitleKey)!;
        string message = intent.GetStringExtra(NotificationManagerService.MessageKey)!;

        var service = IPlatformApplication.Current!.Services.GetService<INotificationManagerService>();
        service!.ReceiveNotification(title, message);

        Logger.LogInformation("Main activity notif finished");
    }

    static void TryWithLogging(ILogger logger, Action action) {
        try {
            action();
        }
        catch (Exception e) {
            logger.LogError("Uncaught exception in Main Activity: {ex}", e);

            throw;
        }
    }

    static ILogger GetLogger() {
        var serilog = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File(Path.Join(FileSystem.AppDataDirectory, "startup.log"))
            .CreateLogger()
        ;

        return new LoggerFactory()
            .AddSerilog(serilog)
            .CreateLogger<MainActivity>()
        ;
    }


}

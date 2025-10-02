using Android.Content;
using Microsoft.Extensions.Logging;
using Tk.Api;
using Serilog;


namespace Tk.App;

using ILogger = Microsoft.Extensions.Logging.ILogger;
using Logger = ILogger<AlarmHandler>;


[BroadcastReceiver(Enabled = true, Label = "Local Notifications Broadcast Receiver")]
public class AlarmHandler
    : BroadcastReceiver 
{

    private readonly ILogger Logger = GetLogger();
    
    public override void OnReceive(Context? context, Intent? intent) {

        Logger.LogInformation("made it to alarm handler");

        if (intent?.Extras == null) {
            return;
        }

        string title   = intent.GetStringExtra(NotificationManagerService.TitleKey)   ?? throw Bail("Unable to get notification title");
        string message = intent.GetStringExtra(NotificationManagerService.MessageKey) ?? throw Bail("Unable to get notification message");

        var manager = NotificationManagerService.GetInstance();
        manager.Show(title, message, new() {
            MainActivity = typeof(MainActivity),
            SmallIcon    = Resource.Drawable.dotnet_bot,
            LargeIcon    = Resource.Drawable.dotnet_bot,
        });
    }

    private Exception Bail(string message) =>
        new (message)
    ;

    static ILogger GetLogger() {
        var serilog = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File(Path.Join(FileSystem.AppDataDirectory, "alarm_handler.log"))
            .CreateLogger()
        ;

        return new LoggerFactory()
            .AddSerilog(serilog)
            .CreateLogger<NotificationManagerService>()
        ;
    }
}
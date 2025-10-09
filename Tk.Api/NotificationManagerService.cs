using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using AndroidX.Core.App;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Storage;
using Serilog;

namespace Tk.Api;

using ILogger = Microsoft.Extensions.Logging.ILogger;
using Path = System.IO.Path;

// TODO:
#pragma warning disable CA1416 // Validate platform compatibility

// https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/local-notifications?view=net-maui-9.0&pivots=devices-android

public class NotificationManagerService
    // : INotificationManagerService
{

    const string channelId          = "default";
    const string channelName        = "Default";
    const string channelDescription = "The default channel for notifications.";

    public const string TitleKey   = "title";
    public const string MessageKey = "message";

    bool channelInitialized = false;
    int  messageId          = 0;
    int  pendingIntentId    = 0;



    readonly      NotificationManagerCompat   compatManager;

    public event  EventHandler?               NotificationReceived;

    public static NotificationManagerService? Instance { get; private set; }


    private readonly PendingIntentFlags UpdateImmutable = PendingIntentFlags.UpdateCurrent | PendingIntentFlags.Immutable;
    private readonly PendingIntentFlags UpdateOnly      = PendingIntentFlags.UpdateCurrent;
    private readonly PendingIntentFlags CancelImmutable = PendingIntentFlags.CancelCurrent | PendingIntentFlags.Immutable;
    private readonly PendingIntentFlags CancelOnly      = PendingIntentFlags.CancelCurrent;

    private readonly bool ImmutableSupported = Build.VERSION.SdkInt >= BuildVersionCodes.S;
    private readonly bool ChannelsSupported  = Build.VERSION.SdkInt >= BuildVersionCodes.O;

    private readonly ILogger Logger;



    public NotificationManagerService() {
        CreateNotificationChannel();
        compatManager = NotificationManagerCompat.From(Platform.AppContext) ?? throw Panic("Unable to get compat manager");
        Logger = GetLogger();
    }


    public static NotificationManagerService GetInstance() =>
        Instance ??= new()
    ;
    



    public void SendNotification(string title, string message, Resources resources, DateTime? notifyTime = null) {
        Logger.LogInformation("Heyo");
        CreateNotificationChannel();

        if (notifyTime == null) {
            Show(title, message, resources);
            return;
        }

        var pendingIntent = GetPendingIntent(
            title,
            message,
            resources,
            CancelImmutable,
            CancelOnly,
            PendingIntent.GetBroadcast
        );

        long          triggerTime   = GetNotifyTime(notifyTime.Value);
        AlarmManager  alarmManager  = (Platform.AppContext.GetSystemService(Context.AlarmService) as AlarmManager)
            ?? throw Panic("Unable to get alarm manager")
        ;


        alarmManager.Set(AlarmType.RtcWakeup, triggerTime, pendingIntent);

        Logger.LogInformation("finished");
    }

    public void ReceiveNotification(string title, string message) {

        var args = new NotificationEventArgs{
            Title   = title,
            Message = message,
        };

        NotificationReceived?.Invoke(null, args);
    }


    public void Show(string title, string message, Resources resources) {

        Logger.LogInformation("Log");

        var pendingIntent = GetPendingIntent(
            title, 
            message, 
            resources, 
            UpdateImmutable,
            UpdateOnly,
            PendingIntent.GetActivity
        );


        var builder       = new NotificationCompat.Builder(Platform.AppContext, channelId)
             .SetContentIntent(pendingIntent)
            ?.SetContentTitle (title)
            ?.SetContentText  (message)
            ?.SetLargeIcon    (BitmapFactory.DecodeResource(
                Platform.AppContext.Resources,
                resources.LargeIcon
            ))
            ?.SetSmallIcon    (resources.SmallIcon)

            ?? throw Panic("Unable to build notification")

        ;

        var notification = builder.Build();
        compatManager.Notify(messageId++, notification);
    }

    private void CreateNotificationChannel() {

        // Create the notification channel, but only on API 26+.
        if (channelInitialized || !ChannelsSupported) {
            return;
        }

        var channel = new NotificationChannel(channelId, channelName, NotificationImportance.Default) {
            Description = channelDescription
        };

        // Register the channel
        NotificationManager manager = (NotificationManager) (
            Platform.AppContext.GetSystemService(Context.NotificationService) ?? throw Panic("Unable to get notification manager")
        );

        manager.CreateNotificationChannel(channel);
        channelInitialized = true;
    }

    private long GetNotifyTime(DateTime notifyTime) {

        DateTime utcTime      = TimeZoneInfo.ConvertTimeToUtc(notifyTime);
        double   epochDiff    = (new DateTime(1970, 1, 1) - DateTime.MinValue).TotalSeconds;
        long     utcAlarmTime = utcTime.AddSeconds(-epochDiff).Ticks / 10000;

        return utcAlarmTime; // milliseconds
    }

    private Exception Panic(string message) =>
        new(message)
    ;

    private PendingIntent GetPendingIntent(
        string             title,
        string             message,
        Resources          resources,
        PendingIntentFlags actionImmutable,
        PendingIntentFlags actionOnly,

        Func<Context?, int, Intent, PendingIntentFlags, PendingIntent?> getIntent
    ) {

        Intent intent = new (Platform.AppContext, resources.MainActivity);
        
        intent.PutExtra(TitleKey,   title);
        intent.PutExtra(MessageKey, message);
        intent.SetFlags(ActivityFlags.SingleTop | ActivityFlags.ClearTop);

        var pendingIntentFlags = ImmutableSupported? actionImmutable : actionOnly;


        return getIntent(
            Platform.AppContext,
            pendingIntentId++,
            intent,
            pendingIntentFlags
        )
            ?? throw Panic("Unable to get pending intent")
        ;

    }

    static ILogger GetLogger() {
        var serilog = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File(Path.Join(FileSystem.AppDataDirectory, "notification_manager.log"))
            .CreateLogger()
        ;

        return new LoggerFactory()
            .AddSerilog(serilog)
            .CreateLogger<NotificationManagerService>()
        ;
    }
}

// public interface INotificationManagerService {

//     event EventHandler NotificationReceived;
//     void SendNotification   (string title, string message, Resources resources, DateTime? notifyTime = null);
//     void ReceiveNotification(string title, string message);
// }

public class NotificationEventArgs() 
    : EventArgs
{
    public required string Title   { get; set; }
    public required string Message { get; set; }
}

public class Resources {
    public required Type MainActivity { get; set; }
    public required int  SmallIcon    { get; set; }
    public required int  LargeIcon    { get; set; }
}
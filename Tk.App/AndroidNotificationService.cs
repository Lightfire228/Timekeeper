
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using AndroidX.Core.App;

using Microsoft.Extensions.Logging;

namespace Tk.Api.AndroidApi;

using ILogger = ILogger;


// TODO: 
#pragma warning disable CA1416 // Validate platform compatibility

public class AndroidNotificationService(AndroidNotificationServiceOpts opts)
    : INotificationService
{
    static int MessageId                  = 0;
    static int PendingActivityRequestCode = 0;


    readonly AndroidNotificationServiceOpts Opts = opts;


    static readonly bool ImmutableSupported = Build.VERSION.SdkInt >= BuildVersionCodes.S;
    static readonly bool ChannelsSupported  = Build.VERSION.SdkInt >= BuildVersionCodes.O;

    static PendingIntentFlags UpdateFlags { get => 
        ImmutableSupported ? PendingIntentFlags.UpdateCurrent | PendingIntentFlags.Immutable
                           : PendingIntentFlags.UpdateCurrent
    ;}

    static PendingIntentFlags CancelFlags { get =>
        ImmutableSupported ? PendingIntentFlags.CancelCurrent | PendingIntentFlags.Immutable
                           : PendingIntentFlags.CancelCurrent
    ;}
        

    public void SendNotification(string title, string message, NotificationChannelType channel, DateTime? notifyTime = null) {
        Opts.Logger.LogInformation("Heyo");

        CreateNotificationChannel(channel);

        if (notifyTime == null) {
            Show(title, message, channel);
            return;
        }

        // var pendingIntent = GetPendingIntent(
        //     title,
        //     message,
        //     resources,
        //     CancelImmutable,
        //     CancelOnly,
        //     PendingIntent.GetBroadcast
        // );

        // long          triggerTime   = GetNotifyTime(notifyTime.Value);
        // AlarmManager  alarmManager  = (AppContext.GetSystemService(Context.AlarmService) as AlarmManager)
        //     ?? throw Panic("Unable to get alarm manager")
        // ;


        // alarmManager.Set(AlarmType.RtcWakeup, triggerTime, pendingIntent);

        // Logger.LogInformation("finished");
    }

    // public void ReceiveNotification(string title, string message) {

    //     var args = new NotificationEventArgs{
    //         Title   = title,
    //         Message = message,
    //     };

    //     NotificationReceived?.Invoke(null, args);
    // }


    public void Show(string title, string message, NotificationChannelType channel) {

        Opts.Logger.LogInformation("Log");

        var pendingIntent = GetPendingIntent(
            title, 
            message, 
            UpdateFlags,
            PendingIntent.GetActivity
        );


        var builder       = new NotificationCompat.Builder(Opts.AppContext, channel.GetAttr().ChannelId)
             .SetContentIntent(pendingIntent)
            ?.SetContentTitle (title)
            ?.SetContentText  (message)
            ?.SetLargeIcon    (BitmapFactory.DecodeResource(
                Opts.AppContext.Resources,
                Opts.LargeIcon
            ))
            ?.SetSmallIcon    (Opts.SmallIcon)

            ?? throw Panic("Unable to build notification")

        ;

        var notification = builder.Build();
        Opts.CompatManager.Notify(MessageId++, notification);
    }

    private void CreateNotificationChannel(NotificationChannelType channelType) {

        if (!ChannelsSupported) {
            return;
        }

        var attr = channelType.GetAttr();

        var channel = new NotificationChannel(
            attr.ChannelId, 
            attr.DisplayName, 
            NotificationImportance.Default
        ) {
            Description = attr.ChannelDesc
        };

        Opts.NotifManager.CreateNotificationChannel(channel);
    }

    // private long GetNotifyTime(DateTime notifyTime) {

    //     DateTime utcTime      = TimeZoneInfo.ConvertTimeToUtc(notifyTime);
    //     double   epochDiff    = (new DateTime(1970, 1, 1) - DateTime.MinValue).TotalSeconds;
    //     long     utcAlarmTime = utcTime.AddSeconds(-epochDiff).Ticks / 10000;

    //     return utcAlarmTime; // milliseconds
    // }

    private Exception Panic(string message) =>
        new(message)
    ;

    private PendingIntent GetPendingIntent(
        string             title,
        string             message,
        PendingIntentFlags intentFlags,

        Func<Context?, int, Intent, PendingIntentFlags, PendingIntent?> getIntent
    ) {

        Intent intent = new (Opts.AppContext, Opts.MainActivity);
        
        intent.PutExtra("Title",   title);
        intent.PutExtra("Message", message);
        intent.SetFlags(ActivityFlags.SingleTop | ActivityFlags.ClearTop);

        return getIntent(
            Opts.AppContext,
            PendingActivityRequestCode++,
            intent,
            intentFlags
        )
            ?? throw Panic("Unable to get pending intent")
        ;

    }

    // static ILogger GetLogger() {
    //     var serilog = new LoggerConfiguration()
    //         .WriteTo.Console()
    //         .WriteTo.File(Path.Join(FileSystem.AppDataDirectory, "notification_manager.log"))
    //         .CreateLogger()
    //     ;

    //     return new LoggerFactory()
    //         .AddSerilog(serilog)
    //         .CreateLogger<AndroidNotificationService>()
    //     ;
    // }
}


public class NotificationEventArgs() 
    : EventArgs
{
    public required string Title   { get; set; }
    public required string Message { get; set; }
}

public class AndroidNotificationServiceOpts {
    public required NotificationManagerCompat CompatManager { get; set; }
    public required NotificationManager       NotifManager  { get; set; }
    public required Type                      MainActivity  { get; set; }
    public required Context                   AppContext    { get; set; }
    public required ILogger                   Logger        { get; set; }

    public required int                       SmallIcon     { get; set; }
    public required int                       LargeIcon     { get; set; }
}




namespace Tk.Api;

using System.Threading.Channels;
using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using AndroidX.Core.App;
using Microsoft.Extensions.Logging;

// TODO:
#pragma warning disable CA1416 // Validate platform compatibility

public class Notifications {

    static readonly string ChannelId = "test";

    static Context Ctx { get; } = Application.Context;

    static int Id { get; set; } = 0;

    public static void RegisterTestChannel() {

        // TODO: move this to app startup 
        var channel     = new NotificationChannel(ChannelId, "test notification", NotificationCompat.PriorityDefault);
        var notiManager = (NotificationManager) Ctx.GetSystemService(Context.NotificationService)!;

        notiManager.CreateNotificationChannel(channel);
    }

    // Android `R.resource` uses ints as ids
    public static void TestNotification(int icon, ILogger logger) {

        logger.LogInformation("Register notif channel");
        RegisterTestChannel();

        // var pendingIntent = PendingIntent.GetActivity(Ctx, 0, null, PendingIntentFlags.Immutable);

        logger.LogInformation("Register build notif");
        var builder = new NotificationCompat.Builder(Ctx, ChannelId)
             .SetContentTitle("test title")
            !.SetSmallIcon   (icon)
            !.SetContentText ("test text")
            !.SetPriority    (NotificationCompat.PriorityDefault)
            !.SetAutoCancel  (true)
        ;

        // TODO: check for notif permissions, and ask to enable

        logger.LogInformation("Send notif");
        NotificationManagerCompat.From(Ctx)!.Notify(++Id, builder!.Build());

        logger.LogInformation("Done");
    }

    public static void TestReminder(int icon, ILogger logger) {
        
    }

}
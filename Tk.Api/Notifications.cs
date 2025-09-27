
namespace Tk.Api;

using System.Threading.Channels;
using Android.App;
using Android.Content;
using Android.OS;
using AndroidX.Core.App;

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

    public static void TestNotification() {

        RegisterTestChannel();

        // var pendingIntent = PendingIntent.GetActivity(Ctx, 0, null, PendingIntentFlags.Immutable);

        var builder = new NotificationCompat.Builder(Ctx, ChannelId)
             .SetContentTitle("test title")
            !.SetContentText ("test text")
            !.SetPriority    (NotificationCompat.PriorityDefault)
            !.SetAutoCancel  (true)
        ;

        NotificationManagerCompat.From(Ctx)!.Notify(++Id, builder!.Build());
    }

}
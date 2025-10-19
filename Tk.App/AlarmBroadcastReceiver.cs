using Android.Content;

namespace Tk.App;

public class AlarmBroadcastRecevier
    : BroadcastReceiver
{

    public override void OnReceive(Context? context, Intent? intent) {

        if (context == null || intent == null) {
            return;
        }

        if (intent.Action != TkIntents.TK_REMINDER) {
            return;
        }

    }
} 
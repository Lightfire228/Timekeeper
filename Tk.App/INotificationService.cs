using System.Reflection;

namespace Tk.App;

public interface INotificationService {
    void SendNotification   (
        string                  title,
        string                  message,
        NotificationChannelType channel,
        DateTime?               notifyTime = null
    );

    // void ReceiveReminder(string title, string message);
}

public enum NotificationChannelType {
    [NotificationChannel(
        DisplayName: "Default",
        ChannelId:   "default",
        ChannelDesc: "The default channel for notifications"
    )]
    Default,
}

public static class NotificationChannelExtensions {

    const           string _attrName = nameof(NotificationChannelAttribute);
    static readonly Type   _enumType = typeof(NotificationChannelType);


    public static NotificationChannelAttribute GetAttr(this NotificationChannelType channel) => _enumType
         .GetMember(GetName(channel))
        ?.First()
         .GetCustomAttribute<NotificationChannelAttribute>()
            ?? throw new Exception($"Value '{GetName(channel)}' must declare attribute of '{_attrName}'")
    ;

    static string GetName(NotificationChannelType channel) => _enumType.GetEnumName(channel) ?? 
        throw new Exception("Unable to get name of channel type")
    ;
}
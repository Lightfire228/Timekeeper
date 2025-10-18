using System.Reflection;

namespace Tk.Api;

[AttributeUsage(AttributeTargets.Field)]
public class NotificationChannelAttribute(
    string DisplayName,
    string ChannelId,
    string ChannelDesc
)
    : Attribute
{
    
    public string DisplayName { get; } = DisplayName;
    public string ChannelId   { get; } = ChannelId;
    public string ChannelDesc { get; } = ChannelDesc;
}

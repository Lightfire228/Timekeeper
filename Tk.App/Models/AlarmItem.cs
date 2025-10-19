namespace Tk.App.Models;

public class AlarmItem {
    
    public required DateTime ScheduledTime { get; set; }
    public          string   Message       { get; set; } = "";
    public          string   Title         { get; set; } = "";
}
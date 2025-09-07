namespace Tk.Database.Models;

public class TaskModel {
    public          long         Id          { get; set; }
    public required string       Name        { get; set; }
    public          string       Description { get; set; } = "";
    public          TaskPriority Priority    { get; set; } = TaskPriority.Low;
    public          DateTime?    Due         { get; set; }
    public          DateTime     CreatedAt   { get; set; } = DateTime.UtcNow;


    public virtual IEnumerable<CompletionEvent> CompletionEvents { get; set; } = [];
}

public enum TaskPriority {
    None,
    Low,
    Medim,
    High,
    FUCK
}
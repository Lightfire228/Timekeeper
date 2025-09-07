namespace Tk.Database.Models;

public class CompletionEvent {
    public long      Id          { get; set; }

    public long      TaskId      { get; set; }

    public DateTime  CompletedAt { get; set; } = DateTime.UtcNow;


    public virtual TaskModel Task { get; set; } = default!;
}

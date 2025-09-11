namespace Tk.App.ViewModels;

public class TaskListViewModel {

    public required string Name     { get; set; }
    public required string DueDate  { get; set; }
    public required bool   Complete { get; set; }
    
}
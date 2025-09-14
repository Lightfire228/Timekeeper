namespace Tk.App.ViewModels;

public class NewItemViewModel {

    public required string   Name         { get; set; }
    public required string   Description  { get; set; }
    public          bool     HasDueDate   { get; set; } = true;
    public          DateTime DueDate      { get; set; } = DateTime.Today.AddDays(1);

    
}
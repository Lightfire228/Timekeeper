
using Microsoft.Extensions.Logging;
using Tk.App.ViewModels;
using Tk.Database;

namespace Tk.App.Pages;

using Logger = Microsoft.Extensions.Logging.ILogger<NewItemPage>;

public partial class NewItemPage
    : ContentPage
{

    public NewItemPage(TkDbContext db, Logger logger) {
        
        InitializeComponent();
        Db = db;

        logger.LogInformation("Made it here");
        // Item = new() {
        //     Name        = "",
        //     Description = "",
        // };
    }

    TkDbContext Db { get; set; }

    public NewItemViewModel Item {
        
        get => (NewItemViewModel) BindingContext;
        set => BindingContext = value;
    }



    async void OnSaveClicked(object sender, EventArgs e) {

        if (string.IsNullOrWhiteSpace(Item.Name)) {

            await DisplayAlert("Name Required", "Please enter a name for the item.", "OK");
            return;
        }

        await Db.Tasks.AddAsync(new() {
            Name        = Item.Name,
            Description = Item.Description,
            Due         = Item.HasDueDate? Item.DueDate : null,

        });
        await Db.SaveChangesAsync();
        await Shell.Current.GoToAsync("..");
    }

    // async void OnDeleteClicked(object sender, EventArgs e) {
    //     if (Item.ID == 0) {
    //         return;
    //     }
    //     await database.DeleteItemAsync(Item);
    //     await Shell.Current.GoToAsync("..");
    // }

    async void OnCancelClicked(object sender, EventArgs e) {
        await Shell.Current.GoToAsync("..");
    }
}
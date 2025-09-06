using Microsoft.EntityFrameworkCore;
using Tk.Database;

namespace TK.Android;

public partial class MainPage : ContentPage {
    public MainPage(
        TkDbContext db
    ) {
        InitializeComponent();
        CounterBtn.Text = "Initialize db";
        ErrorLabel.Text = MauiProgram.Exception?.Message;
        this.db = db;
    }

    TkDbContext db { get; set; }

    private async void OnCounterClicked(object sender, EventArgs e) {

        CounterBtn.Text = "Initializing db";
        
        try {

            await db.Database.MigrateAsync();

            CounterBtn.Text = "Done";
        }
        catch (Exception ex) {
            CounterBtn.Text = "Err";
            ErrorLabel.Text = ex.Message;
        }



        SemanticScreenReader.Announce(CounterBtn.Text);
    }

}


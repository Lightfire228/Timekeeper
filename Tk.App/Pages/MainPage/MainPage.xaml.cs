namespace Tk.App.Pages.MainPage;

using MauiIcons.Core;
using MauiIcons.FontAwesome.Solid;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tk.Database;

using Logger = Microsoft.Extensions.Logging.ILogger<MainPage>;

public partial class MainPage : ContentPage {
    public MainPage(
        TkDbContext db,
        Logger      logger
    ) {
        InitializeComponent();
        CounterBtn.Text = "Initialize db";
        ErrorLabel.Text = MauiProgram.Exception?.Message;
        this.db     = db;
        this.logger = logger;

        logger.LogInformation("test");
    }

    private TkDbContext db     { get; set; }
    private Logger      logger { get; set; }

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


namespace Tk.App.Pages;

using MauiIcons.Core;
using MauiIcons.FontAwesome.Solid;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls;
using Tk.Database;
using Tk.Models;
using Logger = Microsoft.Extensions.Logging.ILogger<MainPage>;

public partial class MainPage : ContentPage {
    public MainPage(
        TkDbContext db,
        Logger      logger
    ) {
        InitializeComponent();
        this.db         = db;
        this.logger     = logger;

        logger.LogInformation("test");
    }

    private TkDbContext db     { get; set; }
    private Logger      logger { get; set; }

    private async void OnCounterClicked(object sender, EventArgs e) {

        SemanticScreenReader.Announce(CounterBtn.Text);
    }

}


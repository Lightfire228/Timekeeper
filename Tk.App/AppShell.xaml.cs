using MauiIcons.Core;
using Microsoft.EntityFrameworkCore;
using Tk.App.Pages;
using Tk.Database;
using Tk.Database.Migrations;
using Tk.Models;

namespace Tk.App;

public partial class AppShell : Shell {

    public AppShell(
        TkDbContext db
    ) {
        _db         = db;
        Task initDb = InitDb();
        
        InitializeComponent();
        Icons.InitializeIcons();
        RegisterRoutes();

        Task.WaitAll([initDb]);
    }

    TkDbContext _db { get; set; }

    private async Task InitDb() {
        await _db.Database.MigrateAsync();

        bool hasData = await _db.Tasks.AnyAsync();

        if (!hasData) {
            await _db.Tasks.AddRangeAsync(TestData.Tasks);
            await _db.SaveChangesAsync();
        }
    }

    private void RegisterRoutes() {
        Routing.RegisterRoute("NewItemPage", typeof(NewItemPage));
    }

}

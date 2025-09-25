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
        this.db = db;
        Task initDb = InitDb();
        
        InitializeComponent();
        Icons.InitializeIcons();
        RegisterRoutes();

        Task.WaitAll([initDb]);
    }

    TkDbContext db { get; set; }

    private async Task InitDb() {
        await db.Database.MigrateAsync();

        bool hasData = await db.Tasks.AnyAsync();

        if (!hasData) {
            await db.Tasks.AddRangeAsync(TestData.Tasks);
            await db.SaveChangesAsync();
        }
    }

    private void RegisterRoutes() {
        Routing.RegisterRoute("NewItemPage", typeof(NewItemPage));
    }

}

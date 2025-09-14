using MauiIcons.Core;
using Microsoft.EntityFrameworkCore;
using Tk.App.Pages;
using Tk.Database;
using Tk.Models;

namespace Tk.App;

public partial class AppShell : Shell {

    public AppShell(
        TkDbContext db
    ) {
        InitializeComponent();
        Icons.InitializeIcons();
        RegisterRoutes();
        this.db = db;
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

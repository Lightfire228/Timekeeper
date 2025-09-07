using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Sqlite;
using Tk.Database;
using Microsoft.EntityFrameworkCore;
using SQLite;
using Microsoft.Data.Sqlite;
using A = Android;
using MauiIcons.FontAwesome.Solid;
using MauiIcons.FontAwesome;

namespace Tk.App;

public static class MauiProgram {

    // TODO: use a proper logger
    public static Exception? Exception { get; private set; }

    public static MauiApp CreateMauiApp() {

        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
        ;

        builder.Services.AddDbContext<TkDbContext>(opt => {

            var dbFile = Path.Join(FileSystem.AppDataDirectory, "timekeeper.db");
            var connStr = $"Data Source={dbFile}";

            try {
                opt.UseSqlite(connStr);
            }
            catch (Exception e) {
                Exception = e;
                // Exception = new(dbFile);
            }
        });


        // Initialise the .Net Maui Icons - FontAwesome Solid
        builder.UseMauiApp<App>().UseFontAwesomeSolidMauiIcons();
        builder.UseMauiApp<App>().UseFontAwesomeMauiIcons();

        builder.Logging.AddDebug();

        return builder.Build();

    }
}

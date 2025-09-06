using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Sqlite;
using Tk.Database;
using Microsoft.EntityFrameworkCore;
using SQLite;
using Microsoft.Data.Sqlite;
using A = Android;

namespace TK.Android;

public static class MauiProgram {

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

        builder.Logging.AddDebug();

        return builder.Build();

    }
}

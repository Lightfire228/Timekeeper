using Tk.Database;
using Microsoft.EntityFrameworkCore;
using MauiIcons.FontAwesome.Solid;
using MauiIcons.FontAwesome;
using Serilog;
using Microsoft.Extensions.Logging;

namespace Tk.App;

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

        ConfigureLogging(builder);
        ConfigureDb     (builder);
        ConfigureIcons  (builder);

        return builder.Build();

    }

    public static void ConfigureLogging(MauiAppBuilder builder) {

        builder.Services.AddSerilog(opts => {
            opts.WriteTo.File(
                Path.Join(FileSystem.AppDataDirectory, "log.log")
            );
        });

        builder.Logging.AddDebug();
    }

    public static void ConfigureDb(MauiAppBuilder builder) {

        builder.Services.AddDbContext<TkDbContext>(opt => {

            var dbFile = Path.Join(FileSystem.AppDataDirectory, "timekeeper.db");

            opt.UseSqlite($"Data Source={dbFile}");
        });
    }

    public static void ConfigureIcons(MauiAppBuilder builder) {

        builder.UseMauiApp<App>().UseFontAwesomeSolidMauiIcons();
        builder.UseMauiApp<App>().UseFontAwesomeMauiIcons();
    }

}

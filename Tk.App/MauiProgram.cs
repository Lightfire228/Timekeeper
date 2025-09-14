using Tk.Database;
using Microsoft.EntityFrameworkCore;
using MauiIcons.FontAwesome.Solid;
using MauiIcons.FontAwesome;
using Serilog;
using Microsoft.Extensions.Logging;
using System.Reflection;
using Tk.App.Pages;

namespace Tk.App;

public static class MauiProgram {


    public static MauiApp CreateMauiApp() =>

        MauiApp.CreateBuilder()
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => {
                fonts.AddFont("OpenSans-Regular.ttf",  "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .ConfigureLogging  ()
            .ConfigureDb       ()
            .ConfigureIcons    ()
            .RegisterViews     ()
            .RegisterViewModels()
            .Build             ()
    ;

    public static MauiAppBuilder ConfigureLogging(this MauiAppBuilder builder) {

        builder.Services.AddSerilog(opts => {
            opts.WriteTo.File(
                Path.Join(FileSystem.AppDataDirectory, "log.log")
            );
        });

        builder.Logging.AddDebug();

        return builder;
    }

    public static MauiAppBuilder ConfigureDb(this MauiAppBuilder builder) {

        builder.Services.AddDbContext<TkDbContext>(opt => {

            var dbFile = Path.Join(FileSystem.AppDataDirectory, "timekeeper.db");

            opt.UseSqlite($"Data Source={dbFile}");
        });

        return builder;
    }

    public static MauiAppBuilder ConfigureIcons(this MauiAppBuilder builder) {

        builder.UseMauiApp<App>().UseFontAwesomeSolidMauiIcons();
        builder.UseMauiApp<App>().UseFontAwesomeMauiIcons();

        return builder;
    }

    public static MauiAppBuilder RegisterViews     (this MauiAppBuilder builder) => Register(builder, "Pages",      "Tk.App.Pages");
    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder) => Register(builder, "ViewMiodel", "Tk.App.ViewModels");

    static MauiAppBuilder Register(
        MauiAppBuilder builder,
        string         name,
        string         ns
    ) {
        Assembly
            .GetAssembly(typeof(MauiProgram))!
            .GetTypes()
            .Where(t => 
                   t.IsClass
                && t.Name.EndsWith(name)
                && (t.Namespace ?? "").StartsWith(ns)
            )
            .ToList()
            .ForEach(x => builder.Services.AddSingleton(x))
        ;

        return builder;
    }

}

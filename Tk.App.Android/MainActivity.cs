using Android.App;
using Android.OS;
using Android.Runtime;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Tk.App.Android;

using Logger = ILogger<MainActivity>;

[Activity(Label = "@string/app_name", MainLauncher = true)]
public class MainActivity
    : Tk.Android.Timekeeper.KMainActivity
{

    static MainActivity() {

        AppDomain.CurrentDomain.UnhandledException += (sender, e) => {
            Logger.LogError("Uncaught error on Main Activity: {e}", e.ExceptionObject);
        };
    }

    protected override void OnCreate(Bundle? savedInstanceState) {
        Logger.LogInformation("On Create");
        base.OnCreate(savedInstanceState);
    }

    static readonly Logger Logger = BuildLogger();

    static Logger BuildLogger() {
        var serilog = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("/data/data/Tk.App.Android.Develop/files/startup.log")
            .CreateLogger()
        ;

        var logger = new LoggerFactory()
            .AddSerilog(serilog)
            .CreateLogger<MainActivity>()
        ;

        logger.LogInformation("init");

        return logger;
    }
}
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Serilog;

using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Tk.App;

using Logger = ILogger<MainActivity>;


public class MainApplication
    : Application
{

    public const string AndroidDataPath = "/data/data/Tk.App.Develop/files";


    static MainApplication() {
        AppDomain.CurrentDomain.UnhandledException += (sender, e) => {
            Logger.LogError("Uncaught error in Application: {e}", e.ExceptionObject);
        };
    }

    static readonly ILogger Logger = BuildLogger();

    public static ILogger BuildLogger() {

        var serilog = new LoggerConfiguration()
            .WriteTo.AndroidLog()
            .CreateLogger()
        ;

        return new LoggerFactory()
            .AddSerilog(serilog)
            .CreateLogger<TimeKeeper>() 
        ;
    }

    public override void OnCreate() {
        Logger.LogInformation("App start");
        base.OnCreate();
    }

}


class TimeKeeper {}

using System.Runtime.ExceptionServices;
using Microsoft.Extensions.Logging;
using Tk.Database;

namespace Tk.App;

using Logger = ILogger<App>;

public partial class App : Application {
    public App(
        TkDbContext db,
        Logger      logger
    ) {
        InitializeComponent();
        Db     = db;
        Logger = logger;
        
        AppDomain.CurrentDomain.UnhandledException += LogException;
    }

    TkDbContext Db     { get; set; }
    Logger      Logger { get; set; }

    protected override Window CreateWindow(IActivationState? activationState) {
        return new(new AppShell(Db));
    }

    void LogException(object? sender, UnhandledExceptionEventArgs e) {
        Logger.LogError("Uncaught Exception in App");
        Logger.LogError("{ex}", e.ExceptionObject);
    }
}

using Android.App;
using Android.OS;
using Android.Runtime;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;
using Tk.Database;
using Tk.Database.Migrations;
using Tk.Models;
using Tk.Android.Timekeeper;
using Tk.Models.Database;

namespace Tk.App.Android;

using Logger = ILogger<MainActivity>;


[Activity(MainLauncher = true)]
public class MainActivity
    : KMainActivity
{

    private readonly DataService _ds;

    const string AndroidDataPath = "/data/data/Tk.App.Android.Develop/files";

    public override KDataService DataService { get => _ds; }

    public MainActivity() {
        _ds = new(Db);
    }

    private TkDbContext Db { get; set; } = new(
        new DbContextOptionsBuilder<TkDbContext>() {}
        .UseSqlite($"Data Source={AndroidDataPath}/timekeeper.db")
        .Options
    );


    

    static MainActivity() {

        AppDomain.CurrentDomain.UnhandledException += (sender, e) => {
            Logger.LogError("Uncaught error on Main Activity: {e}", e.ExceptionObject);
        };
    }

    protected override void OnCreate(Bundle? savedInstanceState) {
        Logger.LogInformation("On Create");

        var _ = InitDb();

        base.OnCreate(savedInstanceState);


    }

    static readonly Logger Logger = BuildLogger();

    static Logger BuildLogger() {
        var serilog = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File($"{AndroidDataPath}/startup.log")
            .CreateLogger()
        ;

        var logger = new LoggerFactory()
            .AddSerilog(serilog)
            .CreateLogger<MainActivity>()
        ;

        logger.LogInformation("init");

        return logger;
    }

    private async Task InitDb() {
        Logger.LogInformation("start init db");


        try {
            await Db.Database.MigrateAsync();

            bool hasData = await Db.Tasks.AnyAsync();


            if (!hasData) {
                await Db.Tasks.AddRangeAsync(TestData.Tasks);
                await Db.SaveChangesAsync();
            }

            Logger.LogInformation("finished init db");
        }
        catch (Exception e) {
            Logger.LogError("Error during db init: {e}", e);
            throw;
        }
    }
}

static class Extensions {
    public static int ToInt(this TaskPriority priority) => (int) priority;
}

public class DataService(TkDbContext db)
    : KDataService
{

    private readonly TkDbContext Db = db;


    public override int Icon { get => Resource.Drawable.appicon; }

    public override IList<KTaskModel> Tasks { get =>
        [..Db.Tasks
            .ToList()
            .Select(x => new KTaskModel(
                x.Id,
                x.Name,
                x.Description,
                x.Priority .ToInt(),
                x.Due     ?.ToString(),
                x.CreatedAt.ToString()
            ))
        ]
    ;}
}
using Android.App;
using Android.OS;
using Android.Runtime;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;
using Tk.Database;
using Tk.Database.Migrations;
using Tk.Models;
using Tk.Models.Database;
using AndroidX.Core.App;
using Tk.Android.Timekeeper;

namespace Tk.App;

using Logger = ILogger<MainActivity>;


[Activity(MainLauncher = true)]
public class MainActivity
    : KMainActivity
{

    private DataService                _ds                  = null!;
    private AndroidNotificationService _notificationService = null!;

    public const string AndroidDataPath = "/data/data/Tk.App.Develop/files";

    public override KDataService DataService { get => _ds; }

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

        Logger.LogInformation("ctx: {ctx}", ApplicationContext);

        _notificationService = new AndroidNotificationService(new() {
            CompatManager = NotificationManagerCompat.From(ApplicationContext)!,
            NotifManager  = (NotificationManager)ApplicationContext!.GetSystemService(NotificationService)!,
            MainActivity  = typeof(MainActivity),
            AppContext    = ApplicationContext,
            Logger        = BuildNotifLogger(),
            SmallIcon     = Resource.Drawable.appicon,
            LargeIcon     = Resource.Drawable.appicon,
        });

        _ds = new(Db, Logger, _notificationService!);
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

    static Microsoft.Extensions.Logging.ILogger BuildNotifLogger() {
        var serilog = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File($"{AndroidDataPath}/notifs.log")
            .CreateLogger()
        ;

        var logger = new LoggerFactory()
            .AddSerilog(serilog)
            .CreateLogger<AndroidNotificationService>()
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

    public static DateTime FromUnixTimestamp(this long unix) =>
        DateTime.UnixEpoch.AddSeconds(unix)
    ;

    public static long ToUnixTimestamp(this DateTime date) =>
        (date.Ticks - DateTime.UnixEpoch.Ticks) / TimeSpan.TicksPerSecond
    ;

    public static Java.Lang.Long ToJavaLong(this long val) =>
        Java.Lang.Long.ValueOf(val)
    ;

    public static DateTime TrimToSeconds(this DateTime date) =>
        new (
            date.Ticks - (date.Ticks % TimeSpan.TicksPerSecond),
            date.Kind
        )
    ;

}

public class DataService(TkDbContext db, Logger logger, INotificationService notificationService)
    : KDataService
{

    private readonly TkDbContext          Db           = db;
    private readonly Logger               Logger       = logger;
    private readonly INotificationService NotifService = notificationService;


    public override int Icon { get => Resource.Drawable.appicon; }

    public override IList<KTaskModel> Tasks { get =>
        [..Db.Tasks
            .ToList()
            .Select(x => new KTaskModel(
                x.Id,
                x.Name,
                x.Description,
                x.Priority .ToInt(),
                x.Due     ?.ToUnixTimestamp().ToJavaLong()
            ))
        ]
    ;}

    public override void OnNotificationButton(Java.Lang.Long? unixTimestamp) {

        Logger.LogInformation("Notification event: {unixTimestamp}", unixTimestamp);

        var date = unixTimestamp?.LongValue().FromUnixTimestamp();
        var now  = DateTime.UtcNow.TrimToSeconds();


        if (date == null) {
            Logger.LogInformation("Notif date is null");
            NotifService.SendNotification("Test title", "test message", NotificationChannelType.Default);
            return;
        }


        if (date < now) {
            var e = new Exception($"Notification date '{date}' cannot be greater than now '{now}'");
            Logger.LogError("{e}", e);
            throw e;
        }

        Logger.LogInformation("Seconds from now: {diff} -- date: {date} -- now {now}", (date - now)?.TotalSeconds, date, now);
        NotifService.SendNotification("Test alarm title", "test alarm message", NotificationChannelType.Default, date);

    }

    public override void OnNewTask(KTaskModel newTask) {
        Logger.LogInformation("On new task, name: {name}, desc: {desc}", newTask.Name, newTask.Description);
        
        Db.Tasks.Add(new() {
            Name        = newTask.Name,
            Description = newTask.Description,
            Priority    = (TaskPriority) newTask.Priority,
            Due         = newTask.Due?.LongValue().FromUnixTimestamp(),
        });
        Db.SaveChanges();
    }
}
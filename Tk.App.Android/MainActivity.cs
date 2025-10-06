using Android.App;
using Android.OS;

namespace Tk.App.Android;


[Activity(Label = "@string/app_name", MainLauncher = true)]
public class MainActivity 
    : Tk.Android.Timekeeper.MainActivityKt
{
    // protected override void OnCreate(Bundle? savedInstanceState) {
    //     base.OnCreate(savedInstanceState);

    //     // Console.WriteLine($">>>>>>>>>>>> {TestKt.TestString()}");

    //     // Set our view from the "main" layout resource
    //     SetContentView(Resource.Layout.activity_main);

    //     // var toast = Toast.MakeText(this, Tk.Kotlin.TestKt.TestString(), ToastLength.Long);
    //     // toast!.Show();
    // }
}
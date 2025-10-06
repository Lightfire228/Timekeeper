using Android.App;
using Android.OS;
using Android.Runtime;

namespace Tk.App.Android;


[Activity(Label = "@string/app_name", MainLauncher = true)]
public class MainActivity //_CS(IntPtr javaReference, JniHandleOwnership transfer) 
    // : Tk.Android.Timekeeper.MainActivity_fileKt.MainActivity(javaReference, transfer)
    : Activity
{

    protected override void OnCreate(Bundle? savedInstanceState) {
        base.OnCreate(savedInstanceState);

        var test = new Tk.Android.Timekeeper.Test();

        var b = new Tk.Android.Timekeeper.HiActivity2();
        var a = new Tk.Android.Timekeeper.HiActivity();

        

        // Console.WriteLine($">>>>>>>>>>>> {TestKt.TestString()}");

        // Set our view from the "main" layout resource
        SetContentView(Resource.Layout.activity_main);

        var toast = Toast.MakeText(this, test.TestActivity(), ToastLength.Long);
        toast!.Show();
    }
}
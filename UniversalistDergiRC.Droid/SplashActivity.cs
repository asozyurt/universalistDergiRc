
using Android.App;
using Android.Content.PM;
using Android.OS;

namespace UniversalistDergiRC.Droid
{
    [Activity(Theme = "@style/Theme.Splash", Label = "Universalist Dergi", Icon = "@drawable/icon", NoHistory = true, MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]

    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            System.Threading.Thread.Sleep(2000); //Let's wait awhile...
            this.StartActivity(typeof(MainActivity));
        }
    }
}


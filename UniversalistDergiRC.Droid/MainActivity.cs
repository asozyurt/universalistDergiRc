
using Android.App;
using Android.Content.PM;
using Android.OS;

namespace UniversalistDergiRC.Droid
{
    [Activity(ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]

    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        UniversalistDergiRC.App loadedApp;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            global::Xamarin.Forms.Forms.SetTitleBarVisibility(Xamarin.Forms.AndroidTitleBarVisibility.Never);
            loadedApp = new UniversalistDergiRC.App();
            LoadApplication(loadedApp);
        }

        public override void OnBackPressed()
        {
            if (loadedApp != null)
            {
                // Application decides to execute 'base.OnBackPressed()'
                bool executeMainMethod = loadedApp.DroidOnBackPressed();

                if (!executeMainMethod)
                    return;
            }
            base.OnBackPressed();
        }
    }
}


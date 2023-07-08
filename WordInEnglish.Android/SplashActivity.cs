using Android.App;
using Android.OS;

namespace WordInEnglishPro.Droid
{
    [Activity(Label = "Words In English Pro", Theme = "@style/SplashTheme", MainLauncher = true, NoHistory = true, ConfigurationChanges = Android.Content.PM.ConfigChanges.ScreenSize)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            StartActivity(typeof(MainActivity));
        }
    }
}
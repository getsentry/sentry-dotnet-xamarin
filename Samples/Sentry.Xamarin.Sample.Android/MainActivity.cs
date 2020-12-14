﻿
using Android;
using Android.App;
using Android.OS;
using Android.Runtime;
using Xamarin.Forms;
using Xamarin.Essentials;
using Sample.Xamarin.Core.Helpers;
using Sample.Xamarin.Core;
using Color = Android.Graphics.Color;
using Android.Content.PM;

namespace Sample.Xamarin.Droid
{
    [Activity(Label = "Sentry.Xamarin.Forms.Sample", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.UiMode)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            SentryInitializer.Init();

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Rg.Plugins.Popup.Popup.Init(this);

            Platform.Init(this, savedInstanceState);
            SetStatusBarColor(Color.Rgb(46, 14, 51));
            Forms.SetFlags("Shapes_Experimental");
            Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override void OnBackPressed()
        {
            if (!Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {
                base.OnBackPressed();
            }
        }
    }
}
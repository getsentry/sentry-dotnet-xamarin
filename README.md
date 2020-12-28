<p align="center">
  <a href="https://sentry.io" target="_blank" align="center">
    <img src="https://sentry-brand.storage.googleapis.com/sentry-logo-black.png" width="280">
  </a>
  <br />
</p>
 
Sentry SDK for Xamarin
===========

[![build](https://github.com/getsentry/sentry-dotnet-xamarin/workflows/build/badge.svg?branch=main)](https://github.com/getsentry/sentry-dotnet-xamarin/actions?query=branch%3Amain)

|      Integrations             |    Downloads     |    NuGet Stable     |    NuGet Preview     |
| ----------------------------- | :-------------------: | :-------------------: | :-------------------: |
|  **Sentry.Xamarin.Forms**     | [![Downloads](https://img.shields.io/nuget/dt/Sentry.Xamarin.Forms.svg)](https://www.nuget.org/packages/Sentry.Xamarin.Forms) | [![NuGet](https://img.shields.io/nuget/v/Sentry.Xamarin.Forms.svg)](https://www.nuget.org/packages/Sentry.Xamarin.Forms)   |    [![NuGet](https://img.shields.io/nuget/vpre/Sentry.Xamarin.Forms.svg)](https://www.nuget.org/packages/Sentry.Xamarin.Forms)   |

This is a work in progress SDK for Xamarin.

Includes for all Platforms supported by Xamarin Essentials:
* Automatic Navigation breacrumbs.
* Xaml warnings as breadcrumbs.
* Simulator flag.
* Device manufacturer.
* Device model.
* Operational system name and version.
* Screen information (Pixel density and resolution).
* Connectivity status.

Additionaly, Android and IOS will include additional information:
* Free Internal memory (Android/iOS).
* Total RAM (Android/iOS).
* CPU model (Android).
<p align="center">
  <b>BEFORE</b>
  
  <img src=".github/before_01.png"/>
</p>
<p align="center">
  <b>AFTER</b>
  
  <img src=".github/after_01.png"/>
</p>

## Setup
All you need to do is to initialize Xamarin integration by calling SentryXamarin.Init, and, it's recommended to start Sentry Xamarin SDK as early as possible, for an example, the start of OnCreate on MainActivity for Android, and, the top of FinishedLaunching on AppDelegate for iOS)

```C#
SentryXamarin.Init(options =>
{
    options.Dsn = "__YOUR__DSN__";
});

```

### Android
On your MainActivity
```C#
public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        SentryXamarin.Init(options =>
        {
            options.Dsn = "__YOUR__DSN__";
        });
        ...
```

### iOS
On AppDelegate.cs
```C#
public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
{
    public override bool FinishedLaunching(UIApplication app, NSDictionary options)
    {
        SentryXamarin.Init(options =>
        {
            options.Dsn = "__YOUR__DSN__";
        });
        ...
```

### UWP
On App.Xaml.cs
```C#
    sealed partial class App : Application
    {
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            SentryXamarin.Init(options =>
            {
                options.Dsn = "__YOUR__DSN__";
            });
        ...        
```

## Compatibility

The package requires the following versions or newer:

* Tizen 4.0 (for Tizen)
* Xamarin.Android 9.0 (for Android)
* Xamarin.iOS 10.14 (for iOS)
* Universal Windows Platform 10.0.16299 (for UWP)
* Xamarin.Forms 4.6.0.726
* Xamarin.Essentials 1.4.0
* Sentry 3.0.0


## Limitations

There are no line numbers on stack traces for UWP and in release builds for Android and iOS, furthermore, mono symbolication is not yet supported.

## Resources

* [![Documentation](https://img.shields.io/badge/documentation-sentry.io-green.svg)](https://docs.sentry.io/platforms/dotnet/)
* [![Forum](https://img.shields.io/badge/forum-sentry-green.svg)](https://forum.sentry.io/c/sdks)
* [![Discord](https://img.shields.io/discord/621778831602221064)](https://discord.gg/Ww9hbqr)
* [![Stack Overflow](https://img.shields.io/badge/stack%20overflow-sentry-green.svg)](http://stackoverflow.com/questions/tagged/sentry)
* [![Twitter Follow](https://img.shields.io/twitter/follow/getsentry?label=getsentry&style=social)](https://twitter.com/intent/follow?screen_name=getsentry)

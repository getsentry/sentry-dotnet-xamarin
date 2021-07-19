using System;
using Xamarin.Essentials;
using Sentry.Xamarin.Internals;
using Sentry.Internals.Session;
using Sentry.Internals.Device.Screenshot;

namespace Sentry
{
    /// <summary>
    /// Extend SentryXamarinOptions by allowing it to manipulate options from Sentry Xamarin.
    /// </summary>
    public  static partial class SentryXamarinOptionsExtensions
    {
        private static Lazy<string> _internalCacheDefaultPath = new Lazy<string>(()=> Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));

        /// <summary>
        /// Disables the automatic Xamarin warning crumbs.
        /// </summary>
        /// <param name="options">The Sentry options.</param>
        public static void DisableXamarinWarningsBreadcrumbs(this SentryXamarinOptions options)
        {
            options.XamarinLoggerEnabled = false;
        }

        /// <summary>
        /// Disables the Sentry Xamarin Native integration.
        /// </summary>
        /// <param name="options">The Sentry options.</param>
        public static void DisableNativeIntegration(this SentryXamarinOptions options)
        {
            options.NativeIntegrationEnabled = false;
        }

        /// <summary>
        /// Disables the cache, must be called before Sentry gets initialized
        /// </summary>
        /// <param name="options">The Sentry options.</param>
        public static void DisableOfflineCaching(this SentryXamarinOptions options)
        {
            options.InternalCacheEnabled = false;
        }

        internal static string DefaultCacheDirectoyPath(this SentryXamarinOptions _)
            => _internalCacheDefaultPath.Value;

        internal static void ConfigureSentryXamarinOptions(this SentryXamarinOptions options)
        {
            options.Release ??= $"{AppInfo.PackageName}@{AppInfo.VersionString}+{AppInfo.BuildString}";
            if (options.InternalCacheEnabled)
            {
                options.CacheDirectoryPath ??= options.DefaultCacheDirectoyPath();
            }
        }

        internal static void RegisterXamarinEventProcessors(this SentryXamarinOptions options)
        {
            options.AddEventProcessor(new XamarinEventProcessor(options));

#if NATIVE_PROCESSOR
            options.AddEventProcessor(new NativeEventProcessor(options));
#else
            options.DiagnosticLogger?.Log(SentryLevel.Debug, "No NativeEventProcessor found for the given target.");
#endif
        }

        internal static void RegisterScreenshotEventProcessor(this SentryXamarinOptions options)
        {
#if NATIVE_PROCESSOR
            if (options.AttachScreenshots)
            {
                options.AddEventProcessor(new ScreenshotEventProcessor(options));
            }
#endif
        }

        internal static bool RegisterNativeIntegrations(this SentryXamarinOptions options)
        {
            if (options.NativeIntegrationEnabled)
            {
#if NATIVE_PROCESSOR
                var nativeintegration = new NativeIntegration(options);
                options.AddIntegration(nativeintegration);
                return true;
#else
                options.DiagnosticLogger?.Log(SentryLevel.Debug, "No NativeIntegration found for the given target.");
#endif
            }
            return false;
        }

        internal static bool RegisterNativeActivityStatus(this SentryXamarinOptions options)
        {
#if NATIVE_PROCESSOR
            if (options.AutoSessionTracking)
            {
                options.SessionLogger = new DeviceActiveLogger();
                return true;
            }
#endif
            return false;
        }

        internal static void AddPageNavigationTrackerIntegration(this SentryXamarinOptions options, IPageNavigationTracker tracker)
        {
            tracker.RegisterXamarinOptions(options);
            options.PageTracker = tracker;
            options.AddIntegration(tracker);
        }

        internal static void RegisterXamarinInAppExclude(this SentryXamarinOptions options)
        {
            options.AddInAppExclude("Java.");
            options.AddInAppExclude("ZXing");
            options.AddInAppExclude("TouchView");
            options.AddInAppExclude("Rg.Plugins");
            options.AddInAppExclude("Prism");
            options.AddInAppExclude("CarouselView.FormsPlugin");
            options.AddInAppExclude("CardsView");
            options.AddInAppExclude("MvvmCross.Forms");
            options.AddInAppExclude("Com.Airbnb.Xamarin.Forms.Lottie");
            options.AddInAppExclude("Com.OneSignal");
            options.AddInAppExclude("SQLite-net");
        }
    }
}

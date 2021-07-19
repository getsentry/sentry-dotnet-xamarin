using Sentry.Internals.Session;
using Sentry.Xamarin.Internals;

namespace Sentry
{
    /// <summary>
    /// Sentry Xamarin SDK options.
    /// </summary>
    public class SentryXamarinOptions : SentryOptions
    {
        /// <summary>
        /// Attaches screenshots from the app to events automatically whenever possible.
        /// </summary>
        /// <remarks>
        /// Pii information might be exposed by activating this feature.
        /// </remarks>
        public bool AttachScreenshots { get; set; }

        //TODO: Maybe an option to choose between png and jpeg where jpeg is the default?
        //public SentryScreenshotQuality ScreenshotQuality { get; set; }

        /// <summary>
        /// Define the range of time that duplicated internal breadcrumbs will be ignored.
        /// </summary>
        public int InternalBreadcrumbDuplicationTimeSpan { get; set; } = 2;


        internal bool XamarinLoggerEnabled { get; set; } = true;
        internal bool NativeIntegrationEnabled { get; set; } = true;
        internal bool InternalCacheEnabled { get; set; } = true;
        internal IPageNavigationTracker PageTracker { get; set; }
        internal string ProtocolPackageName { get; set; }
        internal string ProjectName { get; set; }
        internal int GetCurrentApplicationDelay { get; set; } = 500;
        internal int GetCurrentApplicationMaxRetries { get; set; } = 15;

        /// <summary>
        /// Redirects session callbacks to SentrySdk based on
        /// device's app status.
        /// </summary>
        internal IDeviceActiveLogger SessionLogger { get; set; }

        internal Breadcrumb LastInternalBreadcrumb {get;set;}

        /// <summary>
        /// The Sentry Xamarin Options.
        /// </summary>
        public SentryXamarinOptions()
        {
            IsEnvironmentUser = false;
            AutoSessionTracking = true;
            IsGlobalModeEnabled = true;
        }
    }
}

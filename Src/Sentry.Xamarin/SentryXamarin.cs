using System;

namespace Sentry
{
    /// <summary>
    /// Sentry Xamarin SDK entrypoint.
    /// </summary>
    /// <remarks>
    /// This is a facade to the SDK instance that also Initializes Sentry .NET SDK.
    /// use SentrySdk for additional operations (like capturing exceptions, messages,...).
    /// </remarks>
    public static class SentryXamarin
    {

        internal static readonly string ProtocolPackageName = "sentry.dotnet.xamarin";

        /// <summary>
        /// Initializes the SDK with an optional configuration options callback.
        /// </summary>
        /// <param name="configureOptions">The configure options.</param>
        public static void Init(Action<SentryXamarinOptions> configureOptions)
        {
            var options = new SentryXamarinOptions();
            configureOptions?.Invoke(options);
            Init(options);
        }

        /// <summary>
        /// Initializes the SDK with the specified options instance.
        /// </summary>
        /// <param name="options">The options instance</param>
        public static void Init(SentryXamarinOptions options)
        {
            options ??= new SentryXamarinOptions();

            options.ConfigureSentryXamarinOptions();
            options.RegisterNativeActivityStatus();
            options.RegisterNativeIntegrations();
            options.RegisterXamarinEventProcessors();
            options.RegisterScreenshotEventProcessor();
            options.RegisterXamarinInAppExclude();
            options.ProtocolPackageName ??= ProtocolPackageName;
            SentrySdk.Init(options);
        }
    }
}

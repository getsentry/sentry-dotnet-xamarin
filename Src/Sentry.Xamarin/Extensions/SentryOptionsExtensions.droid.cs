namespace Sentry.Extensions
{
    internal static class SentryOptionsExtensions
    {
        public static void ApplyToSentryAndroidOptions(this SentryOptions options, IO.Sentry.Android.Core.SentryAndroidOptions javaOptions)
        {
            javaOptions.Environment = options.Environment;
            javaOptions.Release = options.Release;
            javaOptions.Dsn = options.Dsn;
            javaOptions.DiagnosticLevel = (IO.Sentry.Map.SentryLevel)options.DiagnosticLevel;
            javaOptions.CacheDirPath = options.CacheDirectoryPath;
            javaOptions.SendDefaultPii = options.SendDefaultPii;

            javaOptions.AnrReportInDebug = options.Debug;
            javaOptions.SetDebug(Java.Lang.Boolean.ValueOf(options.Debug));
        }
    }
}

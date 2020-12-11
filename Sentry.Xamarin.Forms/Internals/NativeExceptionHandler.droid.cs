﻿using Android.Runtime;
using Sentry.Protocol;
using System;
using System.Linq;

namespace Sentry.Xamarin.Forms.Internals
{
    internal partial class NativeExceptionHandler
    {
        internal NativeExceptionHandler()
        {
            AndroidEnvironment.UnhandledExceptionRaiser += (sender, args) =>
            {
                var exceptionEvent = new SentryEvent(args.Exception);
                exceptionEvent.SentryExceptions.First().Mechanism = new Mechanism()
                {
                    Handled = false,
                    Type = "AndroidEnvironment_UnhandledExceptionRaiser"
                };
                SentrySdk.CaptureEvent(exceptionEvent);
                SentrySdk.FlushAsync(TimeSpan.FromSeconds(10)).Wait();
            };

        }
    }
}

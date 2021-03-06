﻿using Sentry.Extensibility;
using System;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.System.Profile;
using UwpPackage = Windows.ApplicationModel.Package;

namespace Sentry.Xamarin.Internals
{
    internal  class NativeEventProcessor : ISentryEventProcessor
    {
        private Lazy<UwpContext> _uwpContext = new Lazy<UwpContext>(() => new UwpContext());
        private volatile bool _uwpContextLoaded = true;

        private SentryOptions _options;

        public NativeEventProcessor(SentryOptions options) => _options = options;

        private class UwpContext
        {
            internal string DeviceFamily { get; }
            internal string DeviceFriendlyName { get; }
            internal string OsName { get; }
            internal string OsVersion { get; }
            internal string OsArchitecture { get; }

            internal UwpContext()
            {
                DeviceFamily = AnalyticsInfo.VersionInfo.DeviceFamily;

                var version = ulong.Parse(AnalyticsInfo.VersionInfo.DeviceFamilyVersion);
                var major = (version & 0xFFFF000000000000L) >> 48;
                var minor = (version & 0x0000FFFF00000000L) >> 32;
                var build = (version & 0x00000000FFFF0000L) >> 16;
                var revision = (version & 0x000000000000FFFFL);
                OsVersion = $"{major}.{minor}.{build}.{revision}";

                OsArchitecture = UwpPackage.Current.Id.Architecture.ToString();
                var deviceInfo = new EasClientDeviceInformation();
                OsName = char.ToUpper(deviceInfo.OperatingSystem[0]) + 
                    deviceInfo.OperatingSystem.Remove(0,1).ToLower();
                DeviceFriendlyName = deviceInfo.FriendlyName;
            }
        }

        public SentryEvent Process(SentryEvent @event)
        {
            if (_uwpContextLoaded)
            {
                try
                {
                    var uwpContext = _uwpContext.Value;
                    @event.Contexts.Device.Family = uwpContext.DeviceFamily;
                    @event.Contexts.Device.Name = uwpContext.DeviceFriendlyName;
                    @event.Contexts.OperatingSystem.Name = uwpContext.OsName;
                    @event.Contexts.OperatingSystem.Version = uwpContext.OsVersion;
                }
                catch (Exception ex)
                {
                    _options.DiagnosticLogger?.Log(SentryLevel.Error, "Failed to add UwpPlatformEventProcessor into event.", ex);
                    //In case of any failure, this process function will be disabled to avoid throwing exceptions for future events.
                    _uwpContextLoaded = false;
                }
            }
            else
            {
                _options.DiagnosticLogger?.Log(SentryLevel.Debug, "UwpPlatformEventProcessor disabled due to previous error.");
            }
            return @event;
        }
    }
}
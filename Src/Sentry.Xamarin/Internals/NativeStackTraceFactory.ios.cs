﻿using Sentry.Extensibility;
using Sentry.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Sentry.Xamarin.Internals
{
    internal class NativeStackTraceFactory
    {
        private static string _nativeRegexFormat => "(?<id>\\d+)\\s+(?<method>[a-zA-Z\\.-_?]+)\\s+(?<offset>0x[0-9a-fA-F]+)\\s+(?<function>.+?(?=\\s\\+))\\s+\\+\\s+(?<line>\\d+)";
        //"(?<id>\d+)\s+(?<method>[a-zA-Z\.-?]+)\s+(?<offset>0x[0-9a-fA-F]+)\s+(?<function>.+?(?=\s\+))\s+\+\s+(?<line>\d+)"
        private readonly SentryXamarinOptions _options;

        public NativeStackTraceFactory(SentryXamarinOptions options) => _options = options;

        internal bool IsNativeException(string exceptionValue)
            => exceptionValue.StartsWith("Objective-C exception");

        internal SentryStackTrace CreateNativeStackTrace(SentryStackTrace managedStackTrace, IEnumerable<string> nativeStackTrace)
        {
            var nativeFramesList = new List<SentryStackFrame>();
            for (int i = nativeStackTrace.Count() - 1; i >= 0; i--)
            {
                var match = Regex.Match(nativeStackTrace.ElementAt(i), _nativeRegexFormat);
                if (match.Success)
                {
                    var method = match.Groups["method"].Value;
                    nativeFramesList.Add(new SentryStackFrame()
                    {
                        Platform = "cocoa",
                        Function = match.Groups["function"].Value,
                        Package = method,
                        InstructionAddress = match.Groups["offset"].Value,
                        InApp = method == _options.ProjectName
                    });
                }
            }
            managedStackTrace.Frames = managedStackTrace.Frames.Concat(nativeFramesList).ToList();
            return managedStackTrace;
        }
    }
}
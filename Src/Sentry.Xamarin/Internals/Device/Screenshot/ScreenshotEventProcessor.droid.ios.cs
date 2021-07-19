using Sentry.Extensibility;
using Xamarin.Essentials;
using System.IO;
using System;

namespace Sentry.Internals.Device.Screenshot
{
    internal class ScreenshotEventProcessor : ISentryEventProcessor
    {
        private ScreenshotAttachmentContent? _screenshot { get; set; }
        private SentryXamarinOptions _options { get; }

        public ScreenshotEventProcessor(SentryXamarinOptions options) => _options = options;

        public SentryEvent? Process(SentryEvent @event)
        {
            try
            {
                if (_options.SessionLogger?.IsBackground() == false)
                {
                    var stream = Capture();
                    if (stream != null)
                    {
                        /* Create a new attachment if no screenshot attachment was fond.
                         * If there's an attachment content but it wasnt read during the last event processing
                         * Assume it was cleared and create a new one.
                         */
                        if(_screenshot?.GetWasRead() != true)
                        {
                            _screenshot = new ScreenshotAttachmentContent(stream);
                            SentrySdk.ConfigureScope(s => s.AddAttachment(new ScreenshotAttachment(_screenshot)));
                        }
                        else 
                        {
                            _screenshot.SetNewData(stream);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _options.DiagnosticLogger?.LogError("Failed to capture a screenshot", ex);
            }
            return @event;
        }

        internal Stream Capture()
        {
            var screenStream = global::Xamarin.Essentials.Screenshot.CaptureAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            return screenStream.OpenReadAsync(ScreenshotFormat.Jpeg).ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}

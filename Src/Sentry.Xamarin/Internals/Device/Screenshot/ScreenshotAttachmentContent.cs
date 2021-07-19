using System.IO;

namespace Sentry.Internals.Device.Screenshot
{
    internal class ScreenshotAttachmentContent : IAttachmentContent
    {
        private byte[] _data { get; set; }

        private bool _wasRead{ get; set; }

        /// <summary>
        /// The content ctor
        /// </summary>
        /// <param name="stream">A Ready once stream containing the image data.</param>
        public ScreenshotAttachmentContent(Stream stream)
        {
            SetNewData(stream);
        }

        public void SetNewData(Stream stream)
        {
            _data = new byte[stream.Length];
            stream.Read(_data, 0, _data.Length);
        }

        /// <summary>
        /// Inform is the contenet was previously read.
        /// By calling this function you will reset the read state to false and return the current state.
        /// </summary>
        /// <returns>true if the content was read.</returns>
        public bool GetWasRead()
        {
            var read = _wasRead;
            _wasRead = false;
            return read;
        }

        /// <summary>
        /// Get a stream from the attachment data.
        /// </summary>
        /// <returns>A Memory stream containing the data.</returns>
        public Stream GetStream()
        {
            _wasRead = true;
            return new MemoryStream(_data);
        }
    }
}
namespace Sentry.Internals.Session
{
    internal class DeviceActiveLogger : IDeviceActiveLogger
    {
        private bool _isPaused { get; set; } = true;

        /// <summary>
        /// Informs if the device is on background.
        /// </summary>
        /// <returns>True if the app is on background, otherwise false</returns>
        public bool IsBackground() => _isPaused;

        /// <inheritdoc cref="IHub.PauseSession"/>
        public void StatePaused()
        {
            _isPaused = true;
            SentrySdk.PauseSession();
        }

        /// <inheritdoc cref="IHub.ResumeSession"/>
        public void StateResumed()
        {
            _isPaused = false;
            SentrySdk.ResumeSession();
        }
    }
}

namespace Sentry.Internals.Session
{
    internal class DeviceActiveLogger : IDeviceActiveLogger
    {
        private bool FirstResumed { get; set; }

        /// <summary>
        /// IsPaused is used to track if the app is currently in background
        /// </summary>
        private bool IsPaused;

        /// <summary>
        /// Informs if the device is on background.
        /// </summary>
        /// <returns>True if the app is on background, otherwise false</returns>
        public bool IsBackground() => IsPaused;


        /// <inheritdoc cref="IHub.PauseSession"/>
        public void StatePaused()
        {
            if (!IsPaused)
            {
                IsPaused = true;
                SentrySdk.PauseSession();
            }
        }

        /// <inheritdoc cref="IHub.ResumeSession"/>
        public void StateResumed()
        {
            // When the app is launched, the Resumed state is triggered.
            // We don't want to register a Resume session when the app initiates.
            if (!FirstResumed)
            {
                FirstResumed = true;
            }
            // Allow the resume setup only if the previous state was paused.
            // Otherwise, we'll ignore the resume session since it's already resumed.
            else
            {
                IsPaused = false;
                SentrySdk.ResumeSession();
            }
        }
    }
}

namespace Sentry.Internals.Session
{
    internal interface IDeviceActiveLogger
    {
        void StatePaused();

        void StateResumed();

        bool IsBackground();
    }
}

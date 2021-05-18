using System;
using static IO.Sentry.Sentry;

namespace Sentry.Internals
{
    internal class Runnable<T> : Java.Lang.Object, IOptionsConfiguration where T : Java.Lang.Object
    {
        private Action<T> _onInvoked;
        public Runnable(Action<T> onInvoked) => _onInvoked = onInvoked;
        public void CallBack(Action<T> onInvoked) => _onInvoked = onInvoked;

        public void Configure(Java.Lang.Object options)
        {
            try
            {
                _onInvoked?.Invoke((T)options);
            }
            catch (Exception ex)
            {
                _ = ex;
            }
        }
    }
}

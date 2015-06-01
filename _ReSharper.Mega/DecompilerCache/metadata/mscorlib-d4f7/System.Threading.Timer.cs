// Type: System.Threading.Timer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Threading
{
    [ComVisible(true)]
    public sealed class Timer : MarshalByRefObject, IDisposable
    {
        [SecuritySafeCritical]
        public Timer(TimerCallback callback, object state, int dueTime, int period);

        [SecuritySafeCritical]
        public Timer(TimerCallback callback, object state, TimeSpan dueTime, TimeSpan period);

        [CLSCompliant(false)]
        [SecuritySafeCritical]
        public Timer(TimerCallback callback, object state, uint dueTime, uint period);

        [SecuritySafeCritical]
        public Timer(TimerCallback callback, object state, long dueTime, long period);

        [SecuritySafeCritical]
        public Timer(TimerCallback callback);

        #region IDisposable Members

        public void Dispose();

        #endregion

        [SecuritySafeCritical]
        public bool Change(int dueTime, int period);

        public bool Change(TimeSpan dueTime, TimeSpan period);

        [SecuritySafeCritical]
        [CLSCompliant(false)]
        public bool Change(uint dueTime, uint period);

        [SecuritySafeCritical]
        public bool Change(long dueTime, long period);

        [SecuritySafeCritical]
        public bool Dispose(WaitHandle notifyObject);
    }
}

// Type: System.Threading.Thread
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System;
using System.Globalization;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Security;
using System.Security.Principal;

namespace System.Threading
{
    [ClassInterface(ClassInterfaceType.None)]
    [ComDefaultInterface(typeof (_Thread))]
    [ComVisible(true)]
    public sealed class Thread : CriticalFinalizerObject, _Thread
    {
        [SecuritySafeCritical]
        public Thread(ThreadStart start);

        [SecuritySafeCritical]
        public Thread(ThreadStart start, int maxStackSize);

        [SecuritySafeCritical]
        public Thread(ParameterizedThreadStart start);

        [SecuritySafeCritical]
        public Thread(ParameterizedThreadStart start, int maxStackSize);

        public int ManagedThreadId { [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success), SecuritySafeCritical]
        get; }

        public ExecutionContext ExecutionContext { [SecuritySafeCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        get; }

        public ThreadPriority Priority { [SecuritySafeCritical]
        get; [SecuritySafeCritical]
        set; }

        public bool IsAlive { [SecuritySafeCritical]
        get; }

        public bool IsThreadPoolThread { [SecuritySafeCritical]
        get; }

        public static Thread CurrentThread { [SecuritySafeCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail),
                                              TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        get; }

        public bool IsBackground { [SecuritySafeCritical]
        get; [SecuritySafeCritical]
        set; }

        public ThreadState ThreadState { [SecuritySafeCritical]
        get; }

        [Obsolete(
            "The ApartmentState property has been deprecated.  Use GetApartmentState, SetApartmentState or TrySetApartmentState instead."
            , false)]
        public ApartmentState ApartmentState { [SecuritySafeCritical]
        get; [SecuritySafeCritical]
        set; }

        public CultureInfo CurrentUICulture { [SecuritySafeCritical]
        get; [SecuritySafeCritical]
        set; }

        public CultureInfo CurrentCulture { [SecuritySafeCritical]
        get; [SecuritySafeCritical]
        set; }

        public static Context CurrentContext { [SecurityCritical]
        get; }

        public static IPrincipal CurrentPrincipal { [SecuritySafeCritical]
        get; [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries"), SecuritySafeCritical
             ]
        set; }

        public string Name { get; [SecuritySafeCritical]
        set; }

        #region _Thread Members

        void _Thread.GetTypeInfoCount(out uint pcTInfo);
        void _Thread.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);
        void _Thread.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

        void _Thread.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams,
                            IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);

        #endregion

        [ComVisible(false)]
        public override int GetHashCode();

        [SecuritySafeCritical]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void Start();

        [SecuritySafeCritical]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void Start(object parameter);

        [Obsolete(
            "Thread.SetCompressedStack is no longer supported. Please use the System.Threading.CompressedStack class")]
        [SecurityCritical]
        public void SetCompressedStack(CompressedStack stack);

        [SecurityCritical]
        [Obsolete(
            "Thread.GetCompressedStack is no longer supported. Please use the System.Threading.CompressedStack class")]
        public CompressedStack GetCompressedStack();

        [SecuritySafeCritical]
        public void Abort(object stateInfo);

        [SecuritySafeCritical]
        public void Abort();

        [SecuritySafeCritical]
        public static void ResetAbort();

        [SecuritySafeCritical]
        [Obsolete(
            "Thread.Suspend has been deprecated.  Please use other classes in System.Threading, such as Monitor, Mutex, Event, and Semaphore, to synchronize Threads or protect resources.  http://go.microsoft.com/fwlink/?linkid=14202"
            , false)]
        public void Suspend();

        [SecuritySafeCritical]
        [Obsolete(
            "Thread.Resume has been deprecated.  Please use other classes in System.Threading, such as Monitor, Mutex, Event, and Semaphore, to synchronize Threads or protect resources.  http://go.microsoft.com/fwlink/?linkid=14202"
            , false)]
        public void Resume();

        [SecuritySafeCritical]
        public void Interrupt();

        [SecuritySafeCritical]
        public void Join();

        [SecuritySafeCritical]
        public bool Join(int millisecondsTimeout);

        public bool Join(TimeSpan timeout);

        [SecuritySafeCritical]
        public static void Sleep(int millisecondsTimeout);

        public static void Sleep(TimeSpan timeout);

        [SecuritySafeCritical]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        public static void SpinWait(int iterations);

        [SecuritySafeCritical]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        public static bool Yield();

        [SecuritySafeCritical]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        ~Thread();

        [SecurityCritical]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [MethodImpl(MethodImplOptions.InternalCall)]
        public void DisableComObjectEagerCleanup();

        [SecuritySafeCritical]
        public ApartmentState GetApartmentState();

        [SecuritySafeCritical]
        public bool TrySetApartmentState(ApartmentState state);

        [SecuritySafeCritical]
        public void SetApartmentState(ApartmentState state);

        public static LocalDataStoreSlot AllocateDataSlot();
        public static LocalDataStoreSlot AllocateNamedDataSlot(string name);
        public static LocalDataStoreSlot GetNamedDataSlot(string name);
        public static void FreeNamedDataSlot(string name);
        public static object GetData(LocalDataStoreSlot slot);
        public static void SetData(LocalDataStoreSlot slot, object data);

        [SecuritySafeCritical]
        public static AppDomain GetDomain();

        [SecuritySafeCritical]
        public static int GetDomainID();

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        [SecuritySafeCritical]
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static void BeginCriticalRegion();

        [SecuritySafeCritical]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static void EndCriticalRegion();

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        [SecurityCritical]
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static void BeginThreadAffinity();

        [SecurityCritical]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static void EndThreadAffinity();

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static byte VolatileRead(ref byte address);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static short VolatileRead(ref short address);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static int VolatileRead(ref int address);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static long VolatileRead(ref long address);

        [CLSCompliant(false)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static sbyte VolatileRead(ref sbyte address);

        [CLSCompliant(false)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static ushort VolatileRead(ref ushort address);

        [CLSCompliant(false)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static uint VolatileRead(ref uint address);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static IntPtr VolatileRead(ref IntPtr address);

        [CLSCompliant(false)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static UIntPtr VolatileRead(ref UIntPtr address);

        [CLSCompliant(false)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static ulong VolatileRead(ref ulong address);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static float VolatileRead(ref float address);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static double VolatileRead(ref double address);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static object VolatileRead(ref object address);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void VolatileWrite(ref byte address, byte value);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void VolatileWrite(ref short address, short value);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void VolatileWrite(ref int address, int value);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void VolatileWrite(ref long address, long value);

        [CLSCompliant(false)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void VolatileWrite(ref sbyte address, sbyte value);

        [CLSCompliant(false)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void VolatileWrite(ref ushort address, ushort value);

        [CLSCompliant(false)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void VolatileWrite(ref uint address, uint value);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void VolatileWrite(ref IntPtr address, IntPtr value);

        [CLSCompliant(false)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void VolatileWrite(ref UIntPtr address, UIntPtr value);

        [CLSCompliant(false)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void VolatileWrite(ref ulong address, ulong value);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void VolatileWrite(ref float address, float value);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void VolatileWrite(ref double address, double value);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void VolatileWrite(ref object address, object value);

        [SecuritySafeCritical]
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static void MemoryBarrier();
    }
}

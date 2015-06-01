// Type: System.ServiceProcess.ServiceBase
// Assembly: System.ServiceProcess, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Assembly location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\System.ServiceProcess.dll

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace System.ServiceProcess
{
    [InstallerType(typeof (ServiceProcessInstaller))]
    public class ServiceBase : Component
    {
        public const int MaxNameLength = 80;

        [ServiceProcessDescription("SBAutoLog")]
        [DefaultValue(true)]
        public bool AutoLog { get; set; }

        [ComVisible(false)]
        public int ExitCode { get; set; }

        [DefaultValue(false)]
        public bool CanHandlePowerEvent { get; set; }

        [ComVisible(false)]
        [DefaultValue(false)]
        public bool CanHandleSessionChangeEvent { get; set; }

        [DefaultValue(false)]
        public bool CanPauseAndContinue { get; set; }

        [DefaultValue(false)]
        public bool CanShutdown { get; set; }

        [DefaultValue(true)]
        public bool CanStop { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual EventLog EventLog { get; }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected IntPtr ServiceHandle { get; }

        [TypeConverter(
            "System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
            )]
        [ServiceProcessDescription("SBServiceName")]
        public string ServiceName { get; set; }

        [ComVisible(false)]
        public void RequestAdditionalTime(int milliseconds);

        protected override void Dispose(bool disposing);
        protected virtual void OnContinue();
        protected virtual void OnPause();
        protected virtual bool OnPowerEvent(PowerBroadcastStatus powerStatus);
        protected virtual void OnSessionChange(SessionChangeDescription changeDescription);
        protected virtual void OnShutdown();
        protected virtual void OnStart(string[] args);
        protected virtual void OnStop();
        protected virtual void OnCustomCommand(int command);
        public static void Run(ServiceBase[] services);
        public static void Run(ServiceBase service);
        public void Stop();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [ComVisible(false)]
        public void ServiceMainCallback(int argCount, IntPtr argPointer);
    }
}

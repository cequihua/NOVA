// Type: System.ServiceProcess.ServiceController
// Assembly: System.ServiceProcess, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Assembly location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\System.ServiceProcess.dll

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.ServiceProcess.Design;

namespace System.ServiceProcess
{
    [ServiceProcessDescription("ServiceControllerDesc")]
    [Designer(
        "System.ServiceProcess.Design.ServiceControllerDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
        )]
    public class ServiceController : Component
    {
        public ServiceController();
        public ServiceController(string name);
        public ServiceController(string name, string machineName);

        [ServiceProcessDescription("SPCanPauseAndContinue")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CanPauseAndContinue { get; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [ServiceProcessDescription("SPCanShutdown")]
        public bool CanShutdown { get; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [ServiceProcessDescription("SPCanStop")]
        public bool CanStop { get; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [ServiceProcessDescription("SPDisplayName")]
        [ReadOnly(true)]
        public string DisplayName { get; set; }

        [ServiceProcessDescription("SPDependentServices")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ServiceController[] DependentServices { get; }

        [SettingsBindable(true)]
        [Browsable(false)]
        [ServiceProcessDescription("SPMachineName")]
        [DefaultValue(".")]
        public string MachineName { get; set; }

        [ReadOnly(true)]
        [TypeConverter(typeof (ServiceNameConverter))]
        [SettingsBindable(true)]
        [ServiceProcessDescription("SPServiceName")]
        [DefaultValue("")]
        public string ServiceName { get; set; }

        [ServiceProcessDescription("SPServicesDependedOn")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ServiceController[] ServicesDependedOn { get; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public SafeHandle ServiceHandle { get; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [ServiceProcessDescription("SPStatus")]
        public ServiceControllerStatus Status { get; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [ServiceProcessDescription("SPServiceType")]
        public ServiceType ServiceType { get; }

        public void Close();
        protected override void Dispose(bool disposing);
        public static ServiceController[] GetDevices();
        public static ServiceController[] GetDevices(string machineName);
        public static ServiceController[] GetServices();
        public static ServiceController[] GetServices(string machineName);
        public void Pause();
        public void Continue();
        public void ExecuteCommand(int command);
        public void Refresh();
        public void Start();
        public void Start(string[] args);
        public void Stop();
        public void WaitForStatus(ServiceControllerStatus desiredStatus);
        public void WaitForStatus(ServiceControllerStatus desiredStatus, TimeSpan timeout);
    }
}

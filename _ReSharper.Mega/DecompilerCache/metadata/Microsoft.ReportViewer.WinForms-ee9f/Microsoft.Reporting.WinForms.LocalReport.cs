// Type: Microsoft.Reporting.WinForms.LocalReport
// Assembly: Microsoft.ReportViewer.WinForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Assembly location: C:\Program Files (x86)\Microsoft Visual Studio 10.0\ReportViewer\Microsoft.ReportViewer.WinForms.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Policy;

namespace Microsoft.Reporting.WinForms
{
    [Serializable]
    public sealed class LocalReport : Report, ISerializable, IDisposable
    {
        public LocalReport();

        [NotifyParentProperty(true)]
        [DefaultValue(null)]
        public string ReportPath { get; set; }

        [TypeConverter(
            "Microsoft.ReportingServices.ReportSelectionConverter, Microsoft.Reporting.Design, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
            )]
        [NotifyParentProperty(true)]
        [DefaultValue(null)]
        public string ReportEmbeddedResource { get; set; }

        [NotifyParentProperty(true)]
        [DefaultValue(false)]
        [Category("Security")]
        public bool EnableExternalImages { get; set; }

        [DefaultValue(true)]
        [NotifyParentProperty(true)]
        public bool ShowDetailedSubreportMessages { get; set; }

        [Category("Security")]
        [NotifyParentProperty(true)]
        [DefaultValue(false)]
        public bool EnableHyperlinks { get; set; }

        [NotifyParentProperty(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Browsable(false)]
        public ReportDataSourceCollection DataSources { get; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public IList<ReportParameter> OriginalParametersToDrillthrough { get; }

        #region IDisposable Members

        public void Dispose();

        #endregion

        #region ISerializable Members

        [SecurityTreatAsSafe]
        [SecurityCritical]
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context);

        #endregion

        public IList<string> GetDataSourceNames();
        public override int GetTotalPages(out PageCountMode pageCountMode);
        public override void LoadReportDefinition(TextReader report);
        public void LoadSubreportDefinition(string reportName, TextReader report);
        public void LoadSubreportDefinition(string reportName, Stream report);
        public override ReportPageSettings GetDefaultPageSettings();

        [Obsolete(
            "This method requires Code Access Security policy, which is deprecated.  For more information please go to http://go.microsoft.com/fwlink/?LinkId=160787."
            )]
        public void ExecuteReportInCurrentAppDomain(Evidence reportEvidence);

        [Obsolete(
            "This method requires Code Access Security policy, which is deprecated.  For more information please go to http://go.microsoft.com/fwlink/?LinkId=160787."
            )]
        public void AddTrustedCodeModuleInCurrentAppDomain(string assemblyName);

        [Obsolete(
            "This method requires Code Access Security policy, which is deprecated.  For more information please go to http://go.microsoft.com/fwlink/?LinkId=160787."
            )]
        public void ExecuteReportInSandboxAppDomain();

        public void AddFullTrustModuleInSandboxAppDomain(StrongName assemblyName);
        public void SetBasePermissionsForSandboxAppDomain(PermissionSet permissions);
        public void ReleaseSandboxAppDomain();
        public override void Refresh();
        public override ReportParameterInfoCollection GetParameters();
        public override void SetParameters(IEnumerable<ReportParameter> parameters);

        public override byte[] Render(string format, string deviceInfo, PageCountMode pageCountMode, out string mimeType,
                                      out string encoding, out string fileNameExtension, out string[] streams,
                                      out Warning[] warnings);

        public void Render(string format, string deviceInfo, CreateStreamCallback createStream, out Warning[] warnings);

        public void Render(string format, string deviceInfo, PageCountMode pageCountMode,
                           CreateStreamCallback createStream, out Warning[] warnings);

        public override RenderingExtension[] ListRenderingExtensions();

        public event SubreportProcessingEventHandler SubreportProcessing;
    }
}

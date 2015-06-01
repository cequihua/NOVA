// Type: System.Drawing.Printing.PrintDocument
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Assembly location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\System.Drawing.dll

using System.ComponentModel;
using System.Runtime;

namespace System.Drawing.Printing
{
    [DefaultProperty("DocumentName")]
    [DefaultEvent("PrintPage")]
    [ToolboxItemFilter("System.Drawing.Printing")]
    public class PrintDocument : Component
    {
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PageSettings DefaultPageSettings { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; set; }

        [DefaultValue("document")]
        public string DocumentName { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; set; }

        [DefaultValue(false)]
        public bool OriginAtMargins { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PrintController PrintController { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public PrinterSettings PrinterSettings { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; set; }

        protected virtual void OnBeginPrint(PrintEventArgs e);
        protected virtual void OnEndPrint(PrintEventArgs e);
        protected virtual void OnPrintPage(PrintPageEventArgs e);
        protected virtual void OnQueryPageSettings(QueryPageSettingsEventArgs e);
        public void Print();
        public override string ToString();

        public event PrintEventHandler BeginPrint;
        public event PrintEventHandler EndPrint;
        public event PrintPageEventHandler PrintPage;
        public event QueryPageSettingsEventHandler QueryPageSettings;
    }
}

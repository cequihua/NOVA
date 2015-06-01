// Type: System.Windows.Forms.TextBoxBase
// Assembly: System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// Assembly location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\System.Windows.Forms.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    [Designer(
        "System.Windows.Forms.Design.TextBoxBaseDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
        )]
    [DefaultEvent("TextChanged")]
    [DefaultBindingProperty("Text")]
    public abstract class TextBoxBase : Control
    {
        [DefaultValue(false)]
        public bool AcceptsTab { get; set; }

        [DefaultValue(true)]
        public virtual bool ShortcutsEnabled { get; set; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Localizable(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [DefaultValue(true)]
        public override bool AutoSize { get; set; }

        [DispId(-501)]
        public override Color BackColor { get; set; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override Image BackgroundImage { get; set; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override ImageLayout BackgroundImageLayout { get; set; }

        [DefaultValue(2)]
        [DispId(-504)]
        public BorderStyle BorderStyle { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; set; }

        protected override bool CanEnableIme { get; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CanUndo { get; }

        protected override CreateParams CreateParams { get; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected override bool DoubleBuffered { get; set; }

        protected override Cursor DefaultCursor { get; }
        protected override Size DefaultSize { get; }

        [DispId(-513)]
        public override Color ForeColor { get; set; }

        [DefaultValue(true)]
        public bool HideSelection { get; set; }

        protected override ImeMode ImeModeBase { get; set; }

        [Editor(
            "System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
            , typeof (UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [MergableProperty(false)]
        [Localizable(true)]
        public string[] Lines { get; set; }

        [DefaultValue(32767)]
        [Localizable(true)]
        public virtual int MaxLength { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Modified { get; set; }

        [DefaultValue(false)]
        [Localizable(true)]
        [RefreshProperties(RefreshProperties.All)]
        public virtual bool Multiline { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Padding Padding { get; set; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int PreferredHeight { get; }

        [DefaultValue(false)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public bool ReadOnly { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual string SelectedText { get; [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int SelectionLength { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public int SelectionStart { get; set; }

        [Localizable(true)]
        [Editor(
            "System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
            , typeof (UITypeEditor))]
        public override string Text { get; set; }

        [Browsable(false)]
        public virtual int TextLength { get; }

        [Localizable(true)]
        [DefaultValue(true)]
        public bool WordWrap { get; set; }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData);
        public void AppendText(string text);
        public void Clear();
        public void ClearUndo();
        public void Copy();
        protected override void CreateHandle();
        public void Cut();
        protected override bool IsInputKey(Keys keyData);
        protected override void OnHandleCreated(EventArgs e);
        protected override void OnHandleDestroyed(EventArgs e);
        public void Paste();
        protected override bool ProcessDialogKey(Keys keyData);
        protected virtual void OnAcceptsTabChanged(EventArgs e);
        protected virtual void OnBorderStyleChanged(EventArgs e);
        protected override void OnFontChanged(EventArgs e);
        protected virtual void OnHideSelectionChanged(EventArgs e);
        protected virtual void OnModifiedChanged(EventArgs e);
        protected override void OnMouseUp(MouseEventArgs mevent);
        protected virtual void OnMultilineChanged(EventArgs e);
        protected override void OnPaddingChanged(EventArgs e);
        protected virtual void OnReadOnlyChanged(EventArgs e);
        protected override void OnTextChanged(EventArgs e);
        public virtual char GetCharFromPosition(Point pt);
        public virtual int GetCharIndexFromPosition(Point pt);
        public virtual int GetLineFromCharIndex(int index);
        public virtual Point GetPositionFromCharIndex(int index);
        public int GetFirstCharIndexFromLine(int lineNumber);
        public int GetFirstCharIndexOfCurrentLine();
        public void ScrollToCaret();

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public void DeselectAll();

        public void Select(int start, int length);
        public void SelectAll();
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified);
        public override string ToString();
        public void Undo();
        protected override void WndProc(ref Message m);

        public event EventHandler AcceptsTabChanged;

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler AutoSizeChanged;

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public new event EventHandler BackgroundImageChanged;

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public new event EventHandler BackgroundImageLayoutChanged;

        public event EventHandler BorderStyleChanged;

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public new event EventHandler Click;

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public new event MouseEventHandler MouseClick;

        public event EventHandler HideSelectionChanged;
        public event EventHandler ModifiedChanged;
        public event EventHandler MultilineChanged;

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new event EventHandler PaddingChanged;

        public event EventHandler ReadOnlyChanged;

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public new event PaintEventHandler Paint;
    }
}

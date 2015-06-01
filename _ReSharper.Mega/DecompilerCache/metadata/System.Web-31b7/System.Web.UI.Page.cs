// Type: System.Web.UI.Page
// Assembly: System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Assembly location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\System.Web.dll

using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Runtime;
using System.Security.Principal;
using System.Web;
using System.Web.Caching;
using System.Web.Routing;
using System.Web.SessionState;
using System.Web.UI.Adapters;
using System.Web.UI.HtmlControls;

namespace System.Web.UI
{
    [DefaultEvent("Load")]
    [ToolboxItem(false)]
    [Designer(
        "Microsoft.VisualStudio.Web.WebForms.WebFormDesigner, Microsoft.VisualStudio.Web, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
        , typeof (IRootDesigner))]
    [DesignerSerializer(
        "Microsoft.VisualStudio.Web.WebForms.WebFormCodeDomSerializer, Microsoft.VisualStudio.Web, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
        ,
        "System.ComponentModel.Design.Serialization.TypeCodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
        )]
    [DesignerCategory("ASPXCodeBehind")]
    public class Page : TemplateControl, IHttpHandler
    {
        [EditorBrowsable(EditorBrowsableState.Never)] public const string postEventSourceID = "__EVENTTARGET";
        [EditorBrowsable(EditorBrowsableState.Never)] public const string postEventArgumentID = "__EVENTARGUMENT";
        public Page();

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public HttpApplicationState Application { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        protected internal override HttpContext Context { [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        get; }

        public ClientScriptManager ClientScript { get; }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        [DefaultValue("")]
        public string ClientTarget { get; set; }

        public string ClientQueryString { get; }

        [DefaultValue("")]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ErrorPage { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public bool IsCallback { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        protected internal virtual string UniqueFilePathSuffix { get; }

        public Control AutoPostBackControl { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public HtmlHead Header { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new virtual char IdSeparator { get; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public bool MaintainScrollPositionOnPostBack { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public MasterPage Master { get; }

        [DefaultValue("")]
        public virtual string MasterPageFile { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public int MaxPageStateFieldLength { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; set; }

        protected virtual PageStatePersister PageStatePersister { get; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public TraceContext Trace { get; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public HttpRequest Request { [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        get; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public HttpResponse Response { get; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public RouteData RouteData { get; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public HttpServerUtility Server { get; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Cache Cache { get; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual HttpSessionState Session { get; }

        [Localizable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Bindable(true)]
        public string Title { get; set; }

        [Localizable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Bindable(true)]
        public string MetaDescription { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Bindable(true)]
        [Localizable(true)]
        public string MetaKeywords { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual string Theme { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Filterable(false)]
        [Browsable(false)]
        public virtual string StyleSheetTheme { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public IPrincipal User { get; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public bool IsCrossPagePostBack { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public bool IsPostBack { get; }

        [Browsable(false)]
        [DefaultValue(true)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool EnableEventValidation { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; set; }

        [Browsable(false)]
        public override bool EnableViewState { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        [DefaultValue(0)]
        public ViewStateEncryptionMode ViewStateEncryptionMode { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; set; }

        [Browsable(false)]
        public string ViewStateUserKey { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public override string ID { get; set; }

        [Browsable(false)]
        public override bool Visible { [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        get; set; }

        public bool IsPostBackEventControlRegistered { get; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public bool IsValid { get; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public ValidatorCollection Validators { get; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Page PreviousPage { get; }

        [Obsolete(
            "The recommended alternative is HttpResponse.AddFileDependencies. http://go.microsoft.com/fwlink/?linkid=14202"
            )]
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected ArrayList FileDependencies { set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public bool Buffer { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public string ContentType { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CodePage { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ResponseEncoding { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public string Culture { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public int LCID { get; set; }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public string UICulture { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public TimeSpan AsyncTimeout { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected int TransactionMode { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected bool AspCompatMode { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected bool AsyncMode { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public bool TraceEnabled { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public TraceMode TraceModeValue { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool EnableViewStateMac { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; set; }

        [Filterable(false)]
        [Browsable(false)]
        [Obsolete(
            "The recommended alternative is Page.SetFocus and Page.MaintainScrollPositionOnPostBack. http://go.microsoft.com/fwlink/?linkid=14202"
            )]
        public bool SmartNavigation { get; set; }

        public bool IsAsync { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        public HtmlForm Form { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        public PageAdapter PageAdapter { get; }

        [Browsable(false)]
        public IDictionary Items { get; }

        #region IHttpHandler Members

        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual void ProcessRequest(HttpContext context);

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsReusable { get; }

        #endregion

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected internal virtual HtmlTextWriter CreateHtmlTextWriter(TextWriter tw);

        public static HtmlTextWriter CreateHtmlTextWriterFromType(TextWriter tw, Type writerType);
        public override Control FindControl(string id);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual int GetTypeHashCode();

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void DesignerInitialize();

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected internal virtual NameValueCollection DeterminePostBackMode();

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected internal virtual object LoadPageStateFromPersistenceMedium();

        public void SetFocus(Control control);
        public void SetFocus(string clientID);

        [Obsolete(
            "The recommended alternative is ClientScript.GetPostBackEventReference. http://go.microsoft.com/fwlink/?linkid=14202"
            )]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public string GetPostBackEventReference(Control control);

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [Obsolete(
            "The recommended alternative is ClientScript.GetPostBackEventReference. http://go.microsoft.com/fwlink/?linkid=14202"
            )]
        public string GetPostBackEventReference(Control control, string argument);

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [Obsolete(
            "The recommended alternative is ClientScript.GetPostBackEventReference. http://go.microsoft.com/fwlink/?linkid=14202"
            )]
        public string GetPostBackClientEvent(Control control, string argument);

        [Obsolete(
            "The recommended alternative is ClientScript.GetPostBackClientHyperlink. http://go.microsoft.com/fwlink/?linkid=14202"
            )]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public string GetPostBackClientHyperlink(Control control, string argument);

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected internal void AddContentTemplate(string templateName, ITemplate template);

        [Obsolete(
            "The recommended alternative is ClientScript.IsClientScriptBlockRegistered(string key). http://go.microsoft.com/fwlink/?linkid=14202"
            )]
        public bool IsClientScriptBlockRegistered(string key);

        [Obsolete(
            "The recommended alternative is ClientScript.IsStartupScriptRegistered(string key). http://go.microsoft.com/fwlink/?linkid=14202"
            )]
        public bool IsStartupScriptRegistered(string key);

        [Obsolete(
            "The recommended alternative is ClientScript.RegisterArrayDeclaration(string arrayName, string arrayValue). http://go.microsoft.com/fwlink/?linkid=14202"
            )]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public void RegisterArrayDeclaration(string arrayName, string arrayValue);

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [Obsolete(
            "The recommended alternative is ClientScript.RegisterHiddenField(string hiddenFieldName, string hiddenFieldInitialValue). http://go.microsoft.com/fwlink/?linkid=14202"
            )]
        public virtual void RegisterHiddenField(string hiddenFieldName, string hiddenFieldInitialValue);

        [Obsolete(
            "The recommended alternative is ClientScript.RegisterClientScriptBlock(Type type, string key, string script). http://go.microsoft.com/fwlink/?linkid=14202"
            )]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public virtual void RegisterClientScriptBlock(string key, string script);

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [Obsolete(
            "The recommended alternative is ClientScript.RegisterStartupScript(Type type, string key, string script). http://go.microsoft.com/fwlink/?linkid=14202"
            )]
        public virtual void RegisterStartupScript(string key, string script);

        [Obsolete(
            "The recommended alternative is ClientScript.RegisterOnSubmitStatement(Type type, string key, string script). http://go.microsoft.com/fwlink/?linkid=14202"
            )]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public void RegisterOnSubmitStatement(string key, string script);

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public void RegisterRequiresControlState(Control control);

        public bool RequiresControlState(Control control);

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public void UnregisterRequiresControlState(Control control);

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public void RegisterRequiresPostBack(Control control);

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument);

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public virtual void RegisterRequiresRaiseEvent(IPostBackEventHandler control);

        public string MapPath(string virtualPath);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        protected virtual void InitOutputCache(int duration, string varyByHeader, string varyByCustom,
                                               OutputCacheLocation location, string varyByParam);

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected virtual void InitOutputCache(int duration, string varyByContentEncoding, string varyByHeader,
                                               string varyByCustom, OutputCacheLocation location, string varyByParam);

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected internal virtual void InitOutputCache(OutputCacheParameters cacheSettings);

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected object GetWrappedFileDependencies(string[] virtualFileDependencies);

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected internal void AddWrappedFileDependencies(object virtualFileDependencies);

        protected virtual void OnLoadComplete(EventArgs e);
        protected virtual void OnPreRenderComplete(EventArgs e);
        protected override void FrameworkInitialize();
        protected virtual void InitializeCulture();
        protected internal override void OnInit(EventArgs e);
        protected virtual void OnPreInit(EventArgs e);
        protected virtual void OnInitComplete(EventArgs e);
        protected virtual void OnPreLoad(EventArgs e);
        public void RegisterRequiresViewStateEncryption();
        protected virtual void OnSaveStateComplete(EventArgs e);
        protected internal override void Render(HtmlTextWriter writer);

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public void RegisterViewStateHandler();

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected internal virtual void SavePageStateToPersistenceMedium(object state);

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected IAsyncResult AspCompatBeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData);

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected void AspCompatEndProcessRequest(IAsyncResult result);

        public void ExecuteRegisteredAsyncTasks();
        public void RegisterAsyncTask(PageAsyncTask task);

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected IAsyncResult AsyncPageBeginProcessRequest(HttpContext context, AsyncCallback callback,
                                                            object extraData);

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected void AsyncPageEndProcessRequest(IAsyncResult result);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public void AddOnPreRenderCompleteAsync(BeginEventHandler beginHandler, EndEventHandler endHandler);

        public void AddOnPreRenderCompleteAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state);
        public virtual void Validate();
        public virtual void Validate(string validationGroup);
        public ValidatorCollection GetValidators(string validationGroup);

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public virtual void VerifyRenderingInServerForm(Control control);

        public object GetDataItem();

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public event EventHandler LoadComplete;

        public event EventHandler PreInit;

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public event EventHandler PreLoad;

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public event EventHandler PreRenderComplete;

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public event EventHandler InitComplete;

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public event EventHandler SaveStateComplete;
    }
}

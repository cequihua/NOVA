<%@ Page Title="Detalle del DIM" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Dim.aspx.cs" Inherits="Mega.Admin.Dim.Dim" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.datepicker-es.js" type="text/javascript"></script>
    <link href="../Styles/css/jquery-ui-custom.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <asp:Label ID="HeaderLabel" runat="server" Text="Label"></asp:Label>
    </h2>
    <p>
        <asp:HyperLink runat="server" ID="ItemList" NavigateUrl="~/Dim/DimList.aspx" Text="Regresar a la Lista de Clientes" />
    </p>
    <div id="edit-client">
        <div>
            <fieldset>
                <legend>Generales (Primer Nivel - Obligatorio)</legend>
                <div class="doble_column_content">
                    <p>
                        <label>
                            <asp:HiddenField ID="DateFormat" runat="server" />
                            ID/DIM:
                        </label>
                        <asp:TextBox ID="IdTextBox" runat="server" MaxLength="12"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="IdRequiredFieldValidator1" runat="server" ErrorMessage="El campo Id es requerido."
                            ControlToValidate="IdTextBox" Display="None" />
                        <asp:RegularExpressionValidator ID="IdRegularExpressionValidator" runat="server"
                            ControlToValidate="IdTextBox" ErrorMessage="El campo Id no es válido." ValidationExpression="^\d+$"
                            Display="None"></asp:RegularExpressionValidator>
                        <asp:CustomValidator ID="IdCustomValidator" runat="server" ErrorMessage="El valor del campo ID ya existe en el Sistema."
                            Display="None" ControlToValidate="IdTextBox" OnServerValidate="IdCustomValidator_OnServerValidate"></asp:CustomValidator>
                        <asp:CustomValidator ID="GeneralCustomValidator" runat="server" ErrorMessage="" Display="None"
                            OnServerValidate="GeneralCustomValidator_OnServerValidate"></asp:CustomValidator>
                    </p>
                    <asp:Panel ID="ValIdPanel" runat="server">
                        <p>
                            <label>
                                Validación ID/DIM:
                            </label>
                            <asp:TextBox ID="ValIdTextBox" runat="server" MaxLength="12"></asp:TextBox>
                            <asp:CompareValidator ID="IdCompareValidator" runat="server" ErrorMessage="El Campo ID y Validación ID deben ser iguales."
                                ControlToValidate="IdTextBox" ControlToCompare="ValIdTextBox" Display="None"></asp:CompareValidator>
                        </p>
                    </asp:Panel>
                    <p>
                        <label>
                            Id JDE:</label>
                        <asp:TextBox ID="IdJDETextBox" runat="server" Enabled="false"></asp:TextBox>
                    </p>
                    <p>
                        <label>
                            Tipo:</label>
                        <asp:DropDownList ID="TypeDropDownList" runat="server" DataSourceID="TypesSqlLinqDataSource"
                            DataTextField="Name" DataValueField="Id">
                        </asp:DropDownList>
                    </p>
                    <asp:UpdatePanel ID="SponsorUpdatePanel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label ID="SponsorLabel" runat="server" Text="Patrocinador:"></asp:Label>
                            <asp:TextBox ID="SponsorTextBox" runat="server" OnTextChanged="SponsorTextBox_TextChanged"
                                AutoPostBack="True">
                            </asp:TextBox>
                            <asp:Label ID="SponsorResultLabel" runat="server" Text="Nombre patrocinador"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdateProgress runat="server" ID="PageUpdateProgress" AssociatedUpdatePanelID="SponsorUpdatePanel">
                        <ProgressTemplate>
                            <asp:Image ID="ProgressImage" runat="server" Width="24px" Height="24px" ImageUrl="~/Styles/Images/spinner.gif" />
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <p>
                        <label>
                            Nombre(s):
                        </label>
                        <asp:TextBox ID="NameTextBox" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="NameRequiredFieldValidator1" runat="server" ErrorMessage="El campo Nombre es requerido."
                            ControlToValidate="NameTextBox" Display="None" />
                    </p>
                    <p>
                        <label>
                            Apellido Paterno:
                        </label>
                        <asp:TextBox ID="LastNameTextBox" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="LastNameRequiredFieldValidator" runat="server" ErrorMessage="El campo Apellido Paterno es requerido."
                            ControlToValidate="LastNameTextBox" Display="None" />
                    </p>
                    <p>
                        <label>
                            Apellido Materno:
                        </label>
                        <asp:TextBox ID="MotherMiadenNameTextBox" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="MotherMiadenNameRequiredFieldValidator" runat="server"
                            ErrorMessage="El campo Apellido Materno es requerido." ControlToValidate="MotherMiadenNameTextBox"
                            Display="None" />
                    </p>
                    <p>
                        <label>
                            Sexo:</label>
                        <asp:DropDownList ID="SexDropDownList" runat="server" DataSourceID="SexLinqDataSource"
                            DataTextField="Name" DataValueField="Id">
                        </asp:DropDownList>
                    </p>
                    <p>
                        <label>
                            Estado Civil:</label>
                        <asp:DropDownList ID="MaritalStatusDropDownList" runat="server" DataSourceID="MaritalStatusLinqDataSource"
                            DataTextField="Name" DataValueField="Id">
                        </asp:DropDownList>
                    </p>
                    <p>
                        <label>
                            <asp:Label ID="BirthDateLabel" runat="server"></asp:Label>
                        </label>
                        <asp:TextBox ID="BirthDateTextBox" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="BirthDateRequiredFieldValidator" runat="server" ErrorMessage="El campo Fecha de Nacimiento es requerido."
                            ControlToValidate="BirthDateTextBox" Display="None" />
                    </p>
                </div>
                <div class="doble_column_content">
                    <fieldset id="sub">
                        <legend>Cónyuge</legend>
                        <div>
                            <p>
                                <label>
                                    Nombre(s):
                                </label>
                                <asp:TextBox ID="SpouseNameTextBox" runat="server"></asp:TextBox>
                            </p>
                            <p>
                                <label>
                                    Apellido Paterno:
                                </label>
                                <asp:TextBox ID="SpouseLastNameTextBox" runat="server"></asp:TextBox>
                            </p>
                            <p>
                                <label>
                                    Apellido Materno:
                                </label>
                                <asp:TextBox ID="SpouseMotherMaidenNameTextBox" runat="server"></asp:TextBox>
                            </p>
                        </div>
                    </fieldset>
                    <p>
                        <label>
                            <asp:Label ID="AddedDateLabel" runat="server"></asp:Label>
                        </label>
                        <asp:TextBox ID="AddedDateDateTimeTextBox" runat="server" Enabled="false"></asp:TextBox>
                    </p>
                    <asp:Panel ID="LowDateTimePanel" runat="server">
                        <p>
                            <label>
                                <asp:Label ID="LowDateLabel" runat="server"></asp:Label>
                            </label>
                            <asp:TextBox ID="LowDateTimeTextBox" runat="server" Enabled="false"></asp:TextBox>
                        </p>
                    </asp:Panel>
                    <asp:Panel ID="DisabledPanel" runat="server">
                        <p>
                            <label>
                                Desactivado:
                            </label>
                            <asp:CheckBox ID="DisabledCheckBox" runat="server" onclick="javascript:alert('Cuidado!, puede estar desabilitando este Cliente para uso futuro');" />
                        </p>
                    </asp:Panel>
                </div>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                    ShowSummary="false" />
            </fieldset>
        </div>
        <p>
        </p>
        <div>
            <fieldset>
                <legend>Información de Contacto (Segundo Nivel)</legend>
                <div class="doble_column_content">
                    <p>
                        <label>
                            Email:
                        </label>
                        <asp:TextBox ID="EmailTextBox" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="EmailRegularExpressionValidator" runat="server"
                            ControlToValidate="EmailTextBox" ErrorMessage="El campo Email no es válido."
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="None"></asp:RegularExpressionValidator>
                    </p>
                    <p>
                        <label>
                            Teléfono Celular:
                        </label>
                        <asp:TextBox ID="PhoneCellTextBox" runat="server"></asp:TextBox>
                    </p>
                    <p>
                        <label>
                            Teléfono Oficina:
                        </label>
                        <asp:TextBox ID="PhoneOfficeTextBox" runat="server"></asp:TextBox>
                    </p>
                    <p>
                        <label>
                            Teléfono Casa:
                        </label>
                        <asp:TextBox ID="PhoneHomeTextBox" runat="server"></asp:TextBox>
                    </p>
                    <p>
                        <label>
                            Calle
                        </label>
                        <asp:TextBox ID="Address1TextBox" runat="server"></asp:TextBox>
                    </p>
                    <p>
                        <label>
                            No. Exterior
                        </label>
                        <asp:TextBox ID="ExteriorNumberTextBox" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ExteriorNumberRequiredFieldValidator" runat="server"
                            ErrorMessage="El campo  No. Exterior es requerido." ControlToValidate="ExteriorNumberTextBox"
                            Display="None" />
                    </p>
                    <p>
                        <label>
                            No. Interior
                        </label>
                        <asp:TextBox ID="InteriorNumberTextBox" runat="server"></asp:TextBox>
                    </p>
                    <p>
                        <label>
                            Entre calles:
                        </label>
                        <asp:TextBox ID="Address2TextBox" runat="server"></asp:TextBox>
                    </p>
                    <p>
                        <label>
                            Colonia:
                        </label>
                        <asp:TextBox ID="Address3TextBox" runat="server"></asp:TextBox>
                    </p>
                    <p>
                        <label>
                            CP:
                        </label>
                        <asp:TextBox ID="CPTextBox" runat="server"></asp:TextBox>
                    </p>
                    <p>
                        <label>
                            Medio de Difusión:</label>
                        <asp:DropDownList ID="DiffusionMediaDropDownList" runat="server" DataTextField="Name" DataValueField="Id"
                            >
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="DiffusionMediaRequiredFieldValidator" runat="server"
                            ErrorMessage="El campo  Medio de Difusión es requerido." ControlToValidate="DiffusionMediaDropDownList"
                            Display="None" />
                    </p>
                   
                </div>
                <div class="doble_column_content">
                 <p>
                        <label>
                            País:</label>
                        <asp:DropDownList ID="CountryDropDownList" runat="server" DataTextField="Name" DataValueField="Id"
                            AutoPostBack="true" OnSelectedIndexChanged="CountryDropDownList_SelectedIndexChanged">
                        </asp:DropDownList>
                    </p>
                    <p>
                        <label>
                            Estado/Provincia:</label>
                        <asp:DropDownList ID="StateDropDownList" runat="server" DataTextField="Name" DataValueField="Id"
                            AutoPostBack="true" OnSelectedIndexChanged="StateDropDownList_SelectedIndexChanged">
                        </asp:DropDownList>
                    </p>
                    <p>
                        <label>
                            Población/Municipio:</label>
                        <asp:DropDownList ID="PopulationDropDownList" runat="server" DataTextField="Name"
                            DataValueField="Id">
                        </asp:DropDownList>
                    </p>
                    <p>
                        <label>
                            Observaciones:</label>
                        <asp:TextBox ID="NotesTextBox" runat="server" TextMode="MultiLine" />
                    </p>
                    <p>
                        <label>
                            Referencias:</label>
                        <asp:TextBox ID="ReferenceTextBox" runat="server" TextMode="MultiLine" />
                    </p>
                </div>
            </fieldset>
        </div>
        <div class="separator">
        </div>
        <p>
        </p>
        <div>
            <fieldset>
                <legend>Datos Complementarios (Tercer Nivel)</legend>
                <div class="doble_column_content">
                    <p>
                        <label>
                            Cotitular:</label>
                        <asp:TextBox ID="CoHolderTextBox" runat="server" />
                    </p>
                    <p>
                        <label>
                            Banco:</label>
                        <asp:DropDownList ID="BankDropDownList" runat="server" DataTextField="Name" DataValueField="Id">
                        </asp:DropDownList>
                    </p>
                    <p>
                        <label>
                            Sucursal:</label>
                        <asp:TextBox ID="SucursalTextBox" runat="server" />
                    </p>
                    <p>
                        <label>
                            Cuenta Bancaria:</label>
                        <asp:TextBox ID="BankAccountTextBox" runat="server" />
                    </p>
                    <p>
                        <label>
                            Validación Cuenta Bancaria:</label>
                        <asp:TextBox ID="BankAccountValidationTextBox" runat="server" />
                        <asp:CompareValidator ID="BankCompareValidator" runat="server" ErrorMessage="El Campo Cuenta Bancaria y Validación Cuenta Bancaria deben ser iguales."
                            ControlToValidate="BankAccountTextBox" ControlToCompare="BankAccountValidationTextBox"
                            Display="None"></asp:CompareValidator>
                    </p>
                    <p>
                        <label>
                            Cuenta Clave:</label>
                        <asp:TextBox ID="KeyAccountTextBox" runat="server" />
                    </p>
                    <p>
                        <label>
                            Validación Cuenta Clave:</label>
                        <asp:TextBox ID="KeyAccountValidationTextBox" runat="server" />
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="El Campo Cuenta Clave y Validación Cuenta Clave deben ser iguales."
                            ControlToValidate="KeyAccountTextBox" ControlToCompare="KeyAccountValidationTextBox"
                            Display="None"></asp:CompareValidator>
                    </p>
                    <p>
                        <label>
                            Tiene Comprob. Domicilio?
                        </label>
                        <asp:CheckBox ID="HasAddressProofCheckBox" runat="server" />
                    </p>
                    <p>
                        <label>
                            Tiene Identificacion Oficial?
                        </label>
                        <asp:CheckBox ID="IsOfficialIdCheckBox" runat="server" />
                    </p>
                    <p>
                        <label>
                            Máximo Crédito:
                        </label>
                        <asp:TextBox ID="MaxCreditTextBox" runat="server" />
                    </p>
                    <p>
                        <label>
                            Crédito Actual:
                        </label>
                        <asp:TextBox ID="CreditAmountTextBox" runat="server" Enabled="False" />
                    </p>
                    <p>
                        <label>
                            Fecha Crédito:
                        </label>
                        <asp:TextBox ID="CreditAmountDateTextBox" runat="server" Enabled="False" />
                    </p>
                </div>
                <div class="doble_column_content">
                    <p>
                        <label>
                            CURP:
                        </label>
                        <asp:TextBox ID="CURPTextBox" runat="server" />
                    </p>
                    <p>
                        <label>
                            Registro Fiscal:
                        </label>
                        <asp:TextBox ID="TaxRegisterTextBox" runat="server" />
                    </p>
                    <p>
                        <label>
                            Calificación:
                        </label>
                        <asp:TextBox ID="CalificationTextBox" runat="server" Enabled="False" Text="A" />
                    </p>
                    <p>
                        <label>
                            Retención Venta:
                        </label>
                        <asp:CheckBox ID="SaleRetentionCheckBox" runat="server" />
                    </p>
                    <p>
                        <label>
                            Retención Impuestos:
                        </label>
                        <asp:CheckBox ID="TaxRetentionCheckBox" runat="server" Enabled="False" />
                    </p>
                    <p>
                        <label>
                            Consignación:
                        </label>
                        <asp:CheckBox ID="ConsignableCheckBox" runat="server" />
                    </p>
                    <p>
                        <label>
                            Máximo Valor Consignación:
                        </label>
                        <asp:TextBox ID="MaxConsignableValueTextBox" runat="server" />
                    </p>
                    <p>
                        <label>
                            Valor Consignación actual:
                        </label>
                        <asp:TextBox ID="ConsignableAmountTextBox" runat="server" Enabled="False" />
                    </p>
                    <p>
                        <label>
                            Fecha Valor Consignación:
                        </label>
                        <asp:TextBox ID="ConsignableAmountDateTextBox" runat="server" Enabled="False" />
                    </p>
                    <p>
                        <label>
                            % IVA Aplicado:</label>
                        <asp:DropDownList ID="IVAGroupDropDownList" runat="server" DataTextField="Name" DataValueField="Id">
                        </asp:DropDownList>
                    </p>
                    <p>
                        <label>
                            % Descuento Aplicado:
                        </label>
                        <asp:TextBox ID="DiscountTextBox" runat="server" Enabled="False" />
                    </p>
                    <p>
                        <label>
                            Necesita Factura:
                        </label>
                        <asp:CheckBox ID="NeedInvoiceCheckBox" runat="server" />
                    </p>
                    <p>
                        <label>
                            Puntos Actuales:
                        </label>
                        <asp:TextBox ID="PointTextBox" runat="server" Enabled="False" />
                    </p>
                    <p>
                        <label>
                            Fecha Puntos Actuales:
                        </label>
                        <asp:TextBox ID="PointDateTextBox" runat="server" Enabled="False" />
                    </p>
                </div>
            </fieldset>
        </div>
        <p id="commands">
            <label>
                &nbsp;
            </label>
            <asp:Button ID="SaveButton" runat="server" Text="Guardar" OnClick="SaveButton_Click" />
            <asp:Button ID="CancelButton" runat="server" Text="Cancelar" CausesValidation="false"
                OnClick="CancelButton_Click" />
        </p>
    </div>
    <asp:LinqDataSource ID="CurrenciesSqlLinqDataSource" runat="server" ContextTypeName="Mega.Common.AdminDataContext"
        EntityTypeName="" Select="new (Id, Code + '-' + Name as Name )" TableName="UDCItems"
        Where="IdUDC == @IdUDC" OnContextCreating="LinqDataSource1_OnContextCreating">
        <WhereParameters>
            <asp:Parameter DefaultValue="CURR" Name="IdUDC" Type="String" />
        </WhereParameters>
    </asp:LinqDataSource>
    <asp:LinqDataSource ID="BanksSqlLinqDataSource" runat="server" ContextTypeName="Mega.Common.AdminDataContext"
        EntityTypeName="" Select="new (Id, Name )" TableName="UDCItems" Where="IdUDC == @IdUDC"
        OnContextCreating="LinqDataSource1_OnContextCreating">
        <WhereParameters>
            <asp:Parameter DefaultValue="BANK" Name="IdUDC" Type="String" />
        </WhereParameters>
    </asp:LinqDataSource>
    <asp:LinqDataSource ID="TypesSqlLinqDataSource" runat="server" ContextTypeName="Mega.Common.AdminDataContext"
        EntityTypeName="" Select="new (Id, Name, Code)" TableName="UDCItems" Where="IdUDC == @IdUDC"
        OnContextCreating="LinqDataSource1_OnContextCreating">
        <WhereParameters>
            <asp:Parameter DefaultValue="DIMTYP" Name="IdUDC" Type="String" />
        </WhereParameters>
    </asp:LinqDataSource>
    <asp:LinqDataSource ID="MaritalStatusLinqDataSource" runat="server" ContextTypeName="Mega.Common.AdminDataContext"
        EntityTypeName="" Select="new (Id, Name, Code)" TableName="UDCItems" Where="IdUDC == @IdUDC"
        OnContextCreating="LinqDataSource1_OnContextCreating">
        <WhereParameters>
            <asp:Parameter DefaultValue="MARSTA" Name="IdUDC" Type="String" />
        </WhereParameters>
    </asp:LinqDataSource>
    <asp:LinqDataSource ID="SexLinqDataSource" runat="server" ContextTypeName="Mega.Common.AdminDataContext"
        EntityTypeName="" Select="new (Id, Name, Code)" TableName="UDCItems" Where="IdUDC == @IdUDC"
        OnContextCreating="LinqDataSource1_OnContextCreating">
        <WhereParameters>
            <asp:Parameter DefaultValue="SEX" Name="IdUDC" Type="String" />
        </WhereParameters>
    </asp:LinqDataSource>
    <asp:LinqDataSource ID="DimsLinqDataSource" runat="server" ContextTypeName="Mega.Common.AdminDataContext"
        EnableInsert="True" EnableUpdate="True" TableName="Dims" OnContextCreating="LinqDataSource1_OnContextCreating">
    </asp:LinqDataSource>
    <script language="javascript" type="text/javascript">
        $(function () {
            $("#MainContent_BirthDateTextBox").datepicker({ changeMonth: true, changeYear: true, dateFormat: $('#MainContent_DateFormat').val() });
        });
    </script>
</asp:Content>

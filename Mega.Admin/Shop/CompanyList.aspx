<%@ Page Title="Lista de Compañías" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="CompanyList.aspx.cs" Inherits="Mega.Admin.Shop.CompanyList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Administración de Compañías
    </h2>
    <p>
    </p>
    <div class="table_container">
        <asp:GridView ID="CompaniesGridView" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            DataSourceID="CompaniesLinqDataSource" DataKeyNames="Id" OnSelectedIndexChanged="CompaniesGridView_SelectedIndexChanged">
            <Columns>
                <asp:CommandField CancelText="Cancelar" DeleteText="Eliminar" EditText="Editar" InsertText="Insertar"
                    NewText="Nuevo" SelectText="Editar" ShowSelectButton="True" 
                    UpdateText="Guardar" ShowDeleteButton="True">
                    <ItemStyle Width="50px"></ItemStyle>
                </asp:CommandField>
                <asp:BoundField DataField="Name" HeaderText="Nombre" SortExpression="Name">
                    <ControlStyle Width="300px"></ControlStyle>
                    <ItemStyle Width="300px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Phone1" HeaderText="Teléfono 1" SortExpression="Phone1">
                    <ControlStyle Width="90px"></ControlStyle>
                    <ItemStyle Width="90px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Phone2" HeaderText="Teléfono 2" SortExpression="Phone2">
                    <ControlStyle Width="90px"></ControlStyle>
                    <ItemStyle Width="90px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Fax" HeaderText="Fax" SortExpression="Fax">
                    <ControlStyle Width="60px"></ControlStyle>
                    <ItemStyle Width="60px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email">
                    <ControlStyle Width="60px"></ControlStyle>
                    <ItemStyle Width="60px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="CurrencyCode" HeaderText="Moneda" SortExpression="">
                    <ControlStyle Width="50px"></ControlStyle>
                    <ItemStyle Width="50px"></ItemStyle>
                </asp:BoundField>
                <asp:CheckBoxField DataField="Disabled" HeaderText="Desabilitado" ReadOnly="True"
                    SortExpression="Disabled" />
            </Columns>
        </asp:GridView>
    </div>
    <p>
    </p>
    <fieldset>
        <legend>
            <asp:Label ID="HeaderLabel" runat="server" Text="Label"></asp:Label>
        </legend>
        <div class="doble_column_content">
            <p>
                <label>
                    Id:
                </label>
                <asp:TextBox ID="IdTextBox" runat="server" MaxLength="12"></asp:TextBox>
                <asp:RequiredFieldValidator ID="IdRequiredFieldValidator1" runat="server" ErrorMessage="El campo Id es requerido."
                    ControlToValidate="IdTextBox" Display="None" ValidationGroup="Add" />
                <asp:RegularExpressionValidator ID="IdRegularExpressionValidator" runat="server"
                    ControlToValidate="IdTextBox" ErrorMessage="El campo Id no es válido." ValidationExpression="^\d+$"
                    Display="None" ValidationGroup="Add"></asp:RegularExpressionValidator>
                <asp:CustomValidator ID="IdCustomValidator" runat="server" ErrorMessage="El valor del campo Id ya existe en el Sistema."
                    Display="None" ControlToValidate="IdTextBox" ValidationGroup="Add" OnServerValidate="IdCustomValidator_OnServerValidate"></asp:CustomValidator>
            </p>
            <p>
                <label>
                    Nombre:
                </label>
                <asp:TextBox ID="NameTextBox" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="NameRequiredFieldValidator1" runat="server" ErrorMessage="El campo Nombre es requerido."
                    ControlToValidate="NameTextBox" Display="None" ValidationGroup="Add" />
            </p>
            <p>
                <label>
                    Certificado Legal (RFC):
                </label>
                <asp:TextBox ID="LegalCertificateTextBox" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="LegalCertificateRequiredFieldValidator" runat="server"
                    ErrorMessage="El campo Certificado Legal es requerido." ControlToValidate="LegalCertificateTextBox"
                    Display="None" ValidationGroup="Add" />
            </p>
            <p>
                <label>
                    Dirección Completa:
                </label>
                <asp:TextBox ID="FullAddressTextBox" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="FullAddressRequiredFieldValidator" runat="server"
                    ErrorMessage="El campo Dirección Completa es requerido." ControlToValidate="FullAddressTextBox"
                    Display="None" ValidationGroup="Add" />
            </p>
            <p>
                <label>
                    Ciudad:
                </label>
                <asp:TextBox ID="CityTextBox" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="CityRequiredFieldValidator" runat="server" ErrorMessage="El campo Ciudad es requerido."
                    ControlToValidate="CityTextBox" Display="None" ValidationGroup="Add" />
            </p>
            <p>
                <label>
                    Requiere Tarjeta de Confirmación?
                </label>
                <asp:CheckBox ID="CardConfirmationRequeriedCheckBox" runat="server"></asp:CheckBox>
            </p>
            <p>
                <label>
                    Utiliza Pedimento?
                </label>
                <asp:CheckBox ID="UsePedimentCheckBox" runat="server"></asp:CheckBox>
            </p>
            <p>
                <label>
                    Máximo ajuste en Tickets por Tasa de Cambio:
                </label>
                <asp:TextBox ID="MaxDiffAmountInChangeRateTextBox" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="MaxDiffAmountInChangeRateRequiredFieldValidator"
                    runat="server" ErrorMessage="El campo Máximo valor de direrencia por Tasas de Cambios es requerido."
                    ControlToValidate="MaxDiffAmountInChangeRateTextBox" Display="None" ValidationGroup="Add" />
            </p>
            <p>
                <label>
                    % Descuento por defecto para Dims:
                </label>
                <asp:TextBox ID="DimDefaultDiscountTextBox" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="DimDefaultDiscountRequiredFieldValidator" runat="server"
                    ErrorMessage="El campo Descuento por defecto para los nuevos Dim es requerido."
                    ControlToValidate="DimDefaultDiscountTextBox" Display="None" ValidationGroup="Add" />
            </p>
        </div>
        <div class="doble_column_content">
            <p>
                <label>
                    Teléfono 1:
                </label>
                <asp:TextBox ID="Phone1TextBox" runat="server"></asp:TextBox>
            </p>
            <p>
                <label>
                    Teléfono 2:
                </label>
                <asp:TextBox ID="Phone2TextBox" runat="server"></asp:TextBox>
            </p>
            <p>
                <label>
                    Fax:
                </label>
                <asp:TextBox ID="FaxTextBox" runat="server"></asp:TextBox>
            </p>
            <p>
                <label>
                    Email:
                </label>
                <asp:TextBox ID="EmailTextBox" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator ID="EmailRegularExpressionValidator" runat="server"
                    ControlToValidate="EmailTextBox" ErrorMessage="El campo Email no es válido."
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Add"
                    Display="None"></asp:RegularExpressionValidator>
            </p>
            <p>
                <label>
                    Email Finanzas:
                </label>
                <asp:TextBox ID="Email2TextBox" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator ID="Email2RegularExpressionValidator" runat="server"
                    ControlToValidate="Email2TextBox" ErrorMessage="El campo Email Finanzas no es válido."
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Add"
                    Display="None"></asp:RegularExpressionValidator>
            </p>
            <p>
                <label>
                    Email Logística:
                </label>
                <asp:TextBox ID="Email3TextBox" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="Email3TextBox"
                    ErrorMessage="El campo Email Logística no es válido." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    ValidationGroup="Add" Display="None"></asp:RegularExpressionValidator>
            </p>
            <p>
                <label>
                    Moneda:</label>
                <asp:DropDownList ID="CurrencyDropDownList" runat="server" DataSourceID="CurrenciesSqlLinqDataSource"
                    DataTextField="Name" DataValueField="Id">
                </asp:DropDownList>
            </p>
            <p>
                <label>
                    Desabilitado:
                </label>
                <asp:CheckBox ID="DisabledCheckBox" runat="server" onclick="javascript:alert('Cuidado!, puede estar desabilitando esta Compañía para uso futuro');" />
            </p>
            <p>
                <label>
                    Compras Mensuales máximas por Nómina de Empleado:
                </label>
                <asp:TextBox ID="MaxAmountPayrollTextBox" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="MaxAmountPayrollRequiredFieldValidator" runat="server"
                    ErrorMessage="El campo Máximo Valor Mensual adquirido por Nómina de Empleado es requerido."
                    ControlToValidate="MaxAmountPayrollTextBox" Display="None" ValidationGroup="Add" />
            </p>
            <p>
                <label>
                    Máximo Efectivo Total permitido en Caja:
                </label>
                <asp:TextBox ID="MaxCashInCashierTextBox" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="MaxCashInCashierRequiredFieldValidator" runat="server"
                    ErrorMessage="El campo Máximo Importe en Caja para los retiros automáticos es requerido."
                    ControlToValidate="MaxCashInCashierTextBox" Display="None" ValidationGroup="Add" />
            </p>
            <p>
                <label>
                    &nbsp;
                </label>
                <asp:Button ID="SaveButton" runat="server" Text="Guardar" ValidationGroup="Add" OnClick="SaveButton_Click" />
                <asp:Button ID="CancelButton" runat="server" Text="Cancelar" CausesValidation="false"
                    OnClick="CancelButton_Click" />
            </p>
        </div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
            ShowSummary="False" ValidationGroup="Add" HeaderText="Revision" />
        <asp:CustomValidator ID="GeneralCustomValidator" runat="server" ErrorMessage="" Display="None"
            ValidationGroup="Add" OnServerValidate="GeneralCustomValidator_OnServerValidate"></asp:CustomValidator>
    </fieldset>
    <asp:LinqDataSource ID="CurrenciesSqlLinqDataSource" runat="server" ContextTypeName="Mega.Common.AdminDataContext"
        EntityTypeName="" Select="new (Id, Code + '-' + Name as Name )" TableName="UDCItems"
        Where="IdUDC == @IdUDC" OnContextCreating="LinqDataSource1_OnContextCreating">
        <WhereParameters>
            <asp:Parameter DefaultValue="CURR" Name="IdUDC" Type="String" />
        </WhereParameters>
    </asp:LinqDataSource>
    <asp:LinqDataSource ID="CompaniesLinqDataSource" runat="server" ContextTypeName="Mega.Common.AdminDataContext"
        EnableInsert="True" EnableUpdate="True" TableName="Companies" OnContextCreating="LinqDataSource1_OnContextCreating">
    </asp:LinqDataSource>
</asp:Content>

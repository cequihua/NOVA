<%@ Page Title="Agregar Tienda (CNV)" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="ShopAdd.aspx.cs" Inherits="Mega.Admin.Shop.ShopAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Agregar Tienda (CNV) al Sistema
    </h2>
    <p>
        <asp:HyperLink runat="server" ID="ItemList" NavigateUrl="~/Shop/ShopList.aspx" Text="Regresar a la Lista de Tiendas" />
    </p>
    <fieldset>
        <legend>Datos de la Tienda</legend>
        <div class="doble_column_content">
            <p>
                <label>
                    Id:
                </label>
                <asp:TextBox ID="IdTextBox" runat="server" MaxLength="12"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="IdTextBox"
                    ErrorMessage="El campo Id es Requerido" Display="None" />
                <asp:CustomValidator ID="IdCustomValidator" runat="server" ErrorMessage="El valor del campo Id ya existe en el Sistema."
                    Display="None" ControlToValidate="IdTextBox" OnServerValidate="IdCustomValidator_OnServerValidate"></asp:CustomValidator>
            </p>
            <p>
                <label>
                    Nombre:
                </label>
                <asp:TextBox ID="NameTextBox" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="NameTextBox"
                    ErrorMessage="El campo Nombre es Requerido." Display="None" />
            </p>
            <p>
                <label>
                    Compañía:
                </label>
                <asp:DropDownList ID="CompanyDropDownList" runat="server" DataTextField="Name" DataValueField="Id"
                    DataSourceID="CompaniesSqlLinqDataSource" AutoPostBack="True" OnSelectedIndexChanged="CompanyDropDownList_SelectedIndexChanged">
                </asp:DropDownList>
            </p>
            <p>
                <label>
                    Moneda de Operación:
                </label>
                <asp:DropDownList ID="CurrencyDropDownList" runat="server" DataTextField="Name" DataValueField="Id"
                    DataSourceID="CurrencyLinqDataSource" Enabled="False">
                </asp:DropDownList>
            </p>
            <p>
                <label>
                    Dirección Postal:
                </label>
                <asp:TextBox ID="AddressTextBox" runat="server" TextMode="MultiLine" />
            </p>
            <p>
                <label>
                    Ciudad (Para el Ticket):
                </label>
                <asp:TextBox ID="TicketCityTextBox" runat="server"  />
            </p>
            <p>
                <label>
                    Grupo IVA:</label>
                <asp:DropDownList ID="IVAGroupDropDownList" runat="server" DataTextField="Name"
                    DataValueField="Id" DataSourceID="IVAGroupLinqDataSource">
                </asp:DropDownList>
            </p>
            <p>
                <label>
                    Tipo IVA p/Manejo:</label>
                <asp:DropDownList ID="IVATypeByManagementDropDownList" runat="server" DataTextField="Name"
                    DataValueField="Id" DataSourceID="IVATypeLinqDataSource">
                </asp:DropDownList>
            </p>
        </div>
        <div class="doble_column_content">
            <p>
                <label>
                    País:
                </label>
                <asp:DropDownList ID="CountryDropDownList" runat="server" DataTextField="Name" DataValueField="Id"
                    DataSourceID="CountriesSqlLinqDataSource">
                </asp:DropDownList>
            </p>
            <p>
                <label>
                    Teléfono 1:
                </label>
                <asp:TextBox ID="Phone1TextBox" runat="server" />
            </p>
            <p>
                <label>
                    Teléfono 2:
                </label>
                <asp:TextBox ID="Phone2TextBox" runat="server" />
            </p>
            <p>
                <label>
                    Teléfono 3:
                </label>
                <asp:TextBox ID="Phone3TextBox" runat="server" />
            </p>
            <p>
                <label>
                    Código Clave:
                </label>
                <asp:TextBox ID="KeyCodeNameTextBox" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="KeyCodeNameTextBox"
                    ErrorMessage="El campo Código Clave es Requerido." Display="None" />
            </p>
            <p>
                <label>
                    Tamaño de Código Clave:
                </label>
                <asp:TextBox ID="KeyCodeSizeTextBox" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="KeyCodeSizeTextBox"
                    ErrorMessage="El campo Tamaño de Código Clave es Requerido." Display="None" />
                <asp:RegularExpressionValidator ID="KeyCodeSizeRegularExpressionValidator" runat="server"
                    ControlToValidate="KeyCodeSizeTextBox" ErrorMessage="El campo Tamaño de Código Clave no es válido."
                    ValidationExpression="^\d+$" Display="None"></asp:RegularExpressionValidator>
            </p>
            <p>
                <label>
                    Email:
                </label>
                <asp:TextBox ID="EmailTextBox" runat="server" Text="" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="EmailTextBox"
                    ErrorMessage="El campo Email es Requerido." Display="None" />
                <asp:RegularExpressionValidator ID="EmailRegularExpressionValidator" runat="server"
                    ControlToValidate="EmailTextBox" ErrorMessage="El campo Email no es válido."
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="None"></asp:RegularExpressionValidator>
            </p>
             <p>
                <label>
                    Email Coordinador(es):
                </label>
                <asp:TextBox ID="Email2TextBox" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator ID="Email2RegularExpressionValidator" runat="server"
                    ControlToValidate="Email2TextBox" ErrorMessage="El campo Email Coordinador no es válido."
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    Display="None"></asp:RegularExpressionValidator>
            </p>
            <p>
                <label>
                    &nbsp;
                </label>
                <asp:Button ID="AddButton" runat="server" Text="Agregar" OnClick="AddButton_Click"
                    CausesValidation="true" />
            </p>
        </div>
    </fieldset>
    <asp:LinqDataSource ID="CompaniesSqlLinqDataSource" runat="server" ContextTypeName="Mega.Common.AdminDataContext"
        EntityTypeName="" OnContextCreating="LinqDataSource1_OnContextCreating" Select="new (Id, Name )"
        TableName="Companies">
    </asp:LinqDataSource>
    <asp:LinqDataSource ID="CountriesSqlLinqDataSource" runat="server" OnContextCreating="LinqDataSource1_OnContextCreating"
        ContextTypeName="Mega.Common.AdminDataContext" EntityTypeName="" Select="new (Id, Name )"
        TableName="UDCItems" Where="IdUDC == @IdUDC">
        <WhereParameters>
            <asp:Parameter DefaultValue="CN" Name="IdUDC" Type="String" />
        </WhereParameters>
    </asp:LinqDataSource>
    <asp:LinqDataSource ID="CurrencyLinqDataSource" runat="server" OnContextCreating="LinqDataSource1_OnContextCreating"
        ContextTypeName="Mega.Common.AdminDataContext" EntityTypeName="" Select="new (Id, Code + '-' + Name as Name )"
        TableName="UDCItems" Where="IdUDC == @IdUDC">
        <WhereParameters>
            <asp:Parameter DefaultValue="CURR" Name="IdUDC" Type="String" />
        </WhereParameters>
    </asp:LinqDataSource>
    <asp:LinqDataSource ID="IVATypeLinqDataSource" runat="server" OnContextCreating="LinqDataSource1_OnContextCreating"
        ContextTypeName="Mega.Common.AdminDataContext" EntityTypeName="" Select="new (Id, Name )"
        TableName="UDCItems" Where="IdUDC == @IdUDC">
        <WhereParameters>
            <asp:Parameter DefaultValue="IVATYP" Name="IdUDC" Type="String" />
        </WhereParameters>
    </asp:LinqDataSource>
    <asp:LinqDataSource ID="IVAGroupLinqDataSource" runat="server" OnContextCreating="LinqDataSource1_OnContextCreating"
        ContextTypeName="Mega.Common.AdminDataContext" EntityTypeName="" Select="new (Id, Name )"
        TableName="UDCItems" Where="IdUDC == @IdUDC">
        <WhereParameters>
            <asp:Parameter DefaultValue="IVAGRP" Name="IdUDC" Type="String" />
        </WhereParameters>
    </asp:LinqDataSource>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" class="error_msg" ShowMessageBox="true"
        ShowSummary="false" />
</asp:Content>

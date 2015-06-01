<%@ Page Title="Modificar Tienda (CNV) en el Sistema" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="ShopEdit.aspx.cs" Inherits="Mega.Admin.Shop.ShopEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Modificar Tienda (CNV) en el Sistema
    </h2>
    <p>
        <asp:HyperLink runat="server" ID="ItemList" NavigateUrl="~/Shop/ShopList.aspx" Text="Regresar a la Lista de Tiendas" />
    </p>
    <div>
        <fieldset>
            <legend>Datos de la Tienda</legend>
            <div class="doble_column_content">
                <p>
                    <label>
                        Id:
                    </label>
                    <asp:TextBox ID="IdTextBox" runat="server" MaxLength="12" Enabled="false"></asp:TextBox>
                </p>
                <p>
                    <label>
                        Nombre:
                    </label>
                    <asp:TextBox ID="NameTextBox" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="NameTextBox"
                        ErrorMessage="El campo Nombre es Requerido." Display="None" ValidationGroup="Shop" />
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
                    <asp:TextBox ID="TicketCityTextBox" runat="server" />
                </p>
                <p>
                    <label>
                        Grupo IVA:</label>
                    <asp:DropDownList ID="IVAGroupDropDownList" runat="server" DataTextField="Name" DataValueField="Id"
                        DataSourceID="IVAGroupLinqDataSource">
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
                        Nombre Código Clave:
                    </label>
                    <asp:TextBox ID="KeyCodeNameTextBox" runat="server" />
                    <asp:RequiredFieldValidator ID="KeyCodeNameRequiredFieldValidator1" runat="server"
                        ControlToValidate="KeyCodeNameTextBox" ErrorMessage="El campo Código Clave es Requerido."
                        Display="None" ValidationGroup="Shop" />
                </p>
                <p>
                    <label>
                        Tamaño de Código Clave:
                    </label>
                    <asp:TextBox ID="KeyCodeSizeTextBox" runat="server" />
                    <asp:RequiredFieldValidator ID="KeyCodeSizeRequiredFieldValidator5" runat="server"
                        ControlToValidate="KeyCodeSizeTextBox" ErrorMessage="El campo Tamaño de Código Clave es Requerido."
                        Display="None" />
                    <asp:RegularExpressionValidator ID="KeyCodeSizeRegularExpressionValidator" runat="server"
                        ControlToValidate="KeyCodeSizeTextBox" ErrorMessage="El campo Tamaño de Código Clave no es válido."
                        ValidationExpression="^\d+$" Display="None" ValidationGroup="Shop"></asp:RegularExpressionValidator>
                </p>
                <p>
                    <label>
                        Email:
                    </label>
                    <asp:TextBox ID="EmailTextBox" runat="server" Text="" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="EmailTextBox"
                        ErrorMessage="El campo Email es Requerido." Display="None" ValidationGroup="Shop" />
                    <asp:RegularExpressionValidator ID="EmailRegularExpressionValidator" runat="server"
                        ControlToValidate="EmailTextBox" ErrorMessage="El campo Email no es válido."
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="None"
                        ValidationGroup="Shop"></asp:RegularExpressionValidator>
                </p>
                <p>
                    <label>
                        Email Coordinador(es):
                    </label>
                    <asp:TextBox ID="Email2TextBox" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="Email2RegularExpressionValidator" runat="server"
                        ControlToValidate="Email2TextBox" ErrorMessage="El campo Email 2 no es válido."
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Shop"
                        Display="None"></asp:RegularExpressionValidator>
                </p>
                <p>
                    <label>
                        Desabilitado:
                    </label>
                    <asp:CheckBox ID="DisabledCheckBox" runat="server" onclick="javascript:alert('Cuidado!, puede estar desabilitando esta Tienda para uso futuro');" />
                </p>
                <p>
                    <label>
                        &nbsp;
                    </label>
                    <asp:Button ID="editButton" runat="server" Text="Guardar" OnClick="EditButton_Click"
                        CausesValidation="true" ValidationGroup="Shop" />
                </p>
            </div>
        </fieldset>
        <asp:LinqDataSource ID="CompaniesSqlLinqDataSource" runat="server" ContextTypeName="Mega.Common.AdminDataContext"
            EntityTypeName="" OnContextCreating="LinqDataSource1_OnContextCreating" Select="new (Id, Name )"
            TableName="Companies">
        </asp:LinqDataSource>
        <asp:LinqDataSource ID="CountriesSqlLinqDataSource" runat="server" ContextTypeName="Mega.Common.AdminDataContext"
            EntityTypeName="" Select="new (Id, Code + '-' + Name as Name )" TableName="UDCItems"
            Where="IdUDC == @IdUDC" OnContextCreating="LinqDataSource1_OnContextCreating">
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
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" class="error_msg" ValidationGroup="Shop"
            ShowMessageBox="true" ShowSummary="false" />
    </div>
    <div class="separator">
    </div>
    <fieldset>
        <legend>Ubicaciones de esta Tienda </legend>
        <div class="table_container">
            <asp:GridView ID="LocationsGridView" runat="server" AutoGenerateColumns="False" DataSourceID="LocationsLinqDataSource"
                DataKeyNames="Id" EmptyDataRowStyle-CssClass="grid-empty-data" EmptyDataText="No existen ubicaciones registradas">
                <Columns>
                    <asp:CommandField ShowEditButton="True" CancelText="Cancelar" EditText="Editar" UpdateText="Guardar">
                        <ItemStyle Width="100px"></ItemStyle>
                    </asp:CommandField>
                    <asp:TemplateField HeaderText="Nombre" SortExpression="Name">
                        <EditItemTemplate>
                            <asp:TextBox ID="GridViewNameTextBox" runat="server" Text='<%# Bind("Name") %>' />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="GridViewNameTextBox"
                                ErrorMessage="El campo Nombre es Requerido" Display="Dynamic" class="error_msg">*</asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Name" runat="server" Text='<%# Bind("Name") %>' />
                        </ItemTemplate>
                        <ControlStyle Width="90%"></ControlStyle>
                        <ItemStyle Width="90%"></ItemStyle>
                    </asp:TemplateField>
                    <asp:CheckBoxField DataField="IsSalePoint" HeaderText="Puede Vender?" SortExpression="IsSalePoint">
                        <ControlStyle Width="100px"></ControlStyle>
                        <ItemStyle Width="100px"></ItemStyle>
                    </asp:CheckBoxField>
                    <asp:CheckBoxField DataField="Disabled" HeaderText="Desabilitada" SortExpression="Disabled">
                        <ControlStyle Width="100px"></ControlStyle>
                        <ItemStyle Width="100px"></ItemStyle>
                    </asp:CheckBoxField>
                </Columns>
            </asp:GridView>
            <asp:LinqDataSource ID="LocationsLinqDataSource" runat="server" ContextTypeName="Mega.Common.AdminDataContext"
                EnableUpdate="True" EntityTypeName="" TableName="Locations" OnContextCreating="LinqDataSource1_OnContextCreating"
                Where="IdShop == @IdShop">
                <WhereParameters>
                    <asp:QueryStringParameter Name="IdShop" QueryStringField="id" Type="String" />
                </WhereParameters>
            </asp:LinqDataSource>
        </div>
        <div class="doble_column_content">
            <p>
            </p>
        </div>
        <div class="doble_column_content">
            <p>
                <b>
                    <label>
                        Nueva Ubicación:
                    </label>
                </b>
                <asp:TextBox ID="LocationNameTextBox" runat="server" />
            </p>
            <p>
                <label>
                    Puede Vender?:
                </label>
                <asp:CheckBox ID="LocationIsSalePointCheckBox" runat="server" />
            </p>
            <p>
                <label>
                    &nbsp;
                </label>
                <asp:Button ID="AddLocationButton" runat="server" Text="Agregar" OnClick="AddLocationButton_Click"
                    ValidationGroup="Location" />
            </p>
        </div>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="El campo Nueva Ubicación es requerido."
            ValidationGroup="Location" ControlToValidate="LocationNameTextBox" Display="None" />
        <asp:ValidationSummary ID="ValidationSummary3" runat="server" class="error_msg" ValidationGroup="Location"
            ShowMessageBox="true" ShowSummary="false" />
    </fieldset>
    <div class="separator">
    </div>
    <fieldset>
        <legend>Cajas que posee esta Tienda </legend>
        <div class="table_container">
            <asp:GridView ID="CashiersGridView" runat="server" AutoGenerateColumns="False" DataSourceID="CashiersLinqDataSource"
                DataKeyNames="Id" EmptyDataText="No existen cajas registradas">
                <Columns>
                    <asp:CommandField ShowEditButton="True" CancelText="Cancelar" EditText="Editar" UpdateText="Guardar">
                        <ItemStyle Width="120px"></ItemStyle>
                    </asp:CommandField>
                    <asp:TemplateField HeaderText="Nombre" SortExpression="Name">
                        <EditItemTemplate>
                            <asp:TextBox ID="GridViewNameTextBox" runat="server" Text='<%# Bind("Name") %>' />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="GridViewNameTextBox"
                                ErrorMessage="El campo Nombre es Requerido" Display="Dynamic" class="error_msg">*</asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Name" runat="server" Text='<%# Bind("Name") %>' />
                        </ItemTemplate>
                        <ControlStyle Width="200px"></ControlStyle>
                        <ItemStyle Width="200px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tickets a imprimir" SortExpression="TicketCountToPrint">
                        <EditItemTemplate>
                            <asp:TextBox ID="GridViewTicketCountToPrintTextBox" runat="server" Text='<%# Bind("TicketCountToPrint") %>' />
                            <asp:RequiredFieldValidator ID="GridViewTicketCountToPrintRequiredFieldValidator"
                                runat="server" ControlToValidate="GridViewTicketCountToPrintTextBox" ErrorMessage="El campo Tickets a imprimir es requerido."
                                Display="Dynamic" class="error_msg">*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="GridViewTicketCountToPrintRegularExpressionValidator"
                                runat="server" ControlToValidate="GridViewTicketCountToPrintTextBox" ErrorMessage="El campo Tickets a imprimir no es válido."
                                ValidationExpression="^\d+$" Display="Dynamic" class="error_msg">*</asp:RegularExpressionValidator>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Name" runat="server" Text='<%# Bind("TicketCountToPrint") %>' />
                        </ItemTemplate>
                        <ControlStyle Width="60px"></ControlStyle>
                        <ItemStyle Width="60px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Margenes" SortExpression="TicketPageMargin">
                        <EditItemTemplate>
                            <asp:TextBox ID="GridViewTicketPageMarginTextBox" runat="server" Text='<%# Bind("TicketPageMargin") %>' />
                            <asp:RequiredFieldValidator ID="GridViewTicketPageMarginTextBoxRequiredFieldValidator"
                                runat="server" ControlToValidate="GridViewTicketPageMarginTextBox" ErrorMessage="El campo Margenes es requerido."
                                Display="Dynamic" class="error_msg">*</asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Name" runat="server" Text='<%# Bind("TicketPageMargin") %>' />
                        </ItemTemplate>
                        <ControlStyle Width="70px"></ControlStyle>
                        <ItemStyle Width="70px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tamaño de Página" SortExpression="TicketPageSize">
                        <EditItemTemplate>
                            <asp:TextBox ID="GridViewTicketPageSizeTextBox" runat="server" Text='<%# Bind("TicketPageSize") %>' />
                            <asp:RequiredFieldValidator ID="GridViewTicketPageSizeRequiredFieldValidator" runat="server"
                                ControlToValidate="GridViewTicketPageSizeTextBox" ErrorMessage="El campo Tamaño de Página es requerido."
                                Display="Dynamic" class="error_msg">*</asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Name" runat="server" Text='<%# Bind("TicketPageSize") %>' />
                        </ItemTemplate>
                        <ControlStyle Width="70px"></ControlStyle>
                        <ItemStyle Width="70px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:CheckBoxField DataField="Disabled" HeaderText="Desabilitada" SortExpression="Disabled">
                        <ControlStyle Width="60px"></ControlStyle>
                        <ItemStyle Width="60px"></ItemStyle>
                    </asp:CheckBoxField>
                </Columns>
            </asp:GridView>
            <asp:LinqDataSource ID="CashiersLinqDataSource" runat="server" ContextTypeName="Mega.Common.AdminDataContext"
                EnableUpdate="True" EntityTypeName="" TableName="Cashiers" Where="IdShop == @IdShop"
                OnContextCreating="LinqDataSource1_OnContextCreating">
                <WhereParameters>
                    <asp:QueryStringParameter Name="IdShop" QueryStringField="id" Type="String" />
                </WhereParameters>
            </asp:LinqDataSource>
        </div>
        <div class="doble_column_content">
            <p>
                <b>
                    <label>
                        Nueva Caja:
                    </label>
                </b>
                <asp:TextBox ID="CashierNameTextBox" runat="server" />
            </p>
            <p>
                <b>
                    <label>
                        Tickets a imprimir:
                    </label>
                </b>
                <asp:TextBox ID="TicketCountToPrintTextBox" runat="server" />
            </p>
        </div>
        <div class="doble_column_content">
            <p>
                <b>
                    <label>
                        Margenes:
                    </label>
                </b>
                <asp:TextBox ID="TicketPageMarginTextBox" runat="server" />
            </p>
            <p>
                <b>
                    <label>
                        Tamaño de Página:
                    </label>
                </b>
                <asp:TextBox ID="TicketPageSizeTextBox" runat="server" />
            </p>
            <p>
                <label>
                    &nbsp;
                </label>
                <asp:Button ID="AddCashierButton" runat="server" Text="Agregar" OnClick="AddCashierButton_Click"
                    ValidationGroup="Cashier" />
            </p>
        </div>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="El campo Nueva Caja es requerido."
            ValidationGroup="Cashier" ControlToValidate="CashierNameTextBox" Display="None" />
        <asp:RequiredFieldValidator ID="TicketCountToPrintRequiredFieldValidator" runat="server"
            ErrorMessage="El campo Tickets a imprimir es requerido." ValidationGroup="Cashier"
            ControlToValidate="TicketCountToPrintTextBox" Display="None" />
        <asp:RegularExpressionValidator ID="TicketCountToPrintRegularExpressionValidator1"
            runat="server" ControlToValidate="TicketCountToPrintTextBox" ErrorMessage="El campo Tickets a imprimir no es válido."
            ValidationExpression="^\d+$" Display="None" ValidationGroup="Cashier"></asp:RegularExpressionValidator>
        <asp:RequiredFieldValidator ID="TicketPageMarginRequiredFieldValidator" runat="server"
            ErrorMessage="El campo Margenes es requerido." ValidationGroup="Cashier" ControlToValidate="TicketPageMarginTextBox"
            Display="None" />
        <asp:RequiredFieldValidator ID="TicketPageSizeRequiredFieldValidator" runat="server"
            ErrorMessage="El campo Tamaño de Página es requerido." ValidationGroup="Cashier"
            ControlToValidate="TicketPageSizeTextBox" Display="None" />
        <asp:ValidationSummary ID="ValidationSummary2" runat="server" class="error_msg" ValidationGroup="Cashier"
            ShowMessageBox="true" ShowSummary="false" />
    </fieldset>
    <div class="separator">
    </div>
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
</asp:Content>

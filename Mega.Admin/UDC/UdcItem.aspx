<%@ Page Title="Administración de UDC (Codificadores" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="UdcItem.aspx.cs" Inherits="Mega.Admin.UDC.UdcItem" %>

<%@ Import Namespace="System.Security.AccessControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Administración de UDC (Codificadores)
    </h2>
    <p>
        <label>
            Seleccione la UDC que desea listar/editar:</label>&nbsp;<asp:DropDownList ID="UDCDropDownList"
                runat="server" DataSourceID="UDCLinqDataSource" DataTextField="IdName" DataValueField="Id"
                AutoPostBack="true" OnSelectedIndexChanged="UDCDropDownList_SelectedIndexChanged"
                OnDataBound="UDCDropDownList_SelectedIndexChanged">
            </asp:DropDownList>
        &nbsp;<asp:Label ID="UDCStateLabel" runat="server" Text=""></asp:Label>
    </p>
    <asp:Panel ID="UDCDescPanel" runat="server" CssClass="information">
        <asp:Label ID="UDCDescLabel" runat="server" Text=""></asp:Label>
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Styles/Images/information.png" />
    </asp:Panel>
    <br />
    <div class="table_container">
        <asp:GridView ID="UDCItemsGridView" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            DataSourceID="UDCItemsLinqDataSource" DataKeyNames="Id">
            <Columns>
                <asp:CommandField CancelText="Cancelar" DeleteText="Eliminar" EditText="Editar" InsertText="Insertar"
                    NewText="Nuevo" SelectText="Seleccionar" ShowEditButton="True" UpdateText="Guardar">
                    <ItemStyle Width="120px"></ItemStyle>
                </asp:CommandField>
                <asp:BoundField DataField="Code" HeaderText="Código" SortExpression="Code">
                    <ControlStyle Width="60px"></ControlStyle>
                    <ItemStyle Width="60px"></ItemStyle>
                </asp:BoundField>
                <asp:TemplateField HeaderText="Nombre" SortExpression="Optional4">
                    <EditItemTemplate>
                        <asp:TextBox ID="GridViewNameTextBox" runat="server" Text='<%# Bind("Name") %>' />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="GridViewNameTextBox"
                            ErrorMessage="El campo Nombre es Requerido" Display="Dynamic" class="error_msg">*</asp:RequiredFieldValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Name" runat="server" Text='<%# Bind("Name") %>' />
                    </ItemTemplate>
                    <ControlStyle Width="45%"></ControlStyle>
                    <ItemStyle Width="45%"></ItemStyle>
                </asp:TemplateField>
                <asp:BoundField DataField="Optional1" HeaderText="Opcional1" SortExpression="Optional1">
                    <ControlStyle Width="60px"></ControlStyle>
                    <ItemStyle Width="60px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Optional2" HeaderText="Opcional2" SortExpression="Optional2">
                    <ControlStyle Width="60px"></ControlStyle>
                    <ItemStyle Width="60px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Optional3" HeaderText="Opcional3" SortExpression="Optional3">
                    <ControlStyle Width="60px"></ControlStyle>
                    <ItemStyle Width="60px"></ItemStyle>
                </asp:BoundField>
                <asp:TemplateField HeaderText="Opcional4" SortExpression="Optional4">
                    <EditItemTemplate>
                        <asp:TextBox TextMode="MultiLine" ID="Optional4" runat="server" Text='<%# Bind("Optional4") %>' />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Optional4" runat="server" Text='<%# Bind("Optional4") %>' />
                    </ItemTemplate>
                    <ControlStyle Width="150px"></ControlStyle>
                    <ItemStyle Width="150px"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Desabilitado" InsertVisible="False" SortExpression="Disabled">
                    <EditItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("Disabled") %>' />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("Disabled") %>' Enabled="false" />
                    </ItemTemplate>
                    <ControlStyle Width="60px"></ControlStyle>
                    <ItemStyle Width="60px"></ItemStyle>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <fieldset>
        <legend>Agregar nuevo Elemento</legend>
        <div class="doble_column_content">
            <p>
                <label>
                    Código:
                </label>
                <asp:TextBox ID="CodeTextBox" runat="server" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="El campo Código es requerido. "
                    ControlToValidate="CodeTextBox" Display="None" ValidationGroup="Add" />
            </p>
            <p>
                <label>
                    Nombre:
                </label>
                <asp:TextBox ID="NameTextBox" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="El campo Nombre es requerido. "
                    ControlToValidate="NameTextBox" Display="None" ValidationGroup="Add" />
            </p>
        </div>
        <div class="doble_column_content">
            <p>
                <label>
                    Info1 (opcional):
                </label>
                <asp:TextBox ID="Optional1TextBox" runat="server"></asp:TextBox>
            </p>
            <p>
                <label>
                    Info2 (opcional):
                </label>
                <asp:TextBox ID="Optional2TextBox" runat="server"></asp:TextBox>
            </p>
            <p>
                <label>
                    Info3 (opcional):
                </label>
                <asp:TextBox ID="Optional3TextBox" runat="server"></asp:TextBox>
            </p>
            <p>
                <label>
                    Info4 (opcional):
                </label>
                <asp:TextBox ID="Optional4TextBox" runat="server" TextMode="MultiLine"></asp:TextBox>
            </p>
            <p>
                <label>
                    &nbsp;
                </label>
                <asp:Button ID="AddUDCItemButton" runat="server" Text="Agregar" OnClick="AddUDCItemButton_Click"
                    ValidationGroup="Add" />
            </p>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                ShowSummary="false" ValidationGroup="Add" />
        </div>
    </fieldset>
    <asp:LinqDataSource ID="UDCLinqDataSource" runat="server" ContextTypeName="Mega.Common.AdminDataContext"
        EntityTypeName="" OnContextCreating="LinqDataSource1_OnContextCreating" Select="new (Id, Name, Id + '-' + Name as IdName, AllowEdit, AllowAdd, Disabled)"
        TableName="UDCs">
    </asp:LinqDataSource>
    <asp:QueryExtender ID="QueryExtender1" runat="server" TargetControlID="UDCItemsLinqDataSource">
        <asp:PropertyExpression>
            <asp:ControlParameter ControlID="UDCDropDownList" Name="IdUDC" />
        </asp:PropertyExpression>
    </asp:QueryExtender>
    <asp:LinqDataSource ID="UDCItemsLinqDataSource" runat="server" ContextTypeName="Mega.Common.AdminDataContext"
        EnableInsert="True" EnableUpdate="True" TableName="UDCItems" OnContextCreating="LinqDataSource1_OnContextCreating"
        OnUpdating="UDCItemsLinqDataSource_Updating" Where="Code != @Code">
        <WhereParameters>
            <asp:Parameter DefaultValue="WEB_SYNC" Name="Code" Type="String" />
        </WhereParameters>
    </asp:LinqDataSource>
</asp:Content>

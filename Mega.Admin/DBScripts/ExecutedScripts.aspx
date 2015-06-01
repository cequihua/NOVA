<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ExecutedScripts.aspx.cs" Inherits="Mega.Admin.DBScripts.ExecutedScripts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Scripts de Actualización Ejecutados
    </h2>
    <p>
    </p>
    <div id="product-list">
        <fieldset>
            <legend>Filtros</legend>
            <div class="doble_column_content">
                <p>
                    <label>
                        Solo para:
                    </label>
                    <asp:DropDownList ID="ShopsDropDownList" runat="server" DataTextField="Name" DataValueField="Id">
                    </asp:DropDownList>
                </p>
            </div>
            <div class="doble_column_content">
                <p>
                    <label>
                        &nbsp;
                    </label>
                    <asp:Button ID="FilterButton" runat="server" Text="Filtrar" OnClick="FilterButton_Click"
                        CausesValidation="false" />
                </p>
            </div>
        </fieldset>
    </div>
    <p>
    </p>
    <div class="table_container">
        <asp:GridView ID="ScriptsGridView" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            DataKeyNames="Id" AllowPaging="true" PagerSettings-Visible="true" OnSorting="ScriptsGridView_Sorting"
            OnPageIndexChanging="ScriptsGridView_PageIndexChanging" PagerStyle-CssClass="grid-pager"
            EmptyDataRowStyle-CssClass="grid-empty-data" EmptyDataText="No existen Scripts de Actualización Ejecutados."
             OnSelectedIndexChanged="ScriptsGridView_SelectedIndexChanged">
            <Columns>
                <asp:CommandField CancelText="Cancelar" DeleteText="Eliminar" EditText="Editar" InsertText="Insertar"
                    NewText="Nuevo" SelectText="Eliminar" ShowSelectButton="True" UpdateText="Guardar">
                    <ItemStyle Width="50px"></ItemStyle>
                </asp:CommandField>
                <asp:BoundField DataField="IdScript" HeaderText="ID Script" SortExpression="IdScript">
                    <ControlStyle Width="200px"></ControlStyle>
                    <ItemStyle Width="200px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="IdShop" HeaderText="ID Tienda" SortExpression="IdShop">
                    <ControlStyle Width="200px"></ControlStyle>
                    <ItemStyle Width="200px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="ModifiedDate" HeaderText="Fecha" SortExpression="ModifiedDate">
                    <ControlStyle Width="145px"></ControlStyle>
                    <ItemStyle Width="145px"></ItemStyle>
                </asp:BoundField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>

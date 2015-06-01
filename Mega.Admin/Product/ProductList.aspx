<%@ Page Title="Lista de Productos" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="ProductList.aspx.cs" Inherits="Mega.Admin.Product.ProductList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Administración de Productos
    </h2>
    <p>
    </p>
    <div id="product-list">
        <fieldset>
            <legend>Filtros</legend>
            <div class="doble_column_content">
                <p>
                    <label>
                        Solo Productos con precio específico para la Tienda:
                    </label>
                    <asp:DropDownList ID="ShopsDropDownList" runat="server" DataTextField="Name" DataValueField="Id">
                    </asp:DropDownList>
                </p>
                <p>
                    <label>
                        Solo Productos en Promoción:
                    </label>
                    <asp:CheckBox ID="OnlyPromortionsCheckBox" runat="server" />
                </p>
            </div>
            <div class="doble_column_content">
                <p>
                    <label>
                        No. Prod./Cód. Barra:
                    </label>
                    <asp:TextBox ID="IDOrBarCodeTextBox" runat="server" Text="" />
                </p>
                <p>
                    <label>
                        Solo Productos que aplican para Descuentos:
                    </label>
                    <asp:CheckBox ID="OnlyApplyDiscountCheckBox" runat="server" />
                </p>
                <p>
                </p>
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
        <asp:GridView ID="ProductsGridView" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            DataKeyNames="Id" AllowPaging="true" PagerSettings-Visible="true" OnPageIndexChanging="ProductsGridView_PageIndexChanging"
            OnSorting="ProductsGridView_Sorting" PagerStyle-CssClass="grid-pager">
            <Columns>
                <asp:HyperLinkField HeaderText="Opciones" Text="Detalles" DataNavigateUrlFields="Id"
                    DataNavigateUrlFormatString="~/Product/Product.aspx?id={0}" />
                <asp:BoundField DataField="Id" HeaderText="No. Producto" SortExpression="Id">
                    <ControlStyle Width="50px"></ControlStyle>
                    <ItemStyle Width="50px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="UMCode" HeaderText="UM" SortExpression="UMCode">
                    <ControlStyle Width="40px"></ControlStyle>
                    <ItemStyle Width="40px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Name" HeaderText="Nombre" SortExpression="Name">
                    <ControlStyle Width="310px"></ControlStyle>
                    <ItemStyle Width="310px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="ProductTypeName" HeaderText="Tipo" SortExpression="ProductTypeName">
                    <ControlStyle Width="130px"></ControlStyle>
                    <ItemStyle Width="130px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="BarCode" HeaderText="Cod. Barra" SortExpression="BarCode">
                    <ControlStyle Width="100px"></ControlStyle>
                    <ItemStyle Width="100px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="IVATypeAsText" HeaderText="Tipo IVA" SortExpression="IVATypeAsText">
                    <ControlStyle Width="90px"></ControlStyle>
                    <ItemStyle Width="90px"></ItemStyle>
                </asp:BoundField>
                <asp:CheckBoxField DataField="ApplyDiscount" HeaderText="Aplica Descuento" ReadOnly="True"
                    SortExpression="ApplyDiscount" />
                <asp:CheckBoxField DataField="Disabled" HeaderText="Desactivado" ReadOnly="True"
                    SortExpression="Disabled" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>

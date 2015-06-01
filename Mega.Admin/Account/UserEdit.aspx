<%@ Page Title="Editar Usuario" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="UserEdit.aspx.cs" Inherits="Mega.Admin.Account.UserEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Editar Usuario
    </h2>
    <p>
        <asp:HyperLink runat="server" ID="UserList" NavigateUrl="~/Account/UserList.aspx"
            Text="Regresar a la Lista de Usuarios" />
    </p>
    <div class="doble_column_content">
        <fieldset>
            <legend runat="server" id="userLegend">Datos del Usuario</legend>
            <p>
                <label>
                    Login:
                </label>
                <asp:TextBox ID="LoginTextBox" runat="server" ReadOnly="true"></asp:TextBox>
            </p>
            <p>
                <label>
                    Nombre:
                </label>
                <asp:TextBox ID="NameTextBox" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="NameTextBox"
                    ErrorMessage="El campo Nombre es Requerido" Display="None" />
            </p>
            <p>
                <label>
                    Email:
                </label>
                <asp:TextBox ID="EmailTextBox" runat="server" />
                <asp:RequiredFieldValidator ID="mailRequiredFieldValidator" runat="server" Display="None"
                    ControlToValidate="emailTextBox" ErrorMessage="El campo Email es Requerido" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="emailTextBox"
                    ErrorMessage="El formato del Email no es correcto" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    Display="None" />
            </p>
            <p>
                <label>
                    Contraseña:
                </label>
                <asp:TextBox ID="PasswordTextBox" runat="server" />
            </p>
            
            <p>
                <label>
                    &nbsp;
                </label>
                <asp:Button ID="EditButton" runat="server" Text="Guardar" OnClick="EditButton_Click" />
            </p>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" class="error_msg" ShowMessageBox="true"
                ShowSummary="false" />
        </fieldset>
    </div>
    <div class="doble_column_content">
        <fieldset>
            <legend>Opciones Disponibles</legend>
            <p>
                <asp:Button ID="PswResetButton" runat="server" Text="Resetear Contraseña sin Notificar"
                    OnClick="PswResetButton_Click" CssClass="big_button" OnClientClick="javascript:return confirm('Está seguro que desea resetear la contraseña del Usuario? Deberá notificarle posteriormente.')" />
                <asp:Button ID="PswResetAndNotifyButton" runat="server" Text="Resetear Contraseña y Notificar al Usuario"
                    CssClass="big_button" OnClick="PswResetAndNotifyButton_Click" OnClientClick="javascript:return confirm('Está seguro que desea resetear la contraseña del Usuario? Esta acción será notificada con un Email .')" />
                <asp:Button ID="NotifyUserButton" runat="server" Text="Enviar Email al usuario con sus Credenciales y Permisos"
                    CssClass="big_button" OnClick="NotifyUserButton_Click" OnClientClick="javascript:return confirm('Está seguro que desea enviar un Email al Usuario con sus Credenciales y sus Permisos? No podrá eliminarse posteriormente.')" /><asp:Button
                        ID="DisableUserButton" runat="server" Text="Desabilitar este Usuario" CssClass="big_button"
                        OnClick="DisableUserButton_Click" OnClientClick="javascript:return confirm('Está seguro que desea insertar Habilitar/Desabilitar a este Usuario?')" />
            </p>
        </fieldset>
    </div>
    <div class="separator">
    </div>
    <div class="doble_column_content">
        <fieldset>
            <legend>Roles permitidos a este Usuario</legend>
            <asp:CheckBoxList ID="RolesCheckBoxList" runat="server">
                <asp:ListItem>Rol1</asp:ListItem>
            </asp:CheckBoxList>
            <p>
                <asp:Button ID="SaveRolesButton" CausesValidation="false" runat="server" Text="Guardar"
                    OnClick="SaveRolesButton_Click" />
            </p>
        </fieldset>
    </div>
    <div class="doble_column_content">
        <fieldset>
            <legend>Acceso a Tiendas (CNV) permitido a este usuario</legend>
            <asp:CheckBoxList ID="ShopsCheckBoxList" runat="server" RepeatColumns="2">
                <asp:ListItem>Tienda1</asp:ListItem>
            </asp:CheckBoxList>
            <p>
                <asp:Button ID="SaveShopsButton" CausesValidation="false" runat="server" Text="Guardar"
                    OnClick="SaveShopsButton_Click" />
            </p>
        </fieldset>
    </div>
    <div class="separator">
    </div>
       
</asp:Content>

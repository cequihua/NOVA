<%@ Page Title="Agregar Usuario" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="UserAdd.aspx.cs" Inherits="Mega.Admin.Account.UserAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Agregar Usuario al Sistema
    </h2>
    <p>
        <asp:HyperLink runat="server" ID="UserList" NavigateUrl="~/Account/UserList.aspx"
            Text="Regresar a la Lista de Usuarios" />
    </p>
    <fieldset>
        <legend>Datos del Usuario</legend>
        <p>
            <label>
                Login:
            </label>
            <asp:TextBox ID="LoginTextBox" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="LoginTextBox"
                ErrorMessage="El campo Login es Requerido" Display="None" />
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
            <asp:Button ID="AddButton" runat="server" Text="Agregar" OnClick="AddButton_Click" 
                OnClientClick="javascript:return confirm('Está seguro que desea insertar un nuevo Usuario? No podrá eliminarse posteriormente.')"/>
        </p>
    </fieldset>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" class="error_msg" />
    <br />
    
</asp:Content>

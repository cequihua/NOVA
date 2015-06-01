<%@ Page Title="Página de autenticación del Sistema" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Mega.Admin.Account.Login" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Página de autenticación del Sistema
    </h2>
    <div style="width: 330px; margin: auto;">
        <fieldset>
            <legend>Credenciales de Acceso</legend>
            <p>
                <label>
                    Usuario:</label>
                <asp:TextBox ID="txtLogin" runat="server" />
            </p>
            <p>
                <label>
                    Contraseña:</label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" />
            </p>
            <p>
                <label>
                    &nbsp;</label>
                <asp:Button ID="loginButton" runat="server" Text="Entrar" OnClick="loginButton_Click" />
            </p>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                class="error_msg" />
            <asp:RequiredFieldValidator ID="loginRequiredValidator" runat="server" Display="None"
                ControlToValidate="txtLogin" ErrorMessage="Debe introducir su Usuario (login)" />
            <asp:RegularExpressionValidator ID="UsernameValidator" runat="server" ControlToValidate="txtLogin"
                Display="None" ErrorMessage="Ha introducido caracteres no válidos en Usuario (login)"
                ValidationExpression="[\w| ]*" />
            <asp:RequiredFieldValidator ID="pswRequiredFieldValidator" runat="server" Display="None"
                ControlToValidate="txtPassword" ErrorMessage="Debe introducir su Contraseña" />
            <asp:Label runat="server" ID="ErrorLabel" class="error_msg"/>
        </fieldset>
    </div>
</asp:Content>

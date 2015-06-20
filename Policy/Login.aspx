<%@ Page Title="Policy - Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Policy_Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1><%: Page.Title %></h1>
        <fieldset>
            <legend>
                <asp:Label ID="LabelLogin" runat="server" Text="Login"></asp:Label>
            </legend>
            <label for="PolicyNumberTextBox">Policy Number:</label>
            <asp:TextBox ID="PolicyNumberTextBox" runat="server" CssClass="textbox"></asp:TextBox>
            <asp:Label ID="PolicyNumberErrorLabel" runat="server"></asp:Label>
            <br />

            <label for="PasswordTextBox">Password:</label>
            <asp:TextBox ID="PasswordTextBox" runat="server" TextMode="Password" CssClass="textbox"></asp:TextBox>
            <asp:Label ID="PasswordErrorLabel" runat="server"></asp:Label>
            <br />

            <asp:Button ID="Login" runat="server" Text="Login" CssClass="button" OnClick="Login_Click" />
        </fieldset>
        <p>
            <asp:Label ID="ErrorLabel" runat="server"></asp:Label>
        </p>
    </div>
</asp:Content>
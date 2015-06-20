<%@ Page Title="New Quote - Password" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Password.aspx.cs" Inherits="NewQuote_Password" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1><%: Page.Title %></h1>
        <fieldset>
            <legend>
                <asp:Label ID="LabelPassword" runat="server" Text="Specify a password for future access"></asp:Label>
            </legend>
            <label for="Password1TextBox">Password:</label>
            <asp:TextBox ID="Password1TextBox" runat="server" TextMode="Password" CssClass="textbox" MaxLength="50"></asp:TextBox>
            <asp:Label ID="Password1ErrorLabel" runat="server"></asp:Label>
            <br />
            
            <label for="Password2TextBox">Retype Password:</label>
            <asp:TextBox ID="Password2TextBox" runat="server" TextMode="Password" CssClass="textbox" MaxLength="50"></asp:TextBox>
            <asp:Label ID="Password2ErrorLabel" runat="server"></asp:Label>
            <br />

            <asp:Button ID="Payment" runat="server" Text="Go to Payment" CssClass="button" OnClick="Payment_Click" />
        </fieldset>
    </div>
</asp:Content>

<%@ Page Title="New Quote - Payment" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Payment.aspx.cs" Inherits="NewQuote_Payment" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1><%: Page.Title %></h1>
        <fieldset>
            <legend>
                <asp:Label ID="Payment" runat="server" Text="Payment"></asp:Label>
            </legend>
            <p><asp:Label ID="PaymentAmountLabel" runat="server"></asp:Label></p>
            <br />

            <label for="CardTypeDropDownList">Card Type:</label>
            <asp:DropDownList ID="CardTypeDropDownList" runat="server"></asp:DropDownList>
            <asp:Label ID="CardTypeErrorLabel" runat="server" Text="*"></asp:Label>
            <br />

            <label for="CardNumberTextBox">Card Number:</label>
            <asp:TextBox ID="CardNumberTextBox" runat="server" CssClass="textbox" MaxLength="19"></asp:TextBox>
            <asp:Label ID="CardNumberErrorLabel" runat="server" Text="*"></asp:Label>
            <br />
            
            <label for="CardHoldersNameTextBox">Cardholder's Name:</label>
            <asp:TextBox ID="CardHoldersNameTextBox" runat="server" CssClass="textbox" MaxLength="75"></asp:TextBox>
            <asp:Label ID="CardHoldersNameErrorLabel" runat="server" Text="*"></asp:Label>
            <br />
            
            <label for="ExpirationDateTextBox">Expiration Date:</label>
            <asp:TextBox ID="ExpirationDateTextBox" runat="server" CssClass="textbox" MaxLength="10"></asp:TextBox>
            <asp:Label ID="ExpirationDateErrorLabel" runat="server" Text="*"></asp:Label>
            <br />

            <asp:Button ID="MakePayment" runat="server" Text="Make Payment" CssClass="button" OnClick="MakePayment_Click" />
        </fieldset>
    </div>
</asp:Content>
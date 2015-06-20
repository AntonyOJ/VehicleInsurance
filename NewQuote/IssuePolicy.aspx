<%@ Page Title="New Quote - Issue Policy" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="IssuePolicy.aspx.cs" Inherits="NewQuote_IssuePolicy" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1><%: Page.Title %></h1>
        <p><asp:Label ID="PaymentLabel" runat="server"></asp:Label></p>
        <p><asp:Label ID="PolicyNumberLabel" runat="server"></asp:Label></p>        
        <p><a href="Details" class="btn btn-primary btn-lg">Get another quote &raquo;</a></p>
    </div>
</asp:Content>
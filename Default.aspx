<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1><%: Page.Title %></h1>
        <p class="lead">Erehwon Motor Insurance can provide a quote for you free of charge.</p>
        <p>
            <a href="NewQuote/Details" class="btn btn-primary btn-lg">Get a quote &raquo;</a>
            <asp:HyperLink ID="ContinueWithNewQuoteHyperLink" runat="server" NavigateUrl="~/NewQuote/Payment" Visible="false">Continue where you left off</asp:HyperLink>
        </p>
        <p>
            <a href="Policy/Login" class="btn btn-primary btn-lg">Access the details of an existing policy &raquo;</a>
            <asp:HyperLink ID="AccessLoggedInPolicyHyperLink" runat="server" NavigateUrl="~/Policy/Details" Visible="false">Access currently logged in policy</asp:HyperLink>
        </p>        
    </div>
</asp:Content>
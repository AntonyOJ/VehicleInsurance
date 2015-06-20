<%@ Page Title="New Quote - Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Details.aspx.cs" Inherits="NewQuote_Details" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1><%: Page.Title %></h1>
        <fieldset>
            <legend>
                <asp:Label ID="VehicleLabel" runat="server" Text="Vehicle"></asp:Label>
            </legend>
            <asp:UpdatePanel ID="UpdatePanel" runat="server">
                <ContentTemplate>
                    <label for="VehicleMakeDropDownList">Make:</label>
                    <asp:DropDownList ID="VehicleMakeDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="VehicleMakeDropDownList_SelectedIndexChanged"></asp:DropDownList>
                    <asp:Label ID="VehicleMakeErrorLabel" runat="server" Text="*"></asp:Label>
                    <br />

                    <label for="VehicleModelDropDownList">Model:</label>
                    <asp:DropDownList ID="VehicleModelDropDownList" runat="server" Enabled="False" AutoPostBack="True" OnSelectedIndexChanged="VehicleModelDropDownList_SelectedIndexChanged"></asp:DropDownList>
                    <asp:Label ID="VehicleModelErrorLabel" runat="server" Text="*"></asp:Label>
                    <br />

                    <label for="VehicleYearOfManufactorDropDownList">Year of Manufactor:</label>
                    <asp:DropDownList ID="VehicleYearOfManufactorDropDownList" runat="server" Enabled="False" AutoPostBack="True" OnSelectedIndexChanged="VehicleYearOfManufactorDropDownList_SelectedIndexChanged"></asp:DropDownList>
                    <asp:Label ID="VehicleYearOfManufactorErrorLabel" runat="server" Text="*"></asp:Label>
                    <br />

                    <label for="VehicleEngineCapacityDropDownList">Engine Capacity:</label>
                    <asp:DropDownList ID="VehicleEngineCapacityDropDownList" runat="server" Enabled="False" AutoPostBack="True" OnSelectedIndexChanged="VehicleEngineCapacityDropDownList_SelectedIndexChanged"></asp:DropDownList>
                    <asp:Label ID="VehicleEngineCapacityErrorLabel" runat="server" Text="*"></asp:Label>
                    <br />
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:Label ID="VehicleErrorLabel" runat="server"></asp:Label>
        </fieldset>

        <fieldset>
            <legend>
                <asp:Label ID="LabelPersonalDetails" runat="server" Text="Personal Details"></asp:Label>
            </legend>
            <label for="DriverForenameTextBox">Forename:</label>
            <asp:TextBox ID="DriverForenameTextBox" runat="server" CssClass="textbox" MaxLength="35"></asp:TextBox>
            <asp:Label ID="DriverForenameErrorLabel" runat="server" Text="*"></asp:Label>
            <br />

            <label for="DriverSurnameTextBox">Surname:</label>
            <asp:TextBox ID="DriverSurnameTextBox" runat="server" CssClass="textbox" MaxLength="35"></asp:TextBox>
            <asp:Label ID="DriverSurnameErrorLabel" runat="server" Text="*"></asp:Label>
            <br />

            
            <label for="DriverDateOfBirthTextBox">Date of Birth:</label>
            <asp:TextBox ID="DriverDateOfBirthTextBox" runat="server" CssClass="textbox" MaxLength="10"></asp:TextBox>
            <asp:Label ID="DriverDateOfBirthErrorLabel" runat="server" Text="*"></asp:Label>
            <br />

            <label for="DriverDrivingLicenseNumberTextBox">Driving License Number:</label>
            <asp:TextBox ID="DriverDrivingLicenseNumberTextBox" runat="server" CssClass="textbox" MaxLength="19"></asp:TextBox>
            <asp:Label ID="DriverDrivingLicenseNumberErrorLabel" runat="server" Text="*"></asp:Label>
            <br />

            <label for="DriverEmailAddressTextBox">E-mail Address:</label>
            <asp:TextBox ID="DriverEmailAddressTextBox" runat="server" CssClass="textbox" MaxLength="254"></asp:TextBox>
            <asp:Label ID="DriverEmailAddressErrorLabel" runat="server" Text="*"></asp:Label>
            <br />
        </fieldset>

        <fieldset>
            <legend>
                <asp:Label ID="AddressLabel" runat="server" Text="Address"></asp:Label>
            </legend>
            <label for="AddressLine1TextBox">Line 1:</label>
            <asp:TextBox ID="AddressLine1TextBox" runat="server" CssClass="textbox" MaxLength="35"></asp:TextBox>
            <asp:Label ID="AddressLine1ErrorLabel" runat="server" Text="*"></asp:Label>
            <br />

            <label for="AddressLine2TextBox">Line 2:</label>
            <asp:TextBox ID="AddressLine2TextBox" runat="server" CssClass="textbox" MaxLength="35"></asp:TextBox>
            <asp:Label ID="AddressLine2ErrorLabel" runat="server"></asp:Label>
            <br />

            <label for="AddressLine3TextBox">Line 3:</label>
            <asp:TextBox ID="AddressLine3TextBox" runat="server" CssClass="textbox" MaxLength="35"></asp:TextBox>
            <asp:Label ID="AddressLine3ErrorLabel" runat="server"></asp:Label>
            <br />

            <label for="AddressCityTextBox">City:</label>
            <asp:TextBox ID="AddressCityTextBox" runat="server" CssClass="textbox" MaxLength="35"></asp:TextBox>
            <asp:Label ID="AddressCityErrorLabel" runat="server" Text="*"></asp:Label>
            <br />

            <label for="AddressCountyTextBox">County:</label>
            <asp:TextBox ID="AddressCountyTextBox" runat="server" CssClass="textbox" MaxLength="35"></asp:TextBox>
            <asp:Label ID="AddressCountyErrorLabel" runat="server" Text="*"></asp:Label>
            <br />

            <label for="AddressPostcodeTextBox">Postcode:</label>
            <asp:TextBox ID="AddressPostcodeTextBox" runat="server" CssClass="textbox" MaxLength="8"></asp:TextBox>
            <asp:Label ID="AddressPostcodeErrorLabel" runat="server" Text="*"></asp:Label>
            <br />
        </fieldset>

        <fieldset>
            <legend>
                <asp:Label ID="DrivingHistoryLabel" runat="server" Text="Driving History"></asp:Label>
            </legend>
            <label for="DriverDateOfDrivingTestPassTextBox">Date of Driving Test Pass:</label>
            <asp:TextBox ID="DriverDateOfDrivingTestPassTextBox" runat="server" CssClass="textbox" MaxLength="10"></asp:TextBox>
            <asp:Label ID="DriverDateOfDrivingTestPassErrorLabel" runat="server" Text="*"></asp:Label>
            <br />

            <label for="DriverDateOfLastClaimTextBox">Date of Last Claim:</label>
            <asp:TextBox ID="DriverDateOfLastClaimTextBox" runat="server" CssClass="textbox" MaxLength="10"></asp:TextBox>
            <asp:Label ID="DriverDateOfLastClaimErrorLabel" runat="server" Text="Leave blank if not applicable"></asp:Label>
            <br />
        </fieldset>
        <asp:Button ID="GetQuote" runat="server" Text="Get Quote" CssClass="button" OnClick="GetQuote_Click" />
    </div>
</asp:Content>
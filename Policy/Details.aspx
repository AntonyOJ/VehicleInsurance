<%@ Page Title="Policy - Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Details.aspx.cs" Inherits="Policy_Details" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1><%: Page.Title %></h1>
        <fieldset>
            <legend>
                <asp:Label ID="PolicyDetailsLabel" runat="server"></asp:Label>
            </legend>
            <label for="PolicyQuotationLabel">Quote:</label>
            <asp:Label ID="PolicyQuotationLabel" runat="server"></asp:Label>
            <br />

            <label for="PolicyDateOfIssueLabel">Date of Issue:</label>
            <asp:Label ID="PolicyDateOfIssueLabel" runat="server"></asp:Label>
            <br />
        </fieldset>

        <fieldset>
            <legend>
                <asp:Label ID="VehicleLabel" runat="server" Text="Vehicle"></asp:Label>
            </legend>
            <label for="VehicleMakeLabel">Make:</label>
            <asp:Label ID="VehicleMakeLabel" runat="server"></asp:Label>
            <br />

            <label for="VehicleModelLabel">Model:</label>
            <asp:Label ID="VehicleModelLabel" runat="server"></asp:Label>
            <br />

            <label for="VehicleYearOfManufactorLabel">Year of Manufactor:</label>
            <asp:Label ID="VehicleYearOfManufactorLabel" runat="server"></asp:Label>
            <br />

            <label for="VehicleEngineCapacityLabel">Engine Capacity:</label>
            <asp:Label ID="VehicleEngineCapacityLabel" runat="server"></asp:Label>
            <br />
        </fieldset>

        <fieldset>
            <legend>
                <asp:Label ID="LabelPersonalDetails" runat="server" Text="Personal Details"></asp:Label>
            </legend>
            <label for="DriverForenameLabel">Forename:</label>
            <asp:Label ID="DriverForenameLabel" runat="server"></asp:Label>
            <br />

            <label for="DriverSurnameLabel">Surname:</label>
            <asp:Label ID="DriverSurnameLabel" runat="server"></asp:Label>
            <br />

            
            <label for="DriverDateOfBirthLabel">Date of Birth:</label>
            <asp:Label ID="DriverDateOfBirthLabel" runat="server"></asp:Label>
            <br />

            <label for="DriverDrivingLicenseNumberLabel">Driving License Number:</label>
            <asp:Label ID="DriverDrivingLicenseNumberLabel" runat="server"></asp:Label>
            <br />

            <label for="DriverEmailAddressLabel">E-mail Address:</label>
            <asp:Label ID="DriverEmailAddressLabel" runat="server"></asp:Label>
            <br />
        </fieldset>

        <fieldset>
            <legend>
                <asp:Label ID="AddressLabel" runat="server" Text="Address"></asp:Label>
            </legend>
            <label for="AddressLine1Label">Line 1:</label>
            <asp:Label ID="AddressLine1Label" runat="server"></asp:Label>
            <br />

            <label for="AddressLine2Label">Line 2:</label>
            <asp:Label ID="AddressLine2Label" runat="server"></asp:Label>
            <br />

            <label for="AddressLine3Label">Line 3:</label>
            <asp:Label ID="AddressLine3Label" runat="server"></asp:Label>
            <br />

            <label for="AddressCityLabel">City:</label>
            <asp:Label ID="AddressCityLabel" runat="server"></asp:Label>
            <br />

            <label for="AddressCountyLabel">County:</label>
            <asp:Label ID="AddressCountyLabel" runat="server"></asp:Label>
            <br />

            <label for="AddressPostcodeLabel">Postcode:</label>
            <asp:Label ID="AddressPostcodeLabel" runat="server"></asp:Label>
            <br />
        </fieldset>

        <fieldset>
            <legend>
                <asp:Label ID="DrivingHistoryLabel" runat="server" Text="Driving History"></asp:Label>
            </legend>
            <label for="DrivingHistoryDateOfDrivingTestPassLabel">Date of Driving Test Pass:</label>
            <asp:Label ID="DrivingHistoryDateOfDrivingTestPassLabel" runat="server"></asp:Label>
            <br />

            <label for="DrivingHistoryDateOfLastClaimLabel">Date of Last Claim:</label>
            <asp:Label ID="DrivingHistoryDateOfLastClaimLabel" runat="server"></asp:Label>
            <br />
        </fieldset>   
        <p><asp:LinkButton ID="LogoutLinkButton" runat="server" class="btn btn-primary btn-lg" OnClick="LogoutLinkButton_Click">Access another policy &raquo;</asp:LinkButton></p>   
    </div>
</asp:Content>
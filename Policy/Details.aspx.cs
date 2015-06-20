using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Policy_Details : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Policy policy = (Policy)Session["ExistingPolicy"];
        if (policy == null)
        {
            Response.Redirect("~/Policy/Login");
        }
        SetLabelText(policy);
    }

    public void SetLabelText(Policy policy)
    {
        PolicyDetailsLabel.Text = "Details for Policy Number " + policy.policyNumber;
        PolicyQuotationLabel.Text = "£" + policy.quotation.ToString();
        PolicyDateOfIssueLabel.Text = policy.dateOfIssue.ToString("d");

        VehicleMakeLabel.Text = policy.vehicle.make;
        VehicleModelLabel.Text = policy.vehicle.model;
        VehicleYearOfManufactorLabel.Text = policy.vehicle.yearOfManufactor.ToString();
        VehicleEngineCapacityLabel.Text = policy.vehicle.engineCapacity.ToString();

        DriverForenameLabel.Text = policy.driver.forename;
        DriverSurnameLabel.Text = policy.driver.surname;
        DriverDateOfBirthLabel.Text = policy.driver.dateOfBirth.ToString("d");
        DriverDrivingLicenseNumberLabel.Text = policy.driver.drivingLicenseNumber;
        DriverEmailAddressLabel.Text = policy.driver.emailAddress;

        AddressLine1Label.Text = policy.driver.line1;
        string line2 = policy.driver.line2;
        AddressLine2Label.Text = PotentialEmptyStringRepresentation(line2);
        string line3 = policy.driver.line3;
        AddressLine3Label.Text = PotentialEmptyStringRepresentation(line3);
        AddressCityLabel.Text = policy.driver.city;
        AddressCountyLabel.Text = policy.driver.county;
        AddressPostcodeLabel.Text = policy.driver.postcode;

        DrivingHistoryDateOfDrivingTestPassLabel.Text = policy.driver.dateOfDrivingTestPass.ToString("d");
        string dateOfDrivingTestPass = policy.driver.dateOfLastClaim.ToString();
        DrivingHistoryDateOfLastClaimLabel.Text = PotentialNullDateRepresentation(dateOfDrivingTestPass);
    }

    public string PotentialEmptyStringRepresentation(string text)
    {
        if (text.Length < 1)
        {
            return "-";
        }
        return text;
    }

    public string PotentialNullDateRepresentation(string text)
    {
        if (text.Length < 1)
        {
            return "-";
        }
        return text.Substring(0, 10);
    }
    protected void LogoutLinkButton_Click(object sender, EventArgs e)
    {
        Session["ExistingPolicy"] = null;
        Response.Redirect("Login");
    }
}
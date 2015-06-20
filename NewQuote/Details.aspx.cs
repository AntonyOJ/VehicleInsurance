using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;

public partial class NewQuote_Details : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DataTable makes = new DataTable();
            DbConnection dbConn = new DbConnection();
            using (SqlConnection connection1 = dbConn.OpenConnection())
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT [id], [make] FROM [VehicleMake]", connection1);
                adapter.Fill(makes);
                VehicleMakeDropDownList.DataSource = makes;
                VehicleMakeDropDownList.DataTextField = "make";
                VehicleMakeDropDownList.DataValueField = "id";
                VehicleMakeDropDownList.DataBind();
            }
            VehicleMakeDropDownList.Items.Insert(0, new ListItem("Select Make", "0"));
        }
    }

    protected void VehicleMakeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        int makeId = Convert.ToInt32(VehicleMakeDropDownList.SelectedValue);
        DataTable models = new DataTable();
        DbConnection dbConn = new DbConnection();
        using (SqlConnection connection2 = dbConn.OpenConnection())
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT [id], [model] FROM [VehicleModel] WHERE [makeId] = " + makeId, connection2);
            adapter.Fill(models);
            VehicleModelDropDownList.DataSource = models;
            VehicleModelDropDownList.DataTextField = "model";
            VehicleModelDropDownList.DataValueField = "id";
            VehicleModelDropDownList.DataBind();
        }
        if (!(VehicleMakeDropDownList.SelectedValue.Equals("0")))
        {
            VehicleModelDropDownList.Items.Insert(0, new ListItem("Select Model", "0"));
            VehicleModelDropDownList.Enabled = true;
        }
        else
        {
            VehicleModelDropDownList.Enabled = false;
        }
        VehicleYearOfManufactorDropDownList.Items.Clear();
        VehicleYearOfManufactorDropDownList.Enabled = false;
        VehicleEngineCapacityDropDownList.Items.Clear();
        VehicleEngineCapacityDropDownList.Enabled = false;
    }

    protected void VehicleModelDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        int modelId = Convert.ToInt32(VehicleModelDropDownList.SelectedValue);
        DataTable years = new DataTable();
        DbConnection dbConn = new DbConnection();
        using (SqlConnection connection3 = dbConn.OpenConnection())
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT [id], [yearOfManufactor] FROM [VehicleYear] WHERE [modelId] = " + modelId, connection3);
            adapter.Fill(years);
            VehicleYearOfManufactorDropDownList.DataSource = years;
            VehicleYearOfManufactorDropDownList.DataTextField = "yearOfManufactor";
            VehicleYearOfManufactorDropDownList.DataValueField = "id";
            VehicleYearOfManufactorDropDownList.DataBind();
        }
        if (!(VehicleModelDropDownList.SelectedValue.Equals("0")))
        {
            VehicleYearOfManufactorDropDownList.Items.Insert(0, new ListItem("Select Year", "0"));
            VehicleYearOfManufactorDropDownList.Enabled = true;
        }
        else
        {
            VehicleYearOfManufactorDropDownList.Enabled = false;
        }
        VehicleEngineCapacityDropDownList.Items.Clear();
        VehicleEngineCapacityDropDownList.Enabled = false;
    }

    protected void VehicleYearOfManufactorDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        int yearId = Convert.ToInt32(VehicleYearOfManufactorDropDownList.SelectedValue);
        DataTable capacities = new DataTable();
        DbConnection dbConn = new DbConnection();
        using (SqlConnection connection4 = dbConn.OpenConnection())
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT [id], [engineCapacity] FROM [VehicleDetails] WHERE [vehicleYearId] = " + yearId, connection4);
            adapter.Fill(capacities);
            VehicleEngineCapacityDropDownList.DataSource = capacities;
            VehicleEngineCapacityDropDownList.DataTextField = "engineCapacity";
            VehicleEngineCapacityDropDownList.DataValueField = "id";
            VehicleEngineCapacityDropDownList.DataBind();
        }
        if (!(VehicleYearOfManufactorDropDownList.SelectedValue.Equals("0")))
        {
            VehicleEngineCapacityDropDownList.Items.Insert(0, new ListItem("Select Capacity", "0"));
            VehicleEngineCapacityDropDownList.Enabled = true;
        }
        else
        {
            VehicleEngineCapacityDropDownList.Enabled = false;
        }        
    }

    protected void VehicleEngineCapacityDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void GetQuote_Click(object sender, EventArgs e)
    {
        if (Page.IsValid && ValidForm())
        {
            int vehicleId = Int32.Parse(VehicleEngineCapacityDropDownList.SelectedValue);
            Policy policy = new Policy(vehicleId);
            GetDriverDetails(policy);
            policy.CalculateQuotation();
            Session["NewPolicy"] = policy;
            Response.Redirect("Quote");
        }
    }

    public void GetDriverDetails(Policy policy)
    {
        policy.driver.forename = DriverForenameTextBox.Text.Trim();
        policy.driver.surname = DriverSurnameTextBox.Text.Trim();
        policy.driver.dateOfBirth = DateTime.Parse(DriverDateOfBirthTextBox.Text);
        policy.driver.drivingLicenseNumber = DriverDrivingLicenseNumberTextBox.Text.Trim().ToUpper();
        policy.driver.emailAddress = DriverEmailAddressTextBox.Text.Trim();

        policy.driver.line1 = AddressLine1TextBox.Text.Trim();
        policy.driver.line2 = AddressLine2TextBox.Text.Trim();
        policy.driver.line3 = AddressLine3TextBox.Text.Trim();
        policy.driver.city = AddressCityTextBox.Text.Trim();
        policy.driver.county = AddressCountyTextBox.Text.Trim();
        policy.driver.postcode = AddressPostcodeTextBox.Text.Trim().ToUpper();

        policy.driver.dateOfDrivingTestPass = DateTime.Parse(DriverDateOfDrivingTestPassTextBox.Text);
        if (DriverDateOfLastClaimTextBox.Text.Trim().Length < 1)
        {
            policy.driver.dateOfLastClaim = null;
        }
        else
        {
            policy.driver.dateOfLastClaim = DateTime.Parse(DriverDateOfLastClaimTextBox.Text);
        }
        
    }

    public bool ValidForm()
    {
        if (!ValidVehicle() | !ValidDriver())
            return false;
        return true;
    }

    public bool ValidVehicle()
    {
        if (!ValidVehicleMake() || !ValidVehicleModel() || !ValidVehicleYearOfManufactor() || !ValidVehicleEngineCapacity())
            return false;
        return true;
    }

    public bool ValidVehicleMake()
    {
        if (VehicleMakeDropDownList.SelectedValue.Equals("0"))
        {
            VehicleErrorLabel.Text = "You must select a Vehicle Make and its subsequent details";
            return false;
        }
        else
        {
            VehicleErrorLabel.Text = "";
            return true;
        }       
    }

    public bool ValidVehicleModel()
    {
        if (VehicleModelDropDownList.SelectedValue.Equals("0"))
        {
            VehicleErrorLabel.Text = "You must select a Vehicle Model and its subsequent details";
            return false;
        }
        else
        {
            return true;
        } 
    }

    public bool ValidVehicleYearOfManufactor()
    {
        if (VehicleYearOfManufactorDropDownList.SelectedValue.Equals("0"))
        {
            VehicleErrorLabel.Text = "You must select a Year Of Manufactor and its subsequent details";
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool ValidVehicleEngineCapacity()
    {
        if (VehicleEngineCapacityDropDownList.SelectedValue.Equals("0"))
        {
            VehicleErrorLabel.Text = "You must select an Engine Capacity";
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool ValidDriver()
    {
        if (!ValidDriverDetails() | !ValidDriverAddress() | !ValidDriverDrivingHistory())
            return false;
        return true;
    }

    public bool ValidDriverDetails()
    {
        if (!ValidDriverForename() | !ValidDriverSurname() | !ValidDriverDateOfBirth() | !ValidDriverDrivingLicenseNumber()
            | !ValidDriverEmailAddress())
            return false;
        return true;
    }

    public bool ValidDriverForename()
    {
        if (DriverForenameTextBox.Text.Trim().Length < 1)
        {
            DriverForenameErrorLabel.Text = "You must enter your Forename";
            return false;
        }
        else
        {
            DriverForenameErrorLabel.Text = "*";
            return true;
        } 
    }

    public bool ValidDriverSurname()
    {
        if (DriverSurnameTextBox.Text.Trim().Length < 1)
        {
            DriverSurnameErrorLabel.Text = "You must enter your Surname";
            return false;
        }
        else
        {
            DriverSurnameErrorLabel.Text = "*";
            return true;
        } 
    }

    public bool ValidDriverDateOfBirth()
    {
        DateTime input;
        int youngestAgeToTakeTest = 17;
        int oldestAge = 150;
        if (DriverDateOfBirthTextBox.Text.Trim().Length < 1)
        {
            DriverDateOfBirthErrorLabel.Text = "You must enter your Date of Birth";
            return false;
        }
        else if (!DateTime.TryParse(DriverDateOfBirthTextBox.Text, out input))
        {
            DriverDateOfBirthErrorLabel.Text = "You must enter a valid date format for your Date of Birth";
            return false;
        }
        else if (input > DateTime.Today.AddYears(youngestAgeToTakeTest * -1))
        {
            DriverDateOfBirthErrorLabel.Text = "Date of Birth is out of range (must be at least 17 to drive)";
            return false;
        }
        else if (input < DateTime.Today.AddYears(oldestAge * -1))
        {
            DriverDateOfBirthErrorLabel.Text = "Date of Birth is out of range (too far back in time)";
            return false;
        }
        else
        {
            DriverDateOfBirthErrorLabel.Text = "*";
            return true;
        }
    }

    public bool ValidDriverDrivingLicenseNumber()
    {
        if (DriverDrivingLicenseNumberTextBox.Text.Trim().Length < 1)
        {
            DriverDrivingLicenseNumberErrorLabel.Text = "You must enter your Driving License Number";
            return false;
        }
        Regex pattern = new Regex(@"[a-zA-Z][a-zA-Z9]{4}\d{6}[a-zA-Z]([a-zA-Z]|9)\d[a-zA-Z]{2}(\s[0-9]{2})?");
        if (!pattern.IsMatch(DriverDrivingLicenseNumberTextBox.Text.Trim()))
        {
            DriverDrivingLicenseNumberErrorLabel.Text = "You must enter a valid GB Driving License Number format such as 'MCFAK904203F99RC'";
            return false;
        }
        else
        {
            DriverDrivingLicenseNumberErrorLabel.Text = "*";
            return true;
        }
    }

    public bool ValidDriverEmailAddress()
    {
        if (DriverEmailAddressTextBox.Text.Trim().Length < 1)
        {
            DriverEmailAddressErrorLabel.Text = "You must enter your E-mail Address";
            return false;
        }
        Regex pattern = new Regex(@"[^\.][a-zA-Z0-9!#$%&'*\+\-/=?^_`{|}~\.]*[^\.]@[a-zA-Z0-9-\.]+(\.[a-zA-Z]{2,3}){1,2}");
        if (!pattern.IsMatch(DriverEmailAddressTextBox.Text.Trim()))
        {
            DriverEmailAddressErrorLabel.Text = "You must enter a valid E-mail Address format such as 'username@host.domain'";
            return false;
        }
        else
        {
            DriverEmailAddressErrorLabel.Text = "*";
            return true;
        }
    }

    public bool ValidDriverAddress()
    {
        if (!ValidAddressLine1() | !ValidAddressLine2() | !ValidAddressLine3() | !ValidAddressCity() | !ValidAddressCounty()
            | !ValidAddressPostcode())
            return false;
        return true;
    }

    public bool ValidAddressLine1()
    {
        if (AddressLine1TextBox.Text.Trim().Length < 1)
        {
            AddressLine1ErrorLabel.Text = "You must enter the first line of your Address";
            return false;
        }
        else
        {
            AddressLine1ErrorLabel.Text = "*";
            return true;
        }
    }

    public bool ValidAddressLine2()
    {
        return true;
    }

    public bool ValidAddressLine3()
    {
        return true;
    }

    public bool ValidAddressCity()
    {
        if (AddressCityTextBox.Text.Trim().Length < 1)
        {
            AddressCityErrorLabel.Text = "You must enter the City of your Address";
            return false;
        }
        else
        {
            AddressCityErrorLabel.Text = "*";
            return true;
        }
    }

    public bool ValidAddressCounty()
    {
        if (AddressCountyTextBox.Text.Trim().Length < 1)
        {
            AddressCountyErrorLabel.Text = "You must enter the County of your Address";
            return false;
        }
        else
        {
            AddressCountyErrorLabel.Text = "*";
            return true;
        }
    }

    public bool ValidAddressPostcode()
    {
        if (AddressPostcodeTextBox.Text.Trim().Length < 1)
        {
            AddressPostcodeErrorLabel.Text = "You must enter the Postcode of your Address";
            return false;
        }
        Regex pattern = new Regex(@"[a-zA-Z](\d(\d?|[a-zA-Z])|[a-zA-Z]\d(\d?|[a-zA-Z]))\s\d[a-zA-Z]{2}");
        if (!pattern.IsMatch(AddressPostcodeTextBox.Text.Trim()))
        {
            AddressPostcodeErrorLabel.Text = "You must enter a valid Postcode format such as 'AA9A 9AA' or 'AA9 9AA'";
            return false;
        }
        else
        {
            AddressPostcodeErrorLabel.Text = "*";
            return true;
        }
    }

    public bool ValidDriverDrivingHistory()
    {
        if (!ValidDrivingHistoryDateOfDrivingTestPass() | !ValidDrivingHistoryDateOfLastClaim())
            return false;
        return true;
    }

    public bool ValidDrivingHistoryDateOfDrivingTestPass()
    {
        DateTime input;
        if (DriverDateOfDrivingTestPassTextBox.Text.Trim().Length < 1)
        {
            DriverDateOfDrivingTestPassErrorLabel.Text = "You must enter your Date of Driving Test Pass";
            return false;
        }
        else if (!DateTime.TryParse(DriverDateOfDrivingTestPassTextBox.Text, out input))
        {
            DriverDateOfDrivingTestPassErrorLabel.Text = "You must enter a valid date format for your Date of Driving Test Pass";
            return false;
        }
        else if (!(input < DateTime.Today))
        {
            DriverDateOfDrivingTestPassErrorLabel.Text = "Date of Driving Test Pass is out of range (in the future)";
            return false;
        }
        else if (ValidDriverDateOfBirth())
        {
            int youngestAgeToTakeTest = 17;
            DateTime dateTestPassCompareToDOB = input.AddYears(youngestAgeToTakeTest * -1);
            DateTime DateOfBirth = DateTime.Parse(DriverDateOfBirthTextBox.Text);
            if (DateOfBirth > dateTestPassCompareToDOB)
            {
                DriverDateOfDrivingTestPassErrorLabel.Text = "Date of Driving Test Pass is out of range (can't take test before 17)";
                return false;
            }
            else
            {
                DriverDateOfDrivingTestPassErrorLabel.Text = "*";
                return true;
            }
        }
        else
        {
            DriverDateOfDrivingTestPassErrorLabel.Text = "*";
            return true;
        }
    }

    public bool ValidDrivingHistoryDateOfLastClaim()
    {
        DateTime input;
        if (DriverDateOfLastClaimTextBox.Text.Trim().Length < 1)
        {
            DriverDateOfLastClaimErrorLabel.Text = "Leave blank if not applicable";
            return true;
        }
        else if (!DateTime.TryParse(DriverDateOfLastClaimTextBox.Text, out input))
        {
            DriverDateOfLastClaimErrorLabel.Text = "You must enter a valid date format for your Date of Last Claim";
            return false;
        }
        else if (!(input < DateTime.Today))
        {
            DriverDateOfLastClaimErrorLabel.Text = "Date of Last Claim is out of range (in the future)";
            return false;
        }
        else if (ValidDrivingHistoryDateOfDrivingTestPass())
        {
            DateTime DateOfDrivingTestPass = DateTime.Parse(DriverDateOfDrivingTestPassTextBox.Text);
            if (DateOfDrivingTestPass > input)
            {
                DriverDateOfLastClaimErrorLabel.Text = "Date of Last Claim is out of range (can't claim before you pass test)";
                return false;
            }
            else
            {
                DriverDateOfLastClaimErrorLabel.Text = "Leave blank if not applicable";
                return true;
            }
        }
        else
        {
            DriverDateOfLastClaimErrorLabel.Text = "Leave blank if not applicable";
            return true;
        }
    }
}
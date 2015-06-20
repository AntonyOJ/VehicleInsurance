using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Policy
/// </summary>
public class Policy
{
    DbConnection dbConn = null;
    public bool agreeToQuotation = new bool();
    public Int32 policyNumber { get; set; }
    public Driver driver { get; set; }
    public Vehicle vehicle { get; set; }
    public String password = "";
    public Decimal quotation { get; set; }
    public DateTime dateOfIssue { get; set; }

	public Policy()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public Policy(int vehicleId)
    {
        dbConn = new DbConnection();
        SqlConnection connection = dbConn.OpenConnection();
        vehicle = new Vehicle(vehicleId, connection);
        driver = new Driver();
        connection = null;
        agreeToQuotation = false;
    }

    public bool AuthenticatedLogin(string policyNumber, string password)
    {
        bool match = false;
        dbConn = new DbConnection();
        SqlConnection connection = dbConn.OpenConnection();
        string sql = "SELECT * FROM Policy WHERE policyNumber = @policyNumber AND password = @password";
        SqlCommand command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@policyNumber", policyNumber);
        command.Parameters.AddWithValue("@password", password);
        SqlDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            reader.Read();
            ReadAllFromDatabase(connection, reader);
            match = true;   
        }        
        return match;
    }

    public void ReadAllFromDatabase(SqlConnection connection, SqlDataReader reader)
    {
        policyNumber = reader.GetInt32(0);
        Int32 driverId = reader.GetInt32(1);
        Int32 vehicleId = reader.GetInt32(2);
        password = reader.GetString(3);
        quotation = reader.GetDecimal(4);
        dateOfIssue = reader.GetDateTime(5);
        reader.Close();

        driver = new Driver(connection, driverId);
        vehicle = new Vehicle(connection, vehicleId);
    }

    public void WriteAllToDatabase()
    {
        dbConn = new DbConnection();
        SqlConnection connection = dbConn.OpenConnection();
        driver.WriteAllToDatabase(connection);
        WritePolicyToDatabase(connection);
    }

    public void WritePolicyToDatabase(SqlConnection connection)
    {
        string policyInsertion = "INSERT INTO Policy (driverId, vehicleId, password, quotation, dateOfIssue) OUTPUT INSERTED.policyNumber VALUES(@driverId, @vehicleId, @password, @quotation, @dateOfIssue)";
        SqlCommand command = new SqlCommand(policyInsertion, connection);
        command.Parameters.AddWithValue("@driverId", driver.id);
        command.Parameters.AddWithValue("@vehicleId", vehicle.id);
        command.Parameters.AddWithValue("@password", password);
        command.Parameters.AddWithValue("@quotation", quotation);
        command.Parameters.AddWithValue("@dateOfIssue", dateOfIssue);
        policyNumber = (Int32)command.ExecuteScalar();
    }

    public void CalculateQuotation()
    {
        decimal baseValue = 300;

        decimal currentValue = ApplyRiskModifier(baseValue);
        currentValue = ApplyAgeModifier(currentValue);
        currentValue = ApplyNoClaimsDiscountModifier(currentValue);

        decimal.Round(currentValue, 2, MidpointRounding.AwayFromZero);
        quotation = currentValue;
        dateOfIssue = DateTime.Today;
    }

    public decimal ApplyRiskModifier(decimal currentValue)
    {
        // Defined group variables relating to the specification
        int lowGroupUpperBoundary = 10;
        int mediumGroupUpperBoundary = 20;
        decimal lowGroupModifier = 0.5m;
        decimal mediumGroupModifier = 1.0m;
        decimal highGroupModifier = 2.0m;
        decimal result;
        if (vehicle.risk <= lowGroupUpperBoundary)
        {
            result = currentValue * lowGroupModifier;
        }
        else if (vehicle.risk <= mediumGroupUpperBoundary)
        {
            result = currentValue * mediumGroupModifier;
        }
        else
        {
            result = currentValue * highGroupModifier;
        }
        return result;
    }

    public decimal ApplyAgeModifier(decimal currentValue)
    {
        decimal result = currentValue;
        int age = NumberofYearsAgoFromNow(driver.dateOfBirth);
        // Linear price increase between the specified boundaries 
        decimal multipleIncreasePerYear = 1.8m;
        int decreaseLowerBoundary = 21;
        int decreaseUpperBoundary = 25;
        if (age <= decreaseLowerBoundary)
        {
            result = currentValue * (decreaseLowerBoundary - decreaseUpperBoundary) * multipleIncreasePerYear * (-1);
        }
        else if (age >= decreaseUpperBoundary)
        {
            result = currentValue;
        }
        else
        {
            result = currentValue * (age - (decreaseUpperBoundary)) * multipleIncreasePerYear * (-1);
        }
        return result;
    }
        
    public decimal ApplyNoClaimsDiscountModifier(decimal currentValue)
    {
        int numberOfYearsWithNoClaim;
        if (driver.dateOfLastClaim == null)
        {
            numberOfYearsWithNoClaim = NumberofYearsAgoFromNow(driver.dateOfDrivingTestPass);
        }
        else
        {
            // Get the number of years difference from when they passed their test if they've never claimed
            numberOfYearsWithNoClaim = NumberofYearsAgoFromNow(driver.dateOfLastClaim.Value);
        }
        int discountPerYearPercentage = 5;
        int maxDiscountPercentage = 50;
        decimal noClaimsDiscountPercentage = discountPerYearPercentage * numberOfYearsWithNoClaim;
        if (noClaimsDiscountPercentage > maxDiscountPercentage)
            noClaimsDiscountPercentage = maxDiscountPercentage;
        decimal result = currentValue * (1 - (noClaimsDiscountPercentage / 100));
        return result;
    }

    public int NumberofYearsAgoFromNow(DateTime dateToCompare)
    {
        int years = DateTime.Now.Year - dateToCompare.Year;
        if (DateTime.Now.Month < dateToCompare.Month
            || (DateTime.Now.Month == dateToCompare.Month && DateTime.Now.Day < dateToCompare.Day))
        {
            years--;
        }
        return years;
    }
}
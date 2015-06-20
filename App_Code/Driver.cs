using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Driver
/// </summary>
public class Driver
{
    SqlConnection connection = null;
    public Int32 id { get; set; }
    public String forename { get; set; }
    public String surname { get; set; }
    public DateTime dateOfBirth { get; set; }
    public String drivingLicenseNumber { get; set; }
    public String emailAddress { get; set; }

    public String line1 { get; set; }
    public String line2 { get; set; }
    public String line3 { get; set; }
    public String city { get; set; }
    public String county { get; set; }
    public String postcode { get; set; }

    public DateTime dateOfDrivingTestPass { get; set; }
    public DateTime? dateOfLastClaim { get; set; }

	public Driver(SqlConnection connection, Int32 id)
	{
        this.connection = connection;
        this.id = id;
        ReadAllFromDatabase();
        this.connection = null;
	}

    public Driver()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public void ReadAllFromDatabase()
    {
        ReadDriverFromDatabase();
        ReadAddressFromDatabase();
        ReadDrivingHistoryFromDatabase();
    }

    public void ReadDriverFromDatabase()
    {
        string driverQuery = "SELECT * FROM Driver WHERE id = @id";
        SqlDataReader driverReader = PerformSelectQuery(driverQuery);
        forename = driverReader.GetString(1);
        surname = driverReader.GetString(2);
        dateOfBirth = driverReader.GetDateTime(3);
        drivingLicenseNumber = driverReader.GetString(4);
        emailAddress = driverReader.GetString(5);
        driverReader.Close();
    }

    public void ReadAddressFromDatabase()
    {
        string driverAddressQuery = "SELECT * FROM DriverAddress WHERE driverId = @id";
        SqlDataReader driverAddressReader = PerformSelectQuery(driverAddressQuery);
        line1 = driverAddressReader.GetString(1);
        line2 = GetStringSafely(driverAddressReader, 2);
        line3 = GetStringSafely(driverAddressReader, 3);
        city = driverAddressReader.GetString(4);
        county = driverAddressReader.GetString(5);
        postcode = driverAddressReader.GetString(6);
        driverAddressReader.Close();
    }

    public void ReadDrivingHistoryFromDatabase()
    {
        string driverDrivingHistoryQuery = "SELECT * FROM DriverDrivingHistory WHERE driverId = @id";
        SqlDataReader driverDrivingHistoryReader = PerformSelectQuery(driverDrivingHistoryQuery);
        dateOfDrivingTestPass = driverDrivingHistoryReader.GetDateTime(1);
        dateOfLastClaim = GetDateTimeSafely(driverDrivingHistoryReader, 2);
        driverDrivingHistoryReader.Close();
    }

    public SqlDataReader PerformSelectQuery(string query)
    {
        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", id);
        SqlDataReader reader = command.ExecuteReader();
        reader.Read();
        return reader;
    }

    public static string GetStringSafely(SqlDataReader reader, int columnIndex)
    {
        if (!reader.IsDBNull(columnIndex))
            return reader.GetString(columnIndex);
        else
            return string.Empty;
    }

    public static DateTime? GetDateTimeSafely(SqlDataReader reader, int columnIndex)
    {
        if (!reader.IsDBNull(columnIndex))
            return reader.GetDateTime(columnIndex);
        else
            return null;
    }

    public void WriteAllToDatabase(SqlConnection connection)
    {
        this.connection = connection;
        WriteDriverToDatabase();
        WriteAddressToDatabase();
        WriteDrivingHistoryToDatabase();
    }

    public void WriteDriverToDatabase()
    {
        string driverInsertion = "INSERT INTO Driver (forename, surname, dateOfBirth, drivingLicenseNumber, emailAddress) OUTPUT INSERTED.id VALUES(@forename, @surname, @dateOfBirth, @drivingLicenseNumber, @emailAddress)";
        SqlCommand command = new SqlCommand(driverInsertion, connection);
        command.Parameters.AddWithValue("@forename", forename);
        command.Parameters.AddWithValue("@surname", surname);
        command.Parameters.AddWithValue("@dateOfBirth", dateOfBirth);
        command.Parameters.AddWithValue("@drivingLicenseNumber", drivingLicenseNumber);
        command.Parameters.AddWithValue("@emailAddress", emailAddress);
        id = (Int32)command.ExecuteScalar();
    }

    public void WriteAddressToDatabase()
    {
        string driverAddressInsertion = "INSERT INTO DriverAddress (driverId, line1, line2, line3, city, county, postcode) VALUES(@driverId, @line1, @line2, @line3, @city, @county, @postcode)";
        SqlCommand command = new SqlCommand(driverAddressInsertion, connection);
        command.Parameters.AddWithValue("@driverId", id);
        command.Parameters.AddWithValue("@line1", line1);
        if (line2.Length < 1)
        {
            command.Parameters.AddWithValue("@line2", DBNull.Value);
        }
        else
        {
            command.Parameters.AddWithValue("@line2", line2);
        }
        if (line3.Length < 1)
        {
            command.Parameters.AddWithValue("@line3", DBNull.Value);
        }
        else
        {
            command.Parameters.AddWithValue("@line3", line3);
        }
        command.Parameters.AddWithValue("@city", city);
        command.Parameters.AddWithValue("@county", county);
        command.Parameters.AddWithValue("@postcode", postcode);
        command.ExecuteNonQuery();
    }

    public void WriteDrivingHistoryToDatabase()
    {
        string driverDrivingHistoryInsertion = "INSERT INTO DriverDrivingHistory (driverId, dateOfDrivingTestPass, dateOfLastClaim) Values(@driverId, @dateOfDrivingTestPass, @dateOfLastClaim)";
        SqlCommand command = new SqlCommand(driverDrivingHistoryInsertion, connection);
        command.Parameters.AddWithValue("@driverId", id);
        command.Parameters.AddWithValue("@dateOfDrivingTestPass", dateOfDrivingTestPass);
        if (dateOfLastClaim == null)
        {
            command.Parameters.AddWithValue("@dateOfLastClaim", DBNull.Value);
        }
        else
        {
            command.Parameters.AddWithValue("@dateOfLastClaim", dateOfLastClaim);
        }
        command.ExecuteNonQuery();
    }
}
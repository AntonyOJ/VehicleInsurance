using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Vehicle
/// </summary>
public class Vehicle
{
    SqlConnection connection = null;
    public Int32 id { get; set; }
    public String make { get; set; }
    public String model { get; set; }
    public Int32 yearOfManufactor { get; set; }
    public Decimal engineCapacity { get; set; }
    public Int32 risk { get; set; }

    public Vehicle(SqlConnection connection, Int32 id)
	{
        this.connection = connection;
        this.id = id;
        ReadAllFromDatabase();
        connection = null;
	}

    public Vehicle(Int32 id, SqlConnection connection)
    {
        this.id = id;
        this.connection = connection;
        ReadAllFromDatabase();
        this.connection = null;
    }

    public void ReadAllFromDatabase()
    {
        string vehicleDetailsQuery = "SELECT * FROM VehicleDetails WHERE id = @id";
        SqlDataReader vehicleDetailsReader = PerformQuery(vehicleDetailsQuery, id);
        Int32 vehicleYearId = vehicleDetailsReader.GetInt32(1);
        engineCapacity = vehicleDetailsReader.GetDecimal(2);
        risk = vehicleDetailsReader.GetInt32(3);
        vehicleDetailsReader.Close();

        string vehicleYearQuery = "SELECT * FROM VehicleYear WHERE id = @id";
        SqlDataReader vehicleYearReader = PerformQuery(vehicleYearQuery, vehicleYearId);
        Int32 modelId = vehicleYearReader.GetInt32(1);
        yearOfManufactor = vehicleYearReader.GetInt32(2);
        vehicleYearReader.Close();

        string vehicleModelQuery = "SELECT * FROM VehicleModel WHERE id = @id";
        SqlDataReader vehicleModelReader = PerformQuery(vehicleModelQuery, modelId);
        Int32 makeId = vehicleModelReader.GetInt32(1);
        model = vehicleModelReader.GetString(2);
        vehicleModelReader.Close();

        string vehicleMakeQuery = "SELECT * FROM VehicleMake WHERE id = @id";
        SqlDataReader vehicleMakeReader = PerformQuery(vehicleMakeQuery, makeId);
        make = vehicleMakeReader.GetString(1);
        vehicleModelReader.Close();
    }

    public SqlDataReader PerformQuery(string query, Int32 id)
    {
        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", id);
        SqlDataReader reader = command.ExecuteReader();
        reader.Read();
        return reader;
    }
}
using Microsoft.Data.SqlClient;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MVCSmallFarm.ViewModels;

public class DBConnector
{
    public static SqlConnection connect()
    {

        IConfigurationRoot config = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json")
        .Build();

        string connection = config.GetConnectionString("SmallFarm");
        SqlConnection con = new SqlConnection(connection);
        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
        else
        {
            con.Open();
        }
        return con;
    }

    public bool DMLOpperation(string query)
    {
        SqlCommand cmd = new SqlCommand(query, DBConnector.connect());
        int x = cmd.ExecuteNonQuery();
        if (x == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public DataTable SelactAll(string query)
    {
        SqlDataAdapter da = new SqlDataAdapter(query, DBConnector.connect());
        DataTable dt = new DataTable();
        da.Fill(dt);
        return dt;
    }

    public DataTable ProductList(int id) {
        DataTable table = new DataTable();
        using (var con = DBConnector.connect())
        using (var cmd = new SqlCommand("usp_ProductList", con))
        using (var da = new SqlDataAdapter(cmd))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", id);
            da.Fill(table);
        }
        return table;
    }

}

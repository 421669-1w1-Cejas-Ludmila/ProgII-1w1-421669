using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Libreria.Dominio;
using System.Data.Common;

public class DataHelper
{
    private static DataHelper _instance;
    private string _connection = "Data Source=DESKTOP-IRDSJ6T\\SQLEXPRESS;Initial Catalog=ProyectoLibreria;Integrated Security=True;TrustServerCertificate=True";

    private DataHelper() { }

    public static DataHelper GetInstance()
    {
        if (_instance == null)
            _instance = new DataHelper();

        return _instance;
    }

    public DataTable ExecuteSPQuery(string sp, List<ParametroSP> param = null)
    {
        DataTable dt = new DataTable();
        try
        {          
            using (var connection = new SqlConnection(_connection))
            {
                connection.Open();
                var cmd = new SqlCommand(sp, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = sp;

                if (param != null)
                {
                    foreach (ParametroSP p in param)
                    {
                        cmd.Parameters.AddWithValue(p.Name, p.Valor);
                    }
                }

                dt.Load(cmd.ExecuteReader());
            }
        }
        catch (SqlException ex)
        {
            dt = null;
        }
        return dt;
    }
}
using Microsoft.Data.SqlClient;
using Dapper;
public class BD
{
    private static string _connectionString = @"Server=localhost; Database=NombreBase;Integrated Security=True;TrustServerCertificate=True;";
    public static SqlConnection Conexion()
    {
        return new SqlConnection(_connectionString);
    }
    
}
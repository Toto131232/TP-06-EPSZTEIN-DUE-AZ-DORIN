using Microsoft.Data.SqlClient;
using Dapper;
using System.Collections.Generic;

public class InfoUsuario
{
    public string NombreUsuario {get; set;}
    public string Contraseña {get;set;}
    public List<string> Tareas{get;set;}

public static InfoUsuario LevantarUsuario(string nombre, string contraseña)
{
    using(SqlConnection connection =BD.Conexion())
    {
        string query="SELECT USUARIO, TAREAS FROM INFOUSUARIO WHERE Usuario=@NombreUsuario AND Contraseña=@Contraseña";
    }
}
 public static InfoUsuario BuscarXUsuario(string nombreusuario)
    {
        using (SqlConnection connection = BD.Conexion())
        {
            string query = "SELECT USUARIO, TAREAS FROM Integrante WHERE NombreDeUsuario = @NombreUsuario";
            return connection.QueryFirstOrDefault<InfoUsuario>(query, new { nombreusuario });
        }
    }
}
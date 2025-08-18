using Microsoft.Data.SqlClient;
using Dapper;
using System.Collections.Generic;

public class Usuario
{
    public string NombreUsuario { get; set; }
    public string Contraseña { get; set; }
    public int Id {get; set;}

    public Usuario(int Id, string NombreUsuario, string Contraseña)
    {
        this.Id=Id;
        this.NombreUsuario = NombreUsuario;
        this.Contraseña = Contraseña;
        
    }
    public static int AgregarUsuario(string nombreUsuario, string contraseña)
    {
        string query = "Insert Into Usuario(NombreUsuario, Contraseña) Values (@nombreusuario, @contraseña)";
        int registrosafectados = 0;
        using (SqlConnection connection = BD.Conexion())
        {
          registrosafectados = connection.Execute(query, new { nombreUsuario = nombreUsuario, contraseña = contraseña});
        }
        return registrosafectados;
    }
}
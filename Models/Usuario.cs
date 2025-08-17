using Microsoft.Data.SqlClient;
using Dapper;
using System.Collections.Generic;

public class Usuario
{
    public string NombreUsuario { get; set; }
    public string Contraseña { get; set; }
    public List<Tarea> Tareas { get; set; }

    public Usuario(int Id, string NombreUsuario, string Contraseña, List<Tarea> Tareas)
    {
        this.NombreUsuario = NombreUsuario;
        this.Contraseña = Contraseña;
        this.Tareas = Tareas;
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
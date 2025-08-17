using Microsoft.Data.SqlClient;
using Dapper;
public class BD
{
    private static string _connectionString = @"Server=localhost; Database=NombreBase;Integrated Security=True;TrustServerCertificate=True;";
    public static List<Usuario> Usuarios = new List<Usuario>();
    public static List<Tarea> Tareas = new List<Tarea>();
    public static List<Usuario> LevantarUsuarios(string nombreusuario, int contraseña)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Usuarios WHERE NombreUsuario=@nombreusuario AND Contraseña=@contraseña";
            Usuarios = connection.Query<Usuario>(query).ToList();
        }
        return Usuarios;
    }
    public static List<Tarea> LevantarTareas(Usuario usuario)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Tareas INNER JOIN UsuarioXTareas on Id = IdTarea WHERE IdUsuario = @Id";
            Tareas = connection.Query<Tarea>(query, new { ID = usuario.Id }).ToList();
        }
        return Tareas;
    }
    public static Usuario VerificarUsuario(string Nombre)
    {
        Usuario SeEncontro = null;
        foreach (Usuario usuario in Usuarios)
        {
            if (Nombre == usuario.NombreUsuario)
            {
                SeEncontro = usuario;
            }
        }
        return SeEncontro;
    }
    public static SqlConnection Conexion()
    {
        return new SqlConnection(_connectionString);
    }
    
}
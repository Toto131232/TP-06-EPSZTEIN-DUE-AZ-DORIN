using Microsoft.Data.SqlClient;
using Dapper;
public class BD
{
    private static string _connectionString = @"Server=localhost;Database=TP06;Integrated Security=True;TrustServerCertificate=True;";
    
    public static List<Tarea> LevantarTarea(){
        List<Tarea> tarea = new List<Tarea>();
        using(SqlConnection connection = new SqlConnection(_connectionString)){
            string query = "SELECT * FROM Tarea";
            tareas = connection.Query<Tarea>(query).ToList();
        }
        return tareas;
    }

    public static Tarea LevantarTarea2(int id)
    {
        Tarea tarea = null;
        using(SqlConnection connection = new SqlConnection(_connectionString)){
            string query = "SELECT * FROM Tarea WHERE Id = @id";
            tarea = connection.QueryFirstOrDefault<Tarea>(query, new { Id = id });
        }
        return tarea;
    }

    public static void AgregarTarea(Tarea tarea)
    {
        string query = "INSERT INTO Tarea (Titulo, Finalizada) VALUES (@Titulo, @false)";
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Execute(query, new { Titulo = tarea.Titulo, Finalizada=false});
        }
    }

    public static void EditarTarea(Tarea tarea)
    {
        string query = "UPDATE Tarea SET Titulo = @Titulo, Finalizada=@false";
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Execute(query, new { Titulo = tarea.Titulo, Finalizada=false });
        }
    }

    public static void EliminarTarea(int id)
    {
        string query = "EXEC EliminarTarea @Id;";
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Execute(query, new { Id = id });
        }
    }

    public static Usuario LevantarUsuario(string nombre, string contraseña)
    {
        Usuario usuario = null;
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Usuario WHERE Usuario = @nombre AND Contraseña = @contraseña";
            usuario = connection.QueryFirstOrDefault<Usuario>(query, new { Nombre = nombre, Contraseña = contraseña });
        }
        return usuario;
    }

    public static Usuario LevantarUsuarioXNombre(string nombre)
    {
        Usuario usuario = null;
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Usuario WHERE Nombre = @nombre";
            usuario = connection.QueryFirstOrDefault<Usuario>(query, new { Nombre = nombre });
        }
        return usuario;
    }

    public static void AgregarUsuario(Usuario usuario)
    {
        string query = "INSERT INTO Usuario (Nombre, Contraseña) VALUES (@nombre, @Contraseña)";
         using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Execute(query, new { Nombre = usuario.nombre, Contraseña = usuario.Contraseña});
        }
    }

    public static void CompartirTarea(int IdTarea, int IdUsuario)
    {
        string query = "INSERT INTO UsuarioxTarea (IdTarea, IdUsuario) VALUES (@IdTarea, @IdUsuario)";
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Execute(query, new { IdTarea = IdTarea, IdUsuario = IdUsuario });
        }
    }

    public static List<Tarea> LevantarTareasXUsuario(int idUsuario)
    {
        List<Tarea> tareas = new List<Tarea>();
        using(SqlConnection connection = new SqlConnection(_connectionString)){
            string query = @"SELECT .* FROM Tarea 
                           INNER JOIN UsuarioxTarea ON t.Id = uxt.IdTarea 
                           WHERE uxt.IdUsuario = @IdUsuario";
            tareas = connection.Query<Tarea>(query, new { IdUsuario = idUsuario }).ToList();
        }
        return tareas;
    }
}
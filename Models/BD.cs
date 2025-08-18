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
    public static List<Tarea> LevantarTareas(int Id)
    {
        using (SqlConnection connection = Conexion())
        {
            string query = "SELECT * FROM Tareas WHERRE Id= @Id";
            Tareas = connection.Query<Tarea>(query, new {Id}).ToList();
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
    public static List<Tarea> VerificarTareasActivas()
    {
        using (SqlConnection connection = Conexion())
        {
            var query = "SELECT * FROM Tarea WHERE  Finalizada=FALSE";
            Tareas = connection.Query<Tarea>(query).ToList();
            return Tareas;
        }
    }
    public static List<Tarea> VerificarTareasFinalizadas()
    {
        using (SqlConnection connection = Conexion())
        {
            var query = "SELECT * FROM Tareas WHERE Finalizada = true";
            Tareas = connection.Query<Tarea>(query).ToList();
            return Tareas;
        }
    }
    public static void FinalizarTareas(int Id)
    {
        using (SqlConnection connection = Conexion())
        {
            connection.Execute("UPDATE Tareas SET Finalizada = true WHERE Id = @Id", new { Id });
        }
    }
    public static void EliminarTareas(int ID)
    {
        using (SqlConnection connection = Conexion())
        {
            connection.Execute("DELETE FROM UsuarioXTarea WHERE IdTarea = @ID", new { ID });
            connection.Execute("DELETE FROM Tareas WHERE ID = @ID", new { ID });
        }
    }
    public static int AgregarTarea(string titulo)
    {
        int Id = connection.Query<int>("INSERT INTO Tareas Titulo VALUES @titulo", new { titulo });
        return Id;
    }
    public static void AgregarUsuarioXTarea(int IdUsuario, int IdTarea)
    {
        using (SqlConnection connection = Conexion())
        {
            int Id = connection.QueryFirstOrDefault<int>(
                "SELECT ISNULL(MAX(Id), 0) + 1 FROM UsuarioXTarea"
            );

            connection.Execute(
                "INSERT INTO UsuarioXTarea (Id, Idusuario, IdTarea) VALUES (@Id, @IdUsuario, @IdTarea)",
                new { id = Id, IdUsuario, IdTarea }
            );
        }
    }


    
}
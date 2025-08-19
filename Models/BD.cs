using Microsoft.Data.SqlClient;
using Dapper;
public class BD
{
    private static string _connectionString = @"Server=localhost; Database=NombreBase;Integrated Security=True;TrustServerCertificate=True;";
    public static List<Usuario> Usuarios = new List<Usuario>();
    public static Tarea Tareas = new Tarea();
    public static List<Usuario> LevantarUsuarios(string nombreusuario, int contraseña)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Usuarios WHERE NombreUsuario=@nombreusuario AND Contraseña=@contraseña";
            Usuarios = connection.Query<Usuario>(query).ToList();
        }
        return Usuarios;
    }
    public static Tarea LevantarTareas(int Id)
    {
        using (SqlConnection connection = Conexion())
        {
            string query = "SELECT * FROM Tareas WHERRE Id= @Id";
            Tareas = connection.QueryFirstOrDefault<Tarea>(query, new { Id });
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
    public static Tarea VerificarTareasActivas()
    {
        using (SqlConnection connection = Conexion())
        {
            var query = "SELECT * FROM Tarea WHERE  Finalizada=FALSE";
            Tareas = connection.QueryFirstOrDefault<Tarea>(query);
            return Tareas;
        }
    }
    public static Tarea VerificarTareasFinalizadas()
    {
        using (SqlConnection connection = Conexion())
        {
            var query = "SELECT * FROM Tareas WHERE Finalizada = true";
            Tareas = connection.QueryFirstOrDefault<Tarea>(query);
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


    /* ----------Ns si esta bn---------*/
    public static void AgregarUsuarioTarea2(string Usuario, int IdTarea)
    {
        using (SqlConnection connection = Conexion())
        {
            int IdUsuario = connection.QueryFirstOrDefault<int>("SELECT Id FROM Usuario WHERE Usuario = @Usuario", new { Usuario });

            if (IdUsuario <= 0)
            {
                throw new InvalidOperationException("No se encontró en la base de datos.");
            }

            int Id = connection.QueryFirstOrDefault<int>("SELECT ISNULL(MAX(Id), 0) + 1 FROM UsuarioXTarea");

            connection.Execute("INSERT INTO UsuarioXTarea (Id, IdUsuario, IdTarea) VALUES (@Id, @IdUsuario, @IdTarea)",
                new { id = Id, IdUsuario, IdTarea }
            );
        }
    }
    public static Tarea ObtenerTareasPorUsuario(string Usuario)
    {
        using (SqlConnection connection = Conexion())
        {
            string query = @"SELECT t.Id, t.Titulo, t.Finalizada FROM Tareas t
                INNER JOIN UsuarioXTarea uxt ON uxt.IdTarea = t.Id
                INNER JOIN Usuario u ON u.Id = uxt.IdUsuario
                WHERE u.Nombre = @Usuario
                  AND uxt.Id = (
                      SELECT MIN(uxt.Id) FROM UsuarioXTarea WHERE uxt.IdTarea = t.Id)";
            Tareas = connection.QueryFirstOrDefault<Tarea>(query, new { Usuario });
            return Tareas;
        }
    }
    public static bool CompartirTarea(string UsuarioCompartido, int IdTarea)
    {
        using (SqlConnection connection = Conexion())
        {
            int IdUsuario = connection.QueryFirstOrDefault<int>(
                "SELECT Id FROM Usuario WHERE Usuario = @UsuarioCompartido",
                new { UsuarioCompartido }
            );

            if (IdUsuario <= 0)
            {
                throw new InvalidOperationException("El usuario de destino no existe.");
            }

            int ExisteElUsuario = connection.QueryFirstOrDefault<int>(
                "SELECT COUNT(1) FROM UsuarioXTarea WHERE IdUsuario = @IdUsuario AND IdTarea = @IdTarea",
                new { IdUsuario, IdTarea }
            );

            if (ExisteElUsuario > 0)
            {
                return false;
            }

            int Id = connection.QueryFirstOrDefault<int>(
                "SELECT ISNULL(MAX(ID), 0) + 1 FROM UsuarioXTarea"
            );

            connection.Execute(
                "INSERT INTO UsuarioXTarea (Id, IdUsuario, IdTarea) VALUES (@id, @IdUsuario, @IdTarea)",
                new { id = Id, idPerfil = IdUsuario, IdTarea }
            );

            return true;
        }
    }
    public static Tarea TareasCompartidas(string usuario)
    {
        using (SqlConnection connection = Conexion())
        {
            string query = @"
                SELECT t.Id, t.Titulo, t.Fecha, t.IDCategoria, t.Finalizada
                FROM Tareas
                INNER JOIN UsuarioXTarea ON uxt.IdTarea = t.Id
                INNER JOIN Usuario ON u.Id = uxt.Idusuario
                WHERE u.Nombre = @usuario AND t.Finalizada = 0
                  AND uxt.Id <> (
                      SELECT MIN(uxt.Id) FROM UsuarioXTarea WHERE uxt.IdTarea = t.Id
                  )";
            Tareas = connection.QueryFirstOrDefault<Tarea>(query, new { usuario });
            return Tareas;
        }
    }
    


    
}
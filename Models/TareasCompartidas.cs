using Microsoft.Data.SqlClient;
using Dapper;
using System.Collections.Generic;

public class TareasCompartidas
{
    public int Id { get; set; }
        public int IdTarea { get; set; }
        public Tarea Tarea { get; set; }
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }  
}
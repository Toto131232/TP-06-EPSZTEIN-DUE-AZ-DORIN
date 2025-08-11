using Microsoft.Data.SqlClient;
using Dapper;
using System.Collections.Generic;

public class Tarea
{
    public int Id { get; set; }
        public int IdCreaor { get; set; }
        public Usuario Creador { get; set; } = null!;
        public string Titulo { get; set; }
        public bool Finalizada { get; set; }
        public bool Eliminada { get; set; }
}
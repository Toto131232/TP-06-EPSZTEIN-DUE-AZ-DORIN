using Microsoft.Data.SqlClient;
using Dapper;
using System.Collections.Generic;

public class Tarea
{
        public int Id { get; set; }
        public int IdCreaor { get; set; }
        public string Titulo { get; set; }
        public bool Finalizada { get; set; }

    public Tarea(int Id, int IdCreaor, Usuario Creador, string Titulo, bool Finalizada, bool Eliminada)
    {
        this.Id=Id;
        this.IdCreaor = IdCreaor;
        this.Titulo=Titulo;
        this.Finalizada=Finalizada;
    }
}
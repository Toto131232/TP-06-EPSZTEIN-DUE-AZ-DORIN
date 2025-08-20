using Microsoft.Data.SqlClient;
using Dapper;
using System.Collections.Generic;

public class Tarea
{
        public int Id { get; set; }
        public string Titulo { get; set; }
        public bool Finalizada { get; set; }

        public Tarea(string titulo)
        {
                Titulo = titulo;
                Finalizada = false;
        }
}
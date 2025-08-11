using Microsoft.Data.SqlClient;
using Dapper;
using System.Collections.Generic;

public class Usuario
{
    public int Id{get; set;}
    public string NombreUsuario {get; set;}
    public string Contraseña {get;set;}
    public List<string> Tareas{get;set;}
}
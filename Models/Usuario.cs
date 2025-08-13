using Microsoft.Data.SqlClient;
using Dapper;
using System.Collections.Generic;

public class Usuario
{
    public int Id{get; set;}
    public string NombreUsuario {get; set;}
    public string Contraseña {get;set;}
    public List<Tarea> Tareas{get;set;}

    public Usuario(int Id, string NombreUsuario, string Contraseña, List<Tarea> Tareas)
    {
        this.Id=Id;
        this.NombreUsuario=NombreUsuario;
        this.Contraseña=Contraseña;
        this.Tareas=Tareas;
    }
}
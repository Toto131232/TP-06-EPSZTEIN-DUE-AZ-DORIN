using Microsoft.Data.SqlClient;
using Dapper;
using System.Collections.Generic;

public class Usuario
{
    public int Id{get;set;}
    public int IdTarea{get;set;}
    public int IdUsuario{get;set;}

    public Usuario(int Id, int IdTarea, int IdUsuario)
    {
        this.Id=Id;
        this.IdTarea=IdTarea;
        this.IdUsuario=IdUsuario;
    }

}
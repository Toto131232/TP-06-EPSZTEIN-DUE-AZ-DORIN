using Microsoft.Data.SqlClient;
using Dapper;
using System.Collections.Generic;

public class UsuarioXTarea
{
    public int Id{get;set;}
    public int IdTarea{get;set;}
    public int IdUsuario{get;set;}
}
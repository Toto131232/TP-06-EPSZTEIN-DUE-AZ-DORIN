using Microsoft.Data.SqlClient;
using Dapper;
using System.Collections.Generic;

public class Usuario
{
  public string NombreUsuario { get; set; }
  public string Contraseña { get; set; }
  public int Id { get; set; }

  public Usuario(string nombre, string contraseña)
  {
    NombreUsuario = nombre;
    Contraseña = contraseña;
  }
}
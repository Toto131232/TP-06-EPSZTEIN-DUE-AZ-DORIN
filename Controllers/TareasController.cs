using Microsoft.AspNetCore.Mvc;
using TP_06_EPSZTEIN_DUEÑAZ_DORIN.Models;
using System.Data.SqlClient;
using Dapper;


namespace TP06_ToDoList.Controllers
{
    public class TareasController : Controller
    {
       [HttpPost]
        public IActionResult AgregarTarea(string titulo)
        {
            BD.AgregarTarea(titulo);
            return RedirectToAction("AgregarTarea");
        }
        
    }
}

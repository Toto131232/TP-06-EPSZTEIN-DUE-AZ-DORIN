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
        public IActionResult VerificarTareaAEditar(int Id)
        {
            Tarea tarea = BD.LevantarTareas(Id);
            if (tarea == null)
            {
                return NotFound();
            }
            return View(tarea);
        }
        [HttpPost]
        public IActionResult EditarTarea(int id, string titulo)
        {
            BD.ActualizarTarea(id, tiulo);
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult FinalizarTarea(int Id)
        {
            BD.FinalizarTareas(Id);
            return RedirectToAction();
        }
        [HttpPost]
        public IActionResult EliminarTarea(int Id)
        {
            BD.EliminarTareas(Id);
            return RedirectToAction();
        }
        [HttpPost]
        public IActionResult CompartirTarea(int Id, string UsuarioCompartido)
        {
            return View();
       }
        }


    }

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
            string usuario = HttpContext.Session.GetString("integrante");
            if (string.IsNullOrEmpty(usuario))
            {
                return RedirectToAction("Index", "Home");
            }

            Tarea tarea = new Tarea(titulo);
            BD.AgregarTarea(tarea);

            return RedirectToAction("Index");
        }

        public IActionResult VerificarTareaAEditar(int Id)
        {
            string usuario = HttpContext.Session.GetString("integrante");
            if (string.IsNullOrEmpty(usuario))
            {
                return RedirectToAction("Index", "Home");
            }

            Tarea tarea = BD.LevantarTarea2(Id);
            if (tarea == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Tarea = tarea;
            return View();
        }
                public IActionResult VerificarTareaAAgregar()
        {
            string usuario = HttpContext.Session.GetString("integrante");
            if (string.IsNullOrEmpty(usuario))
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
                public IActionResult VerificarTareaACompartir(int id)
        {
            string usuario = HttpContext.Session.GetString("integrante");
            if (string.IsNullOrEmpty(usuario))
            {
                return RedirectToAction("Index", "Home");
            }

            Tarea tarea = BD.LevantarTarea2(id);
            if (tarea == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Tarea = tarea;
            return View();
        }

   public IActionResult EliminarTarea(int id)
        {
            string usuario = HttpContext.Session.GetString("integrante");
            if (string.IsNullOrEmpty(usuario))
            {
                return RedirectToAction("Index", "Home");
            }

            BD.EliminarTarea(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult EditarTarea(int id, string titulo)
        {
            string usuario = HttpContext.Session.GetString("integrante");
            if (string.IsNullOrEmpty(usuario))
            {
                return RedirectToAction("Index", "Home");
            }

            Tarea tarea = BD.LevantarTarea2(id);
            if (tarea != null)
            {
                tarea.Titulo = titulo;

                BD.EditarTarea(tarea);
            }

            return RedirectToAction("Index");
        }

     
        [HttpPost]
        public IActionResult CompartirTareas(int idTarea, string nombre)
        {
            string usuario = HttpContext.Session.GetString("integrante");
            if (string.IsNullOrEmpty(usuario))
            {
                return RedirectToAction("Index", "Home");
            }

            Usuario usuarioDestino = BD.LevantarUsuarioXNombre(nombre);
            if (usuarioDestino != null)
            {
                BD.CompartirTarea(idTarea, usuarioDestino.Id);
            }

            return RedirectToAction("Index");
        }
    }
}

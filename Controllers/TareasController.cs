using Microsoft.AspNetCore.Mvc;
using TP_06_EPSZTEIN_DUEÑAZ_DORIN.Models;
using System.Data.SqlClient;
using Dapper;


namespace TP06_ToDoList.Controllers
{
    public class TareasController : Controller
    {
        public IActionResult VerificarTareaAAgregar()
    {
        string usuario = HttpContext.Session.GetString("integrante");
        if (string.IsNullOrEmpty(usuario))
        {
            return RedirectToAction("Index", "Home");
        }

        return View();
    }

    [HttpPost]
    public IActionResult agregartarea(string titulo)
    {
        string usuario = HttpContext.Session.GetString("integrante");
        if (string.IsNullOrEmpty(usuario))
        {
            return RedirectToAction("Index", "Home");
        }

        Tarea tarea = new Tarea(titulo, false);
        BD.AgregarTarea(nuevaTarea);
        
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
    /*----HASTA ACA*/

    [HttpPost]
    public IActionResult EditarTarea(int id, string titulo, string descripcion, DateTime fechaDeEntrega, string prioridad)
    {
        string usuarioStr = HttpContext.Session.GetString("integrante");
        if (string.IsNullOrEmpty(usuarioStr))
        {
            return RedirectToAction("Index", "Home");
        }

        Tarea tarea = BD.LevantarTareaPorId(id);
        if (tarea != null)
        {
            tarea.Titulo = titulo;
            tarea.Descripcion = descripcion;
            tarea.FechaDeEntrega = fechaDeEntrega;
            tarea.Prioridad = prioridad;
            
            BD.ModificarTarea(tarea);
        }
        
        return RedirectToAction("Index");
    }

    public IActionResult Eliminar(int id)
    {
        string usuarioStr = HttpContext.Session.GetString("integrante");
        if (string.IsNullOrEmpty(usuarioStr))
        {
            return RedirectToAction("Index", "Home");
        }

        BD.EliminarTarea(id);
        return RedirectToAction("Index");
    }

    public IActionResult Compartir(int id)
    {
        string usuarioStr = HttpContext.Session.GetString("integrante");
        if (string.IsNullOrEmpty(usuarioStr))
        {
            return RedirectToAction("Index", "Home");
        }

        Tarea tarea = BD.LevantarTareaPorId(id);
        if (tarea == null)
        {
            return RedirectToAction("Index");
        }

        ViewBag.Tarea = tarea;
        return View();
    }

    [HttpPost]
    public IActionResult Compartir(int idTarea, string emailUsuario)
    {
        string usuarioStr = HttpContext.Session.GetString("integrante");
        if (string.IsNullOrEmpty(usuarioStr))
        {
            return RedirectToAction("Index", "Home");
        }

        Usuario usuarioDestino = BD.LevantarUsuarioPorEmail(emailUsuario);
        if (usuarioDestino != null)
        {
            BD.CompartirTarea(idTarea, usuarioDestino.ID);
        }
        
        return RedirectToAction("Index");
    }


    }

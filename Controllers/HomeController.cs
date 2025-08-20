using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP_06_EPSZTEIN_DUEÑAZ_DORIN.Models;

namespace TP_06_EPSZTEIN_DUEÑAZ_DORIN.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
    public IActionResult IniciarSesion()
    {
        return View("IniciarSesion");
    }
    public IActionResult Registro()
    {
        return View();
    }
    public IActionResult CerrarLaSesion()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult iniciarsesion(string nombre, string contraseña)
    {
        Usuario usuario = BD.LevantarUsuario(nombre, contraseña);
        if (usuario != null)
        {
            string StringUsuario = Objeto.ObjectToString(usuario);
            HttpContext.Session.SetString("integrante", StringUsuario);
            return RedirectToAction("Index", "Tareas");
        }
        else
        {
            ViewBag.Error = "El nombre de usuario o la contraseña son incorrectos";
            return View("IniciarSesion");
        }
    }

    [HttpPost]
    public IActionResult Registrar(string nombre, string contraseña)
    {
        Usuario usuario = BD.LevantarUsuarioXNombre(nombre);
        if (usuario != null)
        {
            ViewBag.Error = "Ya existe un usuario con ese nombre";
            return View("Registro");
        }

        Usuario usuario2 = new Usuario(nombre, contraseña);
        BD.AgregarUsuario(usuario);
        
        string Stringusuario = Objeto.ObjectToString(usuario2);
        HttpContext.Session.SetString("integrante", Stringusuario);
        
        return RedirectToAction("Index", "Tareas");
    }
}
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
        return View();
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
    public IActionResult iniciarsesion(string nombre, int contraseña)
    {
        Usuario usuario = BD.LevantarUsuarios(nombre, contraseña);
        if (usuario == null)
        {
            ViewBag.error = "El usuario o contraseña es incorrecto.";
            return View("IniciarSesion");
        }
        else
        {
            HttpContext.Session.SetString("Usuario", usuario.NombreUsuario);
            return RedirectToAction("Perfil");
        }
    }
    public IActionResult registrar(string nombre, int contraseña)
    {
        Usuario usuario = BD.LevantarUsuarios(nombre, contraseña);
        if (usuario == null)
        {
         int registrosafectados = Usuario.AgregarUsuario(usuario, contraseña);
            if (registrosafectados > 0)
             {
            HttpContext.Session.SetString("Usuario", usuario);
            return RedirectToAction("Index");
            }
        }
        else
        {
            ViewBag.error = "Este nombre de usuario ya esta utilizado, por favor ingresar otro";
            return View("Registro");
        }
        return View();
    }
}
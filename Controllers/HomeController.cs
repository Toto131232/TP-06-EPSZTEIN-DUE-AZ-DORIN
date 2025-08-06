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
    public IActionResult Iniciarsesion(string nombreusuario, string contraseña)
    {
         var info = InfoUsuario.LevantarUsuario(nombreusuario, contraseña);

        if (info != null)
        {
            HttpContext.Session.SetString("NombreDeUsuario", info.NombreUsuario);
            return RedirectToAction();
        }
        else
        {
            ViewBag.ErrorAlIniciarSesion = "Incorrecto";
            return View("IniciarSesion");
        }
    }
    public IActionResult Registro(string nombre, string contraseña)
    {
        
    }
}
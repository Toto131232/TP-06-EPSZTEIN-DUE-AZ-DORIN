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
        private readonly ApplicationDbContext _db;
        private readonly PasswordHasher<Usuario> _hasher = new();

        public AuthController(ApplicationDbContext db) { _db = db; }

        public IActionResult Registro()
        {
            return View();
        } 
        public async IActionResult Register(string username, string password, string? email)
        {
            if (await _db.Usuarios.AnyAsync(u => u.Username == username))
            {
                ModelState.AddModelError("", "El usuario ya existe");
                return View();
            }

            var user = new Usuario { Username = username, Email = email };
            user.PasswordHash = _hasher.HashPassword(user, password);

            _db.Usuarios.Add(user);
            await _db.SaveChangesAsync();

            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("Username", user.Username);

            return RedirectToAction("Index", "Tareas");
        }

        public IActionResult IniciarSesion()
        {
            return View();
        }

        public async IActionResult Login(string username, string password)
        {
            var user = await _db.Usuarios.SingleOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                ModelState.AddModelError("", "Usuario o contraseña inválidos");
                return View();
            }

            var res = _hasher.VerifyHashedPassword(user, user.PasswordHash, password);
            if (res == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError("", "Usuario o contraseña inválidos");
                return View();
            }

            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("Username", user.Username);

            return RedirectToAction("Index", "Tareas");
        }

        public IActionResult Desloguear()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
}
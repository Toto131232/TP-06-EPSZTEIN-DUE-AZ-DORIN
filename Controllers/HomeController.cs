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
}
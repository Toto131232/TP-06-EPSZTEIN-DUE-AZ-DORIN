using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP06_ToDoList.Data;
using TP06_ToDoList.Models;

namespace TP06_ToDoList.Controllers
{
    public class TareasController : Controller
    {
        private readonly ApplicationDbContext _db;
        public TareasController(ApplicationDbContext db) { _db = db; }

        private int? CurrentUserId => HttpContext.Session.GetInt32("UserId");

        public async Task<IActionResult> Index()
        {
            if (CurrentUserId == null) return RedirectToAction("Login", "Auth");

            int uid = CurrentUserId.Value;
            var tareas = await _db.Tareas
                .Include(t => t.Owner)
                .Include(t => t.Compartidas).ThenInclude(c => c.Usuario)
                .Where(t => !t.IsDeleted && (t.OwnerId == uid || t.Compartidas.Any(c => c.UsuarioId == uid)))
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();

            return View(tareas);
        }

        
        public IActionResult Create()
        {
            if (CurrentUserId == null) return RedirectToAction("Login", "Auth");
            return View();
        }

        
        public async Task<IActionResult> Create(string titulo, string? descripcion)
        {
            if (CurrentUserId == null) return RedirectToAction("Login", "Auth");

            var tarea = new Tarea { OwnerId = CurrentUserId.Value, Titulo = titulo, Descripcion = descripcion };
            _db.Tareas.Add(tarea);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        
        public async Task<IActionResult> Edit(int id)
        {
            if (CurrentUserId == null) return RedirectToAction("Login", "Auth");

            var tarea = await _db.Tareas.FindAsync(id);
            if (tarea == null || tarea.IsDeleted) return NotFound();
            if (tarea.OwnerId != CurrentUserId) return Forbid();

            return View(tarea);
        }

        
        public async Task<IActionResult> Edit(int id, string titulo, string? descripcion)
        {
            if (CurrentUserId == null) return RedirectToAction("Login", "Auth");

            var tarea = await _db.Tareas.FindAsync(id);
            if (tarea == null || tarea.IsDeleted) return NotFound();
            if (tarea.OwnerId != CurrentUserId) return Forbid();

            tarea.Titulo = titulo;
            tarea.Descripcion = descripcion;
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        
        public async Task<IActionResult> Delete(int id)
        {
            if (CurrentUserId == null) return RedirectToAction("Login", "Auth");

            var tarea = await _db.Tareas.FindAsync(id);
            if (tarea == null) return NotFound();
            if (tarea.OwnerId != CurrentUserId) return Forbid();

            tarea.IsDeleted = true;
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        
        public async Task<IActionResult> ToggleComplete(int id)
        {
            if (CurrentUserId == null) return RedirectToAction("Login", "Auth");

            var tarea = await _db.Tareas.FindAsync(id);
            if (tarea == null || tarea.IsDeleted) return NotFound();

            var uid = CurrentUserId.Value;
            bool can = tarea.OwnerId == uid || await _db.TareaCompartidas.AnyAsync(tc => tc.TareaId == id && tc.UsuarioId == uid);
            if (!can) return Forbid();

            tarea.IsCompleted = !tarea.IsCompleted;
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        
        public async Task<IActionResult> Share(int id)
        {
            if (CurrentUserId == null) return RedirectToAction("Login", "Auth");

            var tarea = await _db.Tareas.Include(t => t.Compartidas).FirstOrDefaultAsync(t => t.Id == id);
            if (tarea == null || tarea.IsDeleted) return NotFound();
            if (tarea.OwnerId != CurrentUserId) return Forbid();

            var usuarios = await _db.Usuarios.Where(u => u.Id != tarea.OwnerId).ToListAsync();
            ViewBag.Usuarios = usuarios;

            return View(tarea);
        }

        
        public async Task<IActionResult> Share(int id, int usuarioId)
        {
            if (CurrentUserId == null) return RedirectToAction("Login", "Auth");

            var tarea = await _db.Tareas.FindAsync(id);
            if (tarea == null || tarea.IsDeleted) return NotFound();
            if (tarea.OwnerId != CurrentUserId) return Forbid();

            if (!await _db.TareaCompartidas.AnyAsync(tc => tc.TareaId == id && tc.UsuarioId == usuarioId))
            {
                _db.TareaCompartidas.Add(new TareaCompartida { TareaId = id, UsuarioId = usuarioId });
                await _db.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}

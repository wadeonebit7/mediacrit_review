using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mediacrit_Review.Data;
using Mediacrit_Review.Models;

namespace Mediacrit_Review.Controllers
{
    public class MediaController : Controller
    {
        // ==============================================================================
        private readonly ApplicationDbContext _context;
        public MediaController(ApplicationDbContext context)
        {
            _context = context;
        }
        // ==============================================================================
        public async Task<IActionResult> Index()
        {
            var media = await _context.Media.ToListAsync();
            return View(media);
        }
        // ==============================================================================

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Type,Creator,ReleaseYear")] Media media)
        {
            if (ModelState.IsValid)
            {
                _context.Add(media);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // Si el modelo no es válido (ej. año fuera de rango), vuelve a mostrar el formulario con los errores
            return View(media);
        }




        // 4. MOSTRAR FORMULARIO DE EDICIÓN (GET: /Media/Edit/5)
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var media = await _context.Media.FindAsync(id);
            if (media == null)
            {
                return NotFound();
            }
            return View(media);
        }

        // 5. PROCESAR LA EDICIÓN (POST: /Media/Edit/5)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Type,Creator,ReleaseYear")] Media mediaChanges)
        {
            if (id != mediaChanges.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Buscamos el elemento original para actualizar sus campos
                    var originalMedia = await _context.Media.FindAsync(id);
                    if (originalMedia == null)
                    {
                        return NotFound();
                    }

                    originalMedia.Title = mediaChanges.Title;
                    originalMedia.Type = mediaChanges.Type;
                    originalMedia.Creator = mediaChanges.Creator;
                    originalMedia.ReleaseYear = mediaChanges.ReleaseYear;

                    _context.Update(originalMedia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MediaExists(mediaChanges.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(mediaChanges);
        }

        private bool MediaExists(int id)
        {
            return _context.Media.Any(e => e.Id == id);
        }




























        [HttpPost]
        [ValidateAntiForgeryToken] // Añade esto por seguridad contra ataques CSRF
        public async Task<IActionResult> Delete(int id)
        {
            var media = await _context.Media.FindAsync(id);

            // 2. Si no existe, devolver un error 404 Not Found
            if (media == null)
            {
                return NotFound(new { mensaje = $"La pelicula con ID {id} no fue encontrado." });
            }
            // 3. Marcar el objeto para eliminación
            _context.Media.Remove(media);

            // 4. Confirmar y guardar los cambios en la base de datos
            await _context.SaveChangesAsync();

            // 5. Volver a cargar el index
            return RedirectToAction(nameof(Index));
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mediacrit_Review.Data;
using Mediacrit_Review.Models;

namespace Mediacrit_Review.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var reviews = await _context.Reviews
                .Include(u => u.User)
                .Include(m => m.Media)  
                .ToListAsync();

            return View(reviews);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]//Obtenga los datos del formulario de la vista de creacion del usuario.
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId, MediaId, Rating, Comment, IsRecommended")] Review review)
        {
            review.CreatedAt = DateTime.Now;

            if (ModelState.IsValid)
            {
                _context.Add(review);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(review);//De todas manera retorna si no es valido
        }








        // GET: Reviews/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // POST: Reviews/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Excluimos 'CreatedAt' para proteger la fecha original de registro
        public async Task<IActionResult> Edit(int id, [Bind("Id, UserId, MediaId, Rating, Comment, IsRecommended")] Review reviewChanges)
        {
            if (id != reviewChanges.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // 1. Buscamos la reseña original en la base de datos para recuperar su CreatedAt
                    var originalReview = await _context.Reviews.FindAsync(id);
                    if (originalReview == null)
                    {
                        return NotFound();
                    }

                    // 2. Actualizamos los campos modificados por el usuario
                    originalReview.UserId = reviewChanges.UserId;
                    originalReview.MediaId = reviewChanges.MediaId;
                    originalReview.Rating = reviewChanges.Rating;
                    originalReview.Comment = reviewChanges.Comment;
                    originalReview.IsRecommended = reviewChanges.IsRecommended;

                    // 3. Guardamos los cambios
                    _context.Update(originalReview);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewExists(reviewChanges.Id))
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
            return View(reviewChanges);
        }

        private bool ReviewExists(int id)
        {
            return _context.Reviews.Any(e => e.Id == id);
        }


        [HttpPost]
        [ValidateAntiForgeryToken] // Añade esto por seguridad contra ataques CSRF
        public async Task<IActionResult> Delete(int id)
        {
            var review = await _context.Reviews.FindAsync(id);

            // 2. Si no existe, devolver un error 404 Not Found
            if (review == null)
            {
                return NotFound(new { mensaje = $"La review con ID {id} no fue encontrado." });
            }
            // 3. Marcar el objeto para eliminación
            _context.Reviews.Remove(review);

            // 4. Confirmar y guardar los cambios en la base de datos
            await _context.SaveChangesAsync();

            // 5. Volver a cargar el index
            return RedirectToAction(nameof(Index));
        }

    }
}

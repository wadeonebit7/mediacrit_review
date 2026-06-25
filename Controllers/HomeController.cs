using Mediacrit_Review.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mediacrit_Review.Controllers
{
    public class HomeController : Controller
    {
        //Siempre debo de tener este atributo privado y constructor
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Comprobación de estado para la demostración en la pizarra
            ViewBag.CanConnect = _context.Database.CanConnect();

            // Carga de reseñas relacionando los usuarios y el contenido multimedia
            var reviews = await _context.Reviews
                .Include(r => r.User)
                .Include(r => r.Media)
                .ToListAsync();

            return View(reviews);
        }
    }
}

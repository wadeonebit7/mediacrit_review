using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mediacrit_Review.Data;
using Mediacrit_Review.Models;


namespace Mediacrit_Review.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }




        [HttpGet]//Esta funcion redirecciona hacia la pagina de cracion del usuario.
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]//Obtenga los datos del formulario de la vista de creacion del usuario.
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Username, Email, Password")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);//De todas manera retorna si no es valido
        }







        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id); //La búsqueda en la base de datos

            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Username, Email")] User userChanges)
        {
            if (id != userChanges.Id)
            {
                return NotFound();
            }

            ModelState.Remove("Password");

            if (ModelState.IsValid)
            {
                try
                {
                    var originalUser = await _context.Users.FindAsync(id);
                    if (originalUser == null)
                    {
                        return NotFound();
                    }

                    originalUser.Username = userChanges.Username;
                    originalUser.Email = userChanges.Email;

                    _context.Update(originalUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(userChanges.Id))
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
            return View(userChanges);
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }





        [HttpPost]
        [ValidateAntiForgeryToken] // Añade esto por seguridad contra ataques CSRF
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);

            // 2. Si no existe, devolver un error 404 Not Found
            if (user == null)
            {
                return NotFound(new { mensaje = $"El usuario con ID {id} no fue encontrado." });
            }
            // 3. Marcar el objeto para eliminación
            _context.Users.Remove(user);

            // 4. Confirmar y guardar los cambios en la base de datos
            await _context.SaveChangesAsync();

            // 5. Volver a cargar el index
            return RedirectToAction(nameof(Index));
        }
    }
}
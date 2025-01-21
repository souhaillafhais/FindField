using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionTerrains.Data;
using GestionTerrains.Models.Domain;

namespace GestionTerrains.Pages.Reservations
{
    public class ModifierReservationModel : PageModel
    {
        private readonly GestionTerrains.Data.ReservationDbContext _context;

        public ModifierReservationModel(ReservationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Reservation Reservation { get; set; } = default!;

        public SelectList TerrainOptions { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            // Vérification du rôle
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "Administrateur") // Si l'utilisateur n'est pas un administrateur
            {
                return RedirectToPage("/AccessDenied");
            }

            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Terrain)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (reservation == null)
            {
                return NotFound();
            }

            Reservation = reservation;

            // Charger les options pour les terrains
            TerrainOptions = new SelectList(await _context.Terrains.ToListAsync(), "Id", "Nom");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Vérification du rôle
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "Administrateur") // Si l'utilisateur n'est pas un administrateur
            {
                return RedirectToPage("/AccessDenied");
            }

            if (!ModelState.IsValid)
            {
                TerrainOptions = new SelectList(await _context.Terrains.ToListAsync(), "Id", "Nom");
                return Page();
            }

            _context.Attach(Reservation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(Reservation.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./ListeReservation");
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }
    }
}

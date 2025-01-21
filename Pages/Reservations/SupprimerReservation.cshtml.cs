using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GestionTerrains.Data;
using GestionTerrains.Models.Domain;

namespace GestionTerrains.Pages.Reservations
{
    public class SupprimerReservationModel : PageModel
    {
        private readonly GestionTerrains.Data.ReservationDbContext _context;

        public SupprimerReservationModel(GestionTerrains.Data.ReservationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Reservation Reservation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
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
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservation != null)
            {
                
                if (reservation.Terrain != null)
                {
                    reservation.Terrain.Disponible = true;
                }

                
                _context.Reservations.Remove(reservation);

                
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./ListeReservation");
        }

    }
}

using System;
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
    public class AddReservationModel : PageModel
    {
        private readonly ReservationDbContext _context;

        public AddReservationModel(ReservationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Reservation Reservation { get; set; } = default!;

        public SelectList TerrainOptions { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {

            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "Administrateur") 
            {
                return RedirectToPage("/AccessDenied");
            }


            TerrainOptions = new SelectList(
                await _context.Terrains.Where(t => t.Disponible).ToListAsync(),
                "Id",
                "Nom"
            );

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "Administrateur") 
            {
                return RedirectToPage("/AccessDenied");
            }

            if (!ModelState.IsValid)
            {
                await LoadTerrainsAsync();
                return Page();
            }


            var isReserved = await _context.Reservations
                .AnyAsync(r => r.TerrainId == Reservation.TerrainId && r.Date == Reservation.Date);

            if (isReserved)
            {
                ModelState.AddModelError(string.Empty, "La date sélectionnée est déjà réservée pour ce terrain.");
                await LoadTerrainsAsync();
                return Page();
            }


            _context.Reservations.Add(Reservation);
            await _context.SaveChangesAsync();

            return RedirectToPage("./ListeReservation");
        }

        [HttpGet("/Reservations/GetUnavailableDates")]
        public async Task<IActionResult> GetUnavailableDates()
        {
            var unavailableDates = await _context.Reservations
                .Select(r => r.Date)
                .Distinct()
                .ToListAsync();

            return new JsonResult(unavailableDates.Select(date => new
            {
                start = date.ToString("yyyy-MM-dd"),
                display = "background",
                title = "Indisponible"
            }));
        }

        private async Task LoadTerrainsAsync()
        {
            TerrainOptions = new SelectList(
                await _context.Terrains.Where(t => t.Disponible).ToListAsync(),
                "Id",
                "Nom"
            );
        }
    }
}

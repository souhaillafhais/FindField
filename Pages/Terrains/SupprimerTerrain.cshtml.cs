using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GestionTerrains.Data;
using GestionTerrains.Models.Domain;

namespace GestionTerrains.Pages.Terrains
{
    public class SupprimerTerrainModel : PageModel
    {
        private readonly GestionTerrains.Data.ReservationDbContext _context;

        public SupprimerTerrainModel(GestionTerrains.Data.ReservationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Terrain Terrain { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
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

            var terrain = await _context.Terrains.FirstOrDefaultAsync(m => m.Id == id);

            if (terrain == null)
            {
                return NotFound();
            }
            else
            {
                Terrain = terrain;
            }
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

            var terrain = await _context.Terrains.FindAsync(id);
            if (terrain != null)
            {
                Terrain = terrain;
                _context.Terrains.Remove(Terrain);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./ListeTerrain");
        }
    }
}

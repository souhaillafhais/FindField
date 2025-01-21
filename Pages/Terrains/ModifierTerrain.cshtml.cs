using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionTerrains.Data;
using GestionTerrains.Models.Domain;

namespace GestionTerrains.Pages.Terrains
{
    public class ModifierTerrainModel : PageModel
    {
        private readonly GestionTerrains.Data.ReservationDbContext _context;

        public ModifierTerrainModel(GestionTerrains.Data.ReservationDbContext context)
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

            var terrain =  await _context.Terrains.FirstOrDefaultAsync(m => m.Id == id);
            if (terrain == null)
            {
                return NotFound();
            }
            Terrain = terrain;
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "Administrateur") // Si l'utilisateur n'est pas un administrateur
            {
                return RedirectToPage("/AccessDenied");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Terrain).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TerrainExists(Terrain.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./ListeTerrain");
        }

        private bool TerrainExists(int id)
        {
            return _context.Terrains.Any(e => e.Id == id);
        }
    }
}

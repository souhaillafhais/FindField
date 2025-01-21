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
    public class DetailsTerrainModel : PageModel
    {
        private readonly GestionTerrains.Data.ReservationDbContext _context;

        public DetailsTerrainModel(GestionTerrains.Data.ReservationDbContext context)
        {
            _context = context;
        }

        public Terrain Terrain { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            
            var terrain = await _context.Terrains
                                        .Include(t => t.Responsable) 
                                        .FirstOrDefaultAsync(m => m.Id == id);
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
    }
}

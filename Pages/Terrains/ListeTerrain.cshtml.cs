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
    public class ListeTerrainModel : PageModel
    {
        private readonly GestionTerrains.Data.ReservationDbContext _context;

        public ListeTerrainModel(GestionTerrains.Data.ReservationDbContext context)
        {
            _context = context;
        }

        public IList<Terrain> Terrain { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; } = string.Empty; 

        public string CurrentFilter { get; set; } = string.Empty;

        public async Task OnGetAsync()
        {
            
            var query = _context.Terrains.AsQueryable();

            
            if (!string.IsNullOrEmpty(SearchString))
            {
                query = query.Where(t => t.Nom.Contains(SearchString));
            }

            
            Terrain = await query.ToListAsync();

            
            CurrentFilter = SearchString;
        }
    }
}

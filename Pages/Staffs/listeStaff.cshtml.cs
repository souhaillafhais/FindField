using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GestionTerrains.Data;
using GestionTerrains.Models.Domain;

namespace GestionTerrains.Pages.Staffs
{
    public class listeStaffModel : PageModel
    {
        private readonly GestionTerrains.Data.ReservationDbContext _context;

        public listeStaffModel(GestionTerrains.Data.ReservationDbContext context)
        {
            _context = context;
        }

        public IList<Staff> Staff { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; } = string.Empty;

        public string CurrentFilter { get; set; } = string.Empty;

        public async Task OnGetAsync()
        {
        
            var query = _context.Staffs.AsQueryable();

            
            if (!string.IsNullOrEmpty(SearchString))
            {
                query = query.Where(s => s.Nom.Contains(SearchString));
            }

            
            Staff = await query.ToListAsync();

            
            CurrentFilter = SearchString;
        }
    }
}

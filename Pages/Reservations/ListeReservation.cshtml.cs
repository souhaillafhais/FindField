using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GestionTerrains.Data;
using GestionTerrains.Models.Domain;

namespace GestionTerrains.Pages.Reservations
{
    public class ListeReservationModel : PageModel
    {
        private readonly GestionTerrains.Data.ReservationDbContext _context;

        public ListeReservationModel(GestionTerrains.Data.ReservationDbContext context)
        {
            _context = context;
        }

        public IList<Reservation> Reservation { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; } = string.Empty;

        public string CurrentFilter { get; set; } = string.Empty;

        public async Task OnGetAsync()
        {
            // Charger les réservations avec leurs terrains associés
            var query = _context.Reservations
                .Include(r => r.Terrain) // Inclure les données du terrain
                .AsQueryable();

            // Filtrer par nom du client si une recherche est effectuée
            if (!string.IsNullOrEmpty(SearchString))
            {
                query = query.Where(r => r.NomClient.Contains(SearchString));
            }

            // Récupérer les données
            Reservation = await query.ToListAsync();
            CurrentFilter = SearchString;
        }
    }
}

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GestionTerrains.Data;
using GestionTerrains.Models.Domain;

namespace GestionTerrains.Pages.Staffs
{
    public class DetailsStaffModel : PageModel
    {
        private readonly GestionTerrains.Data.ReservationDbContext _context;

        public DetailsStaffModel(ReservationDbContext context)
        {
            _context = context;
        }

        public Staff Staff { get; set; } = default!;

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

            var staff = await _context.Staffs.FirstOrDefaultAsync(m => m.Id == id);
            if (staff == null)
            {
                return NotFound();
            }
            else
            {
                Staff = staff;
            }

            return Page();
        }
    }
}

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GestionTerrains.Data;
using GestionTerrains.Models.Domain;

namespace GestionTerrains.Pages.Staffs
{
    public class AddStaffModel : PageModel
    {
        private readonly ReservationDbContext _context;

        public AddStaffModel(ReservationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Staff Staff { get; set; } = new Staff();

        public IActionResult OnGet()
        {
            // Vérifier le rôle de l'utilisateur
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "Administrateur") // Si l'utilisateur n'est pas un administrateur
            {
                return RedirectToPage("/AccessDenied");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Vérifier le rôle de l'utilisateur
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "Administrateur") // Si l'utilisateur n'est pas un administrateur
            {
                return RedirectToPage("/AccessDenied");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Ajouter un nouveau membre du personnel
            _context.Staffs.Add(Staff);
            await _context.SaveChangesAsync();

            // Rediriger vers la liste des membres
            return RedirectToPage("./listeStaff");
        }
    }
}

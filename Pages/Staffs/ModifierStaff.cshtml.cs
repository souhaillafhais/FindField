using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GestionTerrains.Data;
using GestionTerrains.Models.Domain;

namespace GestionTerrains.Pages.Staffs
{
    public class ModifierStaffModel : PageModel
    {
        private readonly ReservationDbContext _context;

        public ModifierStaffModel(ReservationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Staff Staff { get; set; } = default!;

        // Charger les données existantes
        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Vérification du rôle
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "Administrateur") // Si l'utilisateur n'est pas un administrateur
            {
                return RedirectToPage("/AccessDenied");
            }

            Staff = await _context.Staffs.FindAsync(id);

            if (Staff == null)
            {
                return NotFound();
            }

            return Page();
        }

        // Mettre à jour les données
        public async Task<IActionResult> OnPostAsync()
        {
            // Vérification du rôle
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "Administrateur") // Si l'utilisateur n'est pas un administrateur
            {
                return RedirectToPage("/AccessDenied");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var staffToUpdate = await _context.Staffs.FindAsync(Staff.Id);

            if (staffToUpdate == null)
            {
                return NotFound();
            }

            // Mise à jour des propriétés
            staffToUpdate.Nom = Staff.Nom;
            staffToUpdate.Prenom = Staff.Prenom;
            staffToUpdate.Telephone = Staff.Telephone;
            staffToUpdate.Role = Staff.Role;
            staffToUpdate.Email = Staff.Email;
            staffToUpdate.Password = Staff.Password;

            await _context.SaveChangesAsync();

            return RedirectToPage("./listeStaff");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using GestionTerrains.Data;
using System.Linq;

namespace GestionTerrains.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ReservationDbContext _context;

        public LoginModel(ReservationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string? ErrorMessage { get; set; } // Propriété pour stocker le message d'erreur

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("UserRole") != null)
            {
                return RedirectToPage("/Index");
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var staff = _context.Staffs.FirstOrDefault(s => s.Email == Email && s.Password == Password);

            if (staff == null)
            {
                ErrorMessage = "Email ou mot de passe incorrect. Veuillez réessayer."; // Message d'erreur
                return Page();
            }

            // Stocker les données de l'utilisateur dans la session
            HttpContext.Session.SetString("UserRole", staff.Role);
            HttpContext.Session.SetString("UserName", $"{staff.Prenom} {staff.Nom}"); // Stocker le prénom et le nom

            return RedirectToPage("/Index");
        }
    }
}

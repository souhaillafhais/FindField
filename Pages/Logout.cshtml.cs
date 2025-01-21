using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionTerrains.Pages
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            HttpContext.Session.Clear(); // Supprime toutes les données de session
            return RedirectToPage("/Index"); // Redirige vers la page d'accueil
        }
    }
}
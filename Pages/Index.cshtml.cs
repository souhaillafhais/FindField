using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionTerrains.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public string? UserName { get; set; }

        public void OnGet()
        {
            // Récupérer le nom et prénom de l'utilisateur connecté
            UserName = HttpContext.Session.GetString("UserName");
        }
    }
}

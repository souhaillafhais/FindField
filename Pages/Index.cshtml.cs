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
            // R�cup�rer le nom et pr�nom de l'utilisateur connect�
            UserName = HttpContext.Session.GetString("UserName");
        }
    }
}

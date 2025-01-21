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
    public class AddTerrainModel : PageModel
    {
        private readonly GestionTerrains.Data.ReservationDbContext _context;

        public AddTerrainModel(GestionTerrains.Data.ReservationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Terrain Terrain { get; set; } = default!;

       
        public List<Staff> Staffs { get; set; } = new List<Staff>();

        public IActionResult OnGet()
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "Administrateur") // Si l'utilisateur n'est pas un administrateur
            {
                return RedirectToPage("/AccessDenied");
            }

            Staffs = _context.Staffs.ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "Administrateur") // Si l'utilisateur n'est pas un administrateur
            {
                return RedirectToPage("/AccessDenied");
            }
            if (!ModelState.IsValid)
            {
                Staffs = _context.Staffs.ToList(); 
                
                return Page();
            }

            try
            {
               
                _context.Terrains.Add(Terrain);
                await _context.SaveChangesAsync();

                
                return RedirectToPage("./ListeTerrain");
            }
            catch (Exception ex)
            {
                
                ModelState.AddModelError(string.Empty, $"Une erreur est survenue : {ex.Message}");
                return Page();
            }
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace GestionTerrains.Models.Domain
{
    public class Staff
    {
        public int Id { get; set; }

        [Required]
        public string Nom { get; set; }

        [Required]
        public string Prenom { get; set; }

        [Required]
        public string Telephone { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } // Email pour l'authentification

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } // Mot de passe sécurisé
    }
}

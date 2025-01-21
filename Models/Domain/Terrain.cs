using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionTerrains.Models.Domain
{
    public class Terrain
    {
        public int Id { get; set; }

        [Required]
        public string Nom { get; set; }

        [Required]
        public string Type { get; set; } 

        [Required]
        public bool Disponible { get; set; } = true;

        [Required]
        public float PrixParHeure { get; set; }


        [Required]
        public int StaffId { get; set; }
        [ForeignKey("StaffId")]
        public Staff? Responsable { get; set; }


        public string? ImagePath { get; set; } 
    }
}

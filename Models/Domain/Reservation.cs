using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionTerrains.Models.Domain
{
    public class Reservation
    {
        public int Id { get; set; }

        [Required]
        public string NomClient { get; set; }

        [Required]
        public string Telephone { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public TimeSpan HeureDebut { get; set; }

        [Required]
        public TimeSpan HeureFin { get; set; }


        [Required]
        public int TerrainId { get; set; }
        [ForeignKey("TerrainId")]
        public Terrain? Terrain { get; set; }
    }
}


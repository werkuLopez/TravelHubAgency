using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TravelHubAgency.Models
{
    [Table("CONTINENTES")]
    public class Continente
    {
        [Key]
        [Column("ID_Continente")]
        public int IdContinente { get; set; }
        [Column("Nombre")]
        public string Nombre { get; set; }
    }
}

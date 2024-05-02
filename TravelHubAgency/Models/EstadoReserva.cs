using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TravelHubAgency.Models
{
    [Table("ESTADORESERVA")]
    public class EstadoReserva
    {
        [Key]
        [Column("ID_Estado")]
        public int IdEstado { get; set; }
        [Column("Nombre")]
        public string Estado { get; set; }
    }
}

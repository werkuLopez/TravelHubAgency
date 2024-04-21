using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TravelHubAgency.Models
{
    [Table("RESERVAS_VUELOS")]
    public class ReservaVuelo
    {
        [Key]
        [Column("ID_Reserva_vuelo")]
        public int IdReservaVuelo { get; set; }
        [Column("Vuelo")]
        public int Vuelo { get; set; }
        [Column("Usuario")]
        public int IdUsuario { get; set; }

    }
}

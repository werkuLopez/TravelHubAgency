using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelHubAgency.Models
{
    [Table("RESERVAS_HOTELES")]
    public class ReservaHotel
    {
        [Key]
        [Column("ID_Reserva_hote")]
        public int IdReservaHotel { get; set; }
        [Column("Usuario")]
        public int Usuario { get; set; }
        [Column("Hotel")]
        public int Hotel { get; set; }
        [Column("Precio")]
        public decimal Precio { get; set; }
        [Column("Num_Personas")]
        public int Personas { get; set; }
        [Column("Fecha")]
        public DateTime Fecha { get; set; }
    }
}

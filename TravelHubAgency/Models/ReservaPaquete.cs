using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TravelHubAgency.Models
{
    [Table("RESERVAS_PACK")]
    public class ReservaPaquete
    {
        [Key]
        [Column("ID_Reserva_Pack")]
        public int IdReserva { get; set; }
        [Column("Usuario")]
        public int IdUsuario { get; set; }
        [Column("Paquete")]
        public int IdPaquete { get; set; }
        [Column("Fecha_Reserva")]
        public DateTime FechaReserva { get; set; }
        [Column("ID_Estado_Reserva")]
        public int IdEstadoReserva { get; set; }
    }
}

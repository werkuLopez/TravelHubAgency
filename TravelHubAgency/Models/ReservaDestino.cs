using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelHubAgency.Models
{
    [Table("RESERVAS_DESTINO")]
    public class ReservaDestino
    {
        [Key]
        [Column("ID_Reserva_Destino")]
        public int IdReservaDestino { get; set; }
        [Column("Usuario")]
        public int IdUsuario { get; set; }
        [Column("Destino")]
        public int IdDestino { get; set; }
        [Column("Fecha_Reserva")]
        public DateTime FechaReserva { get; set; }
        [Column("Fecha_Inicio")]
        public DateTime FechaReservaInicio { get; set; }
        [Column("Fecha_Fin")]
        public DateTime FechaReservaFin { get; set; }
        [Column("Personas")]
        public int NumPersonas { get; set; }
        [Column("Precio")]
        public decimal Precio { get; set; }
        [Column("ID_Estado_Reserva")]
        public int IdEstadoReserva { get; set; }
    }
}

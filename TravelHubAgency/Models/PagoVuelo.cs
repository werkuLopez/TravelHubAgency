using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelHubAgency.Models
{
    [Table("PAGOS_VUELO")]
    public class PagoVuelo
    {
        [Key]
        [Column("ID_Pago")]
        public int IdPago { get; set; }
        [Column("Vuelo")]
        public int IdVuelo { get; set; }
        [Column("FechaPago")]
        public DateTime FechaPago { get; set; }
        [Column("MetodoPago")]
        public string MetodoPago { get; set; }
        [Column("Importe")]
        public decimal Precio { get; set; }
        [Column("USUARIO")]
        public int Usuario { get; set; }
    }
}

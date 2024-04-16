using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelHubAgency.Models
{
    [Table("VUELOS")]
    public class Vuelo
    {
        [Key]
        [Column("ID_Vuelo")]
        public int IdVuelo { get; set; }
        [Column("Origen")]
        public string Origen { get; set; }
        [Column("Destino")]
        public int IdDestino { get; set; }
        [Column("Aerolinea")]
        public string Aerolinea { get; set; }
        [Column("Fecha_Salida")]
        public DateTime FechaSalida { get; set; } 
        [Column("Fecha_Salida")]
        public DateTime FechaLlegada {  get; set; }
        [Column("Duracion")]
        public int Duracion { get; set; }
        [Column("Precio")]
        public int Precio { get; set; }
    }
}

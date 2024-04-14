using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TravelHubAgency.Models
{
    [Table("DESTINOS")]
    public class Destino
    {
        [Key]
        [Column("ID_Destino")]
        public int IdDestino { get; set; }
        [Column("Nombre")]
        public string Nombre { get; set; }
        [Column("Pais")]
        public int IdPais { get; set; }
        [Column("Region")]
        public string Region { get; set; }
        [Column("Descripcion")]
        public string Descripcion { get; set; }
        [Column("Imagen")]
        public string Imagen { get; set; }
        [Column("Latitud")]
        public string Latitud { get; set; }
        [Column("Longitud")]
        public string Longitud { get; set; }
        [Column("Precio")]
        public decimal Precio { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelHubAgency.Models
{
    [Table("HOTELES")]
    public class Hotel
    {
        [Key]
        [Column("ID_Hotel")]
        public int IdHotel { get; set; }
        [Column("Nombre")]
        public string Nombre { get; set; }
        [Column("Telefono")]
        public string Telefono { get; set; }
        [Column("Descripcion")]
        public string Descripcion { get; set; }
        [Column("Imagen")]
        public string Imagen { get; set; }
        [Column("Precio")]
        public decimal Precio { get; set; }
    }
}

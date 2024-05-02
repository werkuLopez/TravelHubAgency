using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelHubAgency.Models
{
    [Table("ETIQUETAS")]
    public class Etiqueta
    {
        [Key]
        [Column("ID_Etiqueta")]
        public int IdEtiqueta { get; set; }
        [Column("Nombre")]
        public string Nombre { get; set; }
    }
}

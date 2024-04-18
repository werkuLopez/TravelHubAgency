using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TravelHubAgency.Models
{
    [Table("V_PUBLICACIONES")]
    public class PublicacionesView
    {
        [Key]
        [Column("IDARTICULO")]
        public int IdArticulo { get; set; }
        [Column("AUTOR")]
        public string Autor { get; set; }
        [Column("TITULO")]
        public string Titulo { get; set; }
        [Column("CONTENIDO")]
        public string Contenido { get; set; }
        [Column("FECHAPUBLICACION")]
        public DateTime FechaPublicacion { get; set; }
        [Column("IMAGEN")]
        public string Imagen { get; set; }
        [Column("PAIS")]
        public string Pais { get; set; }
        [Column("DESTINO")]
        public string Ciudad { get; set; }
        [Column("IDDESTINO")]
        public int IdDestino { get; set; }

    }
}

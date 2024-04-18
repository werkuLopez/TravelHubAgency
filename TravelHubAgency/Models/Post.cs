using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelHubAgency.Models
{
    [Table("PUBLICACIONES")]
    public class Post
    {
        [Key]
        [Column("ID_Post")]
        public int IdPublicacion { get; set; }
        [Column("Usuario")]
        public int IdUsuario { get; set; }
        [Column("Titulo")]
        public string Titulo { get; set; }
        [Column("Contenido")]
        public string Contenido { get; set; }
        [Column("Fecha")]
        public DateTime FechaPublicacion { get; set; }
        [Column("Imagen")]
        public string Imagen { get; set; }

    }
}

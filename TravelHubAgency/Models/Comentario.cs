using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TravelHubAgency.Models
{
    [Table("COMENTARIOS")]
    public class Comentario
    {
        [Key]
        [Column("ID_Comentario")]
        public int IdComentario { get; set; }
        [Column("Contenido")]
        public string Contenido { get; set; }
        [Column("Fecha")]
        public DateTime Fecha { get; set; }
        [Column("Usuario")]
        public int IdUsuario { get; set; }
        [Column("Post")]
        public int IdPost { get; set; }
    }
}

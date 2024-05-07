using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelHubAgency.Models
{
    [Table("REPLAYSCOMENTARIO")]
    public class ReplayComentario
    {
        [Key]
        [Column("ID_Replay")]
        public int IdReply { get; set; }
        [Column("Usuario")]
        public int IdUsuario { get; set; }
        [Column("Contenido")]
        public string Replay { get; set; }
        [Column("Comentario")]
        public int IdComentario { get; set; }
        [Column("Fecha")]
        public DateTime Fecha { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TravelHubAgency.Models
{
    [Table("USUARIOS")]
    public class Usuario
    {
        [Key]
        [Column("ID_Usuario")]
        public int IdUsuario { get; set; }
        [Column("Nombre")]
        public string Nombre { get; set; }
        [Column("Apellido")]
        public string Apellido { get; set; }
        [Column("Email")]
        public string Email { get; set; }
        [Column("Password")]
        public string Password { get; set; }
        [Column("Tipousuario")]
        public int TipoUsuario { get; set; }
        [Column("Salt")]
        public string Salt { get; set; }
        [Column("Token")]
        public string Token { get; set; }
        [Column("Imagen")]
        public string Imagen { get; set; }
        [Column("Activo")]
        public bool Activo { get; set; }
        [Column("Pais")]
        public string Pais { get; set; }

    }
}

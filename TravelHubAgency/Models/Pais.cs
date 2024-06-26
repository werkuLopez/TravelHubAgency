﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TravelHubAgency.Models
{
    [Table("PAISES")]
    public class Pais
    {
        [Key]
        [Column("ID_Pais")]
        public int IdPais { get; set; }
        [Column("Nombre")]
        public string Nombre { get; set; }
        [Column("Continente")]
        public int IdContinente { get; set; }
    }
}

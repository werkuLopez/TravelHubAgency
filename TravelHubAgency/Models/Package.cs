﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelHubAgency.Models
{
    [Table("PAQUETES")]
    public class Package
    {
        [Key]
        [Column("ID_Paquete")]
        public int IdPack { get; set; }
        [Column("Nombre")]
        public string Nombre { get; set; }
        [Column("ID_Destino")]
        public int IdDestino { get; set; }
        [Column("Fecha_Inicio")]
        public DateTime FechaInicio { get; set; }
        [Column("Fecha_Fin")]
        public DateTime FechaFin { get; set; }
        [Column("Duracion")]
        public int Duracion { get; set; }
        [Column("Precio")]
        public decimal Precio { get; set; }
        [Column("Num_Personas")]
        public int Personas { get; set; }
        [Column("Descripcion")]
        public string Descripcion { get; set; }
        [Column("Imagen")]
        public string Imagen { get; set; }
        [Column("ID_Hotel")]
        public int? IdHotel { get; set; }
        [Column("ID_Vuelo")]
        public int? IdVuelo { get; set; }
    }
}

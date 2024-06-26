﻿using Microsoft.EntityFrameworkCore;
using TravelHubAgency.Models;

namespace TravelHubAgency.Data
{
    public class TravelhubContext: DbContext
    {
        public TravelhubContext(DbContextOptions<TravelhubContext>options):base(options) { }
        public DbSet<Continente> Continentes { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Destino> Destinos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}

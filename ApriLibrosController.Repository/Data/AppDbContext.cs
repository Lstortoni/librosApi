using ApiLibrosController.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrosController.Repository.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt) { }

        // 🔹 Cada DbSet representa una tabla en la base
        public DbSet<Inscripto> Inscriptos => Set<Inscripto>();
        public DbSet<AuthUser> AuthUsers => Set<AuthUser>();
        public DbSet<Libro> Libros => Set<Libro>();
        public DbSet<FotoLibro> FotosLibro => Set<FotoLibro>();
        public DbSet<Reserva> Reservas => Set<Reserva>();
        public DbSet<Localidad> Localidades => Set<Localidad>();

        protected override void OnModelCreating(ModelBuilder b)
        {
            // Esto le dice a EF que aplique todas las clases de configuración
            // que implementen IEntityTypeConfiguration<T> dentro del mismo assembly.
            b.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}

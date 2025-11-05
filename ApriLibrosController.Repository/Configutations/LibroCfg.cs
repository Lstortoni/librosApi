using ApiLibrosController.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrosController.Repository.Configutations
{
    public class LibroCfg : IEntityTypeConfiguration<Libro>
    {
        public void Configure(EntityTypeBuilder<Libro> e)
        {
            // 1) Tabla y clave
            e.ToTable("Libros");          // opcional (si no, EF usa pluralización por convención)
            e.HasKey(x => x.Id);          // PK

            // 2) Columnas (tipos, longitudes, required/optional)
            e.Property(x => x.Titulo)
             .IsRequired()                // string no-null
             .HasMaxLength(250);

            e.Property(x => x.Autor)
             .IsRequired()
             .HasMaxLength(200);

            e.Property(x => x.Descripcion)
             .HasMaxLength(4000);         // null permitido por ser string?

            e.Property(x => x.Precio)
             .HasPrecision(12, 2);        // DECIMAL(12,2) en PostgreSQL → numeric(12,2)

            e.Property(x => x.FechaPublicacion)
             .HasPrecision(6);            // timestamptz(6) si mapeás UTC

            // 3) Enums (guardarlos como texto hace la BD más legible)
            e.Property(x => x.Condicion)
             .HasConversion<string>()
             .HasMaxLength(50);

            e.Property(x => x.Estado)
             .HasConversion<string>()
             .HasMaxLength(50);

            e.Property(x => x.Disponibilidad)
             .HasConversion<string>()
             .HasMaxLength(50);

            // 4) Relaciones

            // (a) Muchos Libros → 1 Propietario (Inscripto)
            //    FK: PropietarioId (Guid) en Libro
            e.HasOne(x => x.Propietario)
             .WithMany(x => x.Libros)     // la colección inversa en Inscripto
             .HasForeignKey(x => x.PropietarioId)
             .OnDelete(DeleteBehavior.Restrict);
            // Restrict: no permitas borrar un Inscripto si tiene libros

            // (b) 1 Libro → muchas Fotos
            e.HasMany(x => x.Fotos)
             .WithOne(x => x.Libro!)
             .HasForeignKey(x => x.LibroId)
             .OnDelete(DeleteBehavior.Cascade);
            // Cascade: si borrás un Libro, se borran sus fotos

            // (c) 1 Libro → muchas Reservas
            e.HasMany(x => x.Reservas)
             .WithOne(x => x.Libro!)
             .HasForeignKey(x => x.LibroId)
             .OnDelete(DeleteBehavior.NoAction);
            // NoAction: controlás la eliminación por tu lógica (evita cascadas “sorpresa”)

            // 5) Índices útiles (performance)
            e.HasIndex(x => x.PropietarioId);
            e.HasIndex(x => new { x.Estado, x.Disponibilidad });  // búsquedas por estado/disp
            e.HasIndex(x => x.Titulo);                            // si buscás por título
        
    }
    }
}

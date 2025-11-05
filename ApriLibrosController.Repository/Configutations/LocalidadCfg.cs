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
    public class LocalidadCfg : IEntityTypeConfiguration<Localidad>
    {
        public void Configure(EntityTypeBuilder<Localidad> e)
        {
            e.HasKey(x => x.Id);

            e.Property(x => x.Nombre)
             .IsRequired()
             .HasMaxLength(150);

            e.Property(x => x.Provincia)
             .HasMaxLength(150);

            // 1:N Localidad → Inscriptos (FK en Inscripto.LocalidadId)
            e.HasMany(x => x.Inscriptos)
             .WithOne(x => x.Localidad)
             .HasForeignKey(x => x.Id)
             .OnDelete(DeleteBehavior.Restrict);

            // Único por Nombre+Provincia (opcional pero útil)
            e.HasIndex(x => new { x.Nombre, x.Provincia }).IsUnique(false);
        }
    }
}

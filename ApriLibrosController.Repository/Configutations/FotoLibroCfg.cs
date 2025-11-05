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
    public class FotoLibroCfg : IEntityTypeConfiguration<FotoLibro>
    {
        public void Configure(EntityTypeBuilder<FotoLibro> e)
        {
            e.HasKey(x => x.Id);

            e.Property(x => x.Url)
             .IsRequired()
             .HasMaxLength(1000);

            e.Property(x => x.Orden)
             .HasDefaultValue(0);

            e.Property(x => x.EsPrincipal)
             .HasDefaultValue(false);

            e.HasOne(x => x.Libro)
             .WithMany(x => x.Fotos)
             .HasForeignKey(x => x.LibroId)
             .OnDelete(DeleteBehavior.Cascade);

            // Suele ser útil para elegir portada rápidamente
            e.HasIndex(x => new { x.LibroId, x.EsPrincipal });

            // Orden dentro de un libro (no único, pero ayuda a ordenar)
            e.HasIndex(x => new { x.LibroId, x.Orden });
        }
    }
}

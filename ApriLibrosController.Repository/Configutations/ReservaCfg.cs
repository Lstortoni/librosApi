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
    public class ReservaCfg : IEntityTypeConfiguration<Reserva>
    {
        public void Configure(EntityTypeBuilder<Reserva> e)
        {
            e.HasKey(x => x.Id);

            // Estado enum como string (opcional, más legible en DB)
            e.Property(x => x.Estado)
             .HasConversion<string>()
             .HasMaxLength(30);

            e.Property(x => x.MensajeInicial)
             .HasMaxLength(2000);

            // Relaciones
            e.HasOne(x => x.Libro)
             .WithMany(x => x.Reservas)
             .HasForeignKey(x => x.LibroId)
             .OnDelete(DeleteBehavior.NoAction);

            // Dos FKs a la misma entidad (Inscripto)
            e.HasOne(x => x.Comprador)
             .WithMany(x => x.ReservasHechas)
             .HasForeignKey(x => x.CompradorId)
             .OnDelete(DeleteBehavior.NoAction);

            e.HasOne(x => x.Vendedor)
             .WithMany(x => x.ReservasSobreMisLibros)
             .HasForeignKey(x => x.VendedorId)
             .OnDelete(DeleteBehavior.NoAction);

            // Índices útiles
            e.HasIndex(x => x.LibroId);
            e.HasIndex(x => new { x.CompradorId, x.Estado });
            e.HasIndex(x => new { x.VendedorId, x.Estado });
        }
    }
}

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
    public class InscriptoCfg : IEntityTypeConfiguration<Inscripto>
    {
        public void Configure(EntityTypeBuilder<Inscripto> e)
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Nombre).IsRequired().HasMaxLength(200);
            e.Property(x => x.Email).IsRequired().HasMaxLength(200);

            e.HasOne(x => x.Localidad)
             .WithMany()
             .HasForeignKey(x => x.Id)
             .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(x => x.Auth)
             .WithOne(x => x.Inscripto)
             .HasForeignKey<AuthUser>(x => x.Id)
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

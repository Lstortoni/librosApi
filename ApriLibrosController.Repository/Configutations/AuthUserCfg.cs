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
    public class AuthUserCfg : IEntityTypeConfiguration<AuthUser>
    {
        public void Configure(EntityTypeBuilder<AuthUser> e)
        {
            e.HasKey(x => x.Id);

            e.Property(x => x.EmailLogin)
             .IsRequired()
             .HasMaxLength(200);

            e.HasIndex(x => x.EmailLogin).IsUnique(); // evita duplicados de login

            e.Property(x => x.PasswordHash)
             .IsRequired();

            e.Property(x => x.CreatedAt)
             .HasPrecision(6); // pg: timestamptz(6) si usás UTC

            e.Property(x => x.LastLoginAt)
             .HasPrecision(6);

            // Relación 1:1 con clave compartida (config base ya la tenés en InscriptoCfg)
            e.HasOne(x => x.Inscripto)
             .WithOne(x => x.Auth)
             .HasForeignKey<AuthUser>(x => x.Id)
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

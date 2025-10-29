using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrosController.Model
{
    public class AuthUser
    {
        public Guid Id { get; set; }              // PK (== InscriptoId si querés 1:1)
        public Guid InscriptoId { get; set; }     // FK a Inscripto

        public string EmailLogin { get; set; } = "";  // para login (puede = Email de Inscripto)
        public string PasswordHash { get; set; } = "";
        public string PasswordSalt { get; set; } = ""; // si usás salado manual

        public string[] Roles { get; set; } = new[] { "Usuario" }; // p.ej. "Usuario","Vendedor"

        public bool EmailConfirmed { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastLoginAt { get; set; }
        public bool IsLocked { get; set; } = false;

        // (Opcional) refresh tokens si los querés:
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiresAt { get; set; }
    }
}

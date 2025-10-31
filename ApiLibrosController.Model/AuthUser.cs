using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrosController.Model
{
    public class AuthUser
    {
        public Guid Id { get; set; }                      // PK = FK a Inscripto
        public string EmailLogin { get; set; } = "";      // puede = Inscripto.Email
        public string PasswordHash { get; set; } = "";
        public bool EmailConfirmado { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastLoginAt { get; set; }

        public Inscripto? Inscripto { get; set; }
    }
}

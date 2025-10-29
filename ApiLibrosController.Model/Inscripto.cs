using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrosController.Model
{
    public class Inscripto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = "";
        public string Email { get; set; } = "";

        public string? Telefono { get; set; }       // común
        public string? Localidad { get; set; }      // común

        public AuthUser Usuario { get; set; } = new();
        public PerfilComun Comun { get; set; } = new();           // siempre
        public PerfilVendedor? Vendedor { get; set; } = null;     // opcional
    }
}

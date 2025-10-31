using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrosController.Model
{
    public class Inscripto
    {
        public Guid Id { get; set; }                      // PK
        public string Nombre { get; set; } = "";
        public string Email { get; set; } = "";
        public string? Telefono { get; set; }
        public Localidad Localidad { get; set; } = null!;
        public DateTime FechaAlta { get; set; } = DateTime.UtcNow;

        public AuthUser? Auth { get; set; }               // 1:1
        public ICollection<Libro> Libros { get; set; } = new List<Libro>();

        // 🔹 Reservas donde soy comprador
        public ICollection<Reserva> ReservasHechas { get; set; } = new List<Reserva>();

        // 🔹 Reservas hechas por otros sobre mis libros (soy vendedor/propietario)
        public ICollection<Reserva> ReservasSobreMisLibros { get; set; } = new List<Reserva>();
    }
}

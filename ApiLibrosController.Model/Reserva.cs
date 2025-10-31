using ApiLibrosController.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrosController.Model
{
    public class Reserva
    {
        public Guid Id { get; set; }

        public int LibroId { get; set; }
        public Libro? Libro { get; set; }

        public Guid CompradorId { get; set; }   // quien reserva
        public Inscripto? Comprador { get; set; }

        public Guid VendedorId { get; set; }    // dueño del libro
        public Inscripto? Vendedor { get; set; }

        public DateTime Creada { get; set; } = DateTime.UtcNow;
        public DateTime ExpiraEn { get; set; } = DateTime.UtcNow.AddDays(2);
        public EstadoReserva Estado { get; set; } = EstadoReserva.Activa; // Activa/Cancelada/Expirada/Cerrada
        public string? MensajeInicial { get; set; }
    }
}

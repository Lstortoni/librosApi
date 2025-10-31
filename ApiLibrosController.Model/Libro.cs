
using ApiLibrosController.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrosController.Model
{
    public class Libro
    {

        public int Id { get; set; }
        public string Titulo { get; set; } = "";
        public string Autor { get; set; } = "";
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }

        public CondicionLibro Condicion { get; set; } = CondicionLibro.UsadoBueno;
        public EstadoPublicacion Estado { get; set; } = EstadoPublicacion.Publicado;
        public DisponibilidadLibro Disponibilidad { get; set; } = DisponibilidadLibro.Disponible;

        public DateTime FechaPublicacion { get; set; } = DateTime.UtcNow;

        public Guid PropietarioId { get; set; }
        public Inscripto? Propietario { get; set; }

        public List<FotoLibro> Fotos { get; set; } = new();

        // (Opcional) histórico de reservas
        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }
}

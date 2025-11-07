using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrosController.Contracts.Dto.Inscripto
{
    public class InscriptoDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = "";
        public string Email { get; set; } = "";
        public string? Telefono { get; set; }
        public int LocalidadId { get; set; }
        public string LocalidadNombre { get; set; } = "";
        public DateTime FechaAlta { get; set; }
    }
}

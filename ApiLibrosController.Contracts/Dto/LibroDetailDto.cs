using ApiLibrosController.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrosController.Contracts.Dto
{
    public class LibroDetailDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = "";
        public string Autor { get; set; } = "";
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public CondicionLibro Condicion { get; set; }
        public EstadoPublicacion Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaPublicacion { get; set; }

        public VendedorBasicDto Vendedor { get; set; } = new();
        public List<FotoLibroDto> Fotos { get; set; } = new();
    }
}


using ApiLibrosController.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ApiLibrosController.Contracts.Dto
{
   public class LibroListItemDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = "";
        public string Autor { get; set; } = "";
        public decimal Precio { get; set; }
        public EstadoPublicacion Estado { get; set; }
        public string? FotoPrincipalUrl { get; set; }  // primera o marcada como principal
        public VendedorBasicDto Vendedor { get; set; } = new();
        public DateTime FechaPublicacion { get; set; }
    }
}

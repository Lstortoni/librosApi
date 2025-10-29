using ApiLibrosController.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrosController.Contracts.Dto
{
  public class ListLibrosQueryDto
    {
        public string? Term { get; set; }            // busca en Título/Autor
        public int? VendedorId { get; set; }
        public EstadoPublicacion? Estado { get; set; }
        public CondicionLibro? Condicion { get; set; }

        [Range(1, 200)]
        public int PageSize { get; set; } = 20;

        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; } = 1;

        public string? SortBy { get; set; } = "FechaPublicacion_DESC"; // o "Precio_ASC", etc.
    }
}

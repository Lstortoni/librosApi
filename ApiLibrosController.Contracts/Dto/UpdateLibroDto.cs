using ApiLibrosController.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrosController.Contracts.Dto
{
   public class UpdateLibroDto
    {
        [StringLength(180)]
        public string? Titulo { get; set; }

        [StringLength(120)]
        public string? Autor { get; set; }

        [StringLength(2000)]
        public string? Descripcion { get; set; }

        [Range(1, 1_000_000)]
        public decimal? Precio { get; set; }

        public CondicionLibro? Condicion { get; set; }
        public EstadoPublicacion? Estado { get; set; }
    }
}

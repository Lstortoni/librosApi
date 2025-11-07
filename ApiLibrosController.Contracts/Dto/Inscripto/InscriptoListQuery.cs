using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrosController.Contracts.Dto.Inscripto
{
    public class InscriptoListQuery
    {
        public string? Search { get; set; }         // busca en Nombre/Email
        public int? LocalidadId { get; set; }
        public DateTime? Desde { get; set; }        // FechaAlta >=
        public DateTime? Hasta { get; set; }        // FechaAlta <=
        public int Page { get; set; } = 1;          // 1-based
        public int PageSize { get; set; } = 20;
        public string? SortBy { get; set; }         // "nombre", "fechaAlta"
        public bool Desc { get; set; } = false;
    }
}

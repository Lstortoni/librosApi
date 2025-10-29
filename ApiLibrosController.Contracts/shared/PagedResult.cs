using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrosController.Contracts.shared
{
    
    public class PagedResult<C>
    {
        public int PageNumber { get; init; }       // Número de página actual
        public int PageSize { get; init; }         // Cantidad de elementos por página
        public int TotalCount { get; init; }       // Total de registros encontrados
        public IReadOnlyList<C> Items { get; init; } = Array.Empty<C>(); // Los datos reales
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrosController.Contracts.Dto
{
    public class ListVendedorQueryDto
    {
        public string? Nombre { get; set; }

        [Range(1, 200)]
        public int PageSize { get; set; } = 20;

        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; } = 1;

        // (opcional) orden
        public string? SortBy { get; set; } = "Nombre_ASC";


    }
}

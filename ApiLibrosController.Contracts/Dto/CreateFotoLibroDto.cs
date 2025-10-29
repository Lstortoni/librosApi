using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrosController.Contracts.Dto
{
    public class CreateFotoLibroDto
    {
        [Required, StringLength(500)]
        public string Url { get; set; } = "";

        public bool EsPrincipal { get; set; } = false;

        [Range(0, 100)]
        public int Orden { get; set; } = 0;
    }
}

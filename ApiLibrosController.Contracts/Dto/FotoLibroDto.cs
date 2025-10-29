using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrosController.Contracts.Dto
{
    public class FotoLibroDto
    {
        public int Id { get; set; }
        public string Url { get; set; } = "";
        public bool EsPrincipal { get; set; }
        public int Orden { get; set; }
    }
}

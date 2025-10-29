using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrosController.Model
{
    public class FotoLibro
    {
        public int Id { get; set; }
        public string Url { get; set; } = "";    // Puede ser URL pública o ruta local/obj storage
        public bool EsPrincipal { get; set; } = false;
        public int Orden { get; set; } = 0;

        // Relación
        public int LibroId { get; set; }
        public Libro? Libro { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrosController.Model
{
    public class Vendedor
    {
        public int Id { get; set; }                 // Identificador interno
        public string Nombre { get; set; } = "";    // Visible en la publicación
        public string Email { get; set; } = "";     // Contacto (para futuro flujo de compra)
        public string? Telefono { get; set; }       // Opcional
        public DateTime FechaAlta { get; set; } = DateTime.UtcNow;
        public bool Activo { get; set; } = true;

        // Navegación inversa (conveniente para consultas)
        public ICollection<Libro> Libros { get; set; } = new List<Libro>();
    }
}

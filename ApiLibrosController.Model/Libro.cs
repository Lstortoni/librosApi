
using ApiLibrosController.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrosController.Model
{
    public class Libro
    {
        public int Id { get; set; }

        // Datos visibles
        public string Titulo { get; set; } = "";
        public string Autor { get; set; } = "";
        public string? Descripcion { get; set; }

        // Precio (moneda simple por ahora; si luego necesitás multi-moneda, lo modelamos)
        public decimal Precio { get; set; }

        // Metadatos útiles
        public CondicionLibro Condicion { get; set; } = CondicionLibro.UsadoBueno;
        public EstadoPublicacion Estado { get; set; } = EstadoPublicacion.Borrador;
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public DateTime? FechaPublicacion { get; set; }   // se setea cuando pasás a "Publicado"

        // Relación con vendedor
        public int VendedorId { get; set; }        // FK (rápido para consultas/filtrar)
        public Vendedor? Vendedor { get; set; }    // Navegación (si querés sus datos)

        // Fotos (opcional pero muy útil para el listing)
        public List<FotoLibro> Fotos { get; set; } = new();
    }
}

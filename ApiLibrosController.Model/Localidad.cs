using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrosController.Model
{
    public class Localidad
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public string? Provincia { get; set; }   // opcional
        public ICollection<Inscripto> Inscriptos { get; set; } = new List<Inscripto>();
    }
}

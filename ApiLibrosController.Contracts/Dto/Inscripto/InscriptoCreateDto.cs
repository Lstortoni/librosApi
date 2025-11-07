using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrosController.Contracts.Dto.Inscripto
{
    public class InscriptoCreateDto
    {

        [Required, StringLength(200)]
        public string Nombre { get; set; } = "";

        [Required, EmailAddress, StringLength(200)]
        public string Email { get; set; } = "";

        [StringLength(50)]
        public string? Telefono { get; set; }

        [Range(1, int.MaxValue)]
        public int LocalidadId { get; set; }
    }

}


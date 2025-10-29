using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrosController.Contracts.Dto
{
    public class CreateVendedorDto
    {
        [Required, StringLength(80)]
        public string Nombre { get; set; } = "";

        [Required, EmailAddress, StringLength(120)]
        public string Email { get; set; } = "";

        [Phone, StringLength(30)]
        public string? Telefono { get; set; }
    }
}

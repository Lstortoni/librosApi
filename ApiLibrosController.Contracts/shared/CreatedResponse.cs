using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrosController.Contracts.shared
{
    public class CreatedResponse
    {
        public Guid Id { get; init; }
        public string Message { get; init; } = "Creado correctamente.";
    }
}

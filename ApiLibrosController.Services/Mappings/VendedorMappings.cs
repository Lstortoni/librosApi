using ApiLibrosController.Contracts.Dto;
using ApiLibrosController.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrosController.Services.Mappings
{
    public static class VendedorMappings
    {
        public static Vendedor ToEntity(this CreateVendedorDto dto)
        {
            return new Vendedor
            {
                Nombre = dto.Nombre,
                Email = dto.Email,
                Telefono = dto.Telefono,
                Activo = true,
                FechaAlta = DateTime.UtcNow
            };
        }

        // Entidad -> DTO (básico)
        public static VendedorBasicDto ToBasicDto(this Vendedor v)
            => new VendedorBasicDto { Id = v.Id, Nombre = v.Nombre };
    }
}

using ApiLibrosController.Contracts.Dto;
using ApiLibrosController.Model.Enum;
using ApiLibrosController.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrosController.Services.Mappings
{
    public static class LibroMappings
    {
        // DTO -> Entidad (crear)
        public static Libro ToEntity(this CreateLibroDto dto)
        {
            return new Libro
            {
                Titulo = dto.Titulo,
                Autor = dto.Autor,
                Descripcion = dto.Descripcion,
                Precio = dto.Precio,
                Condicion = dto.Condicion,
                Estado = EstadoPublicacion.Borrador,
                VendedorId = dto.VendedorId,
                FechaCreacion = DateTime.UtcNow,
                Fotos = dto.Fotos?.Select(f => new FotoLibro
                {
                    Url = f.Url,
                    EsPrincipal = f.EsPrincipal,
                    Orden = f.Orden
                }).ToList() ?? new List<FotoLibro>()
            };
        }

        // Entidad -> DTO (listado / card)
        public static LibroListItemDto ToListItemDto(this Libro l)
        {
            var principal = l.Fotos.FirstOrDefault(f => f.EsPrincipal)?.Url
                         ?? l.Fotos.OrderBy(f => f.Orden).FirstOrDefault()?.Url;

            return new LibroListItemDto
            {
                Id = l.Id,
                Titulo = l.Titulo,
                Autor = l.Autor,
                Precio = l.Precio,
                Estado = l.Estado,
                FotoPrincipalUrl = principal,
                Vendedor = new VendedorBasicDto
                {
                    Id = l.VendedorId,
                    Nombre = l.Vendedor?.Nombre ?? string.Empty
                },
                FechaPublicacion = l.FechaPublicacion ?? l.FechaCreacion
            };
        }

        // Entidad -> DTO (detalle)
        public static LibroDetailDto ToDetailDto(this Libro l)
        {
            return new LibroDetailDto
            {
                Id = l.Id,
                Titulo = l.Titulo,
                Autor = l.Autor,
                Descripcion = l.Descripcion,
                Precio = l.Precio,
                Condicion = l.Condicion,
                Estado = l.Estado,
                FechaCreacion = l.FechaCreacion,
                FechaPublicacion = l.FechaPublicacion,
                Vendedor = new VendedorBasicDto
                {
                    Id = l.VendedorId,
                    Nombre = l.Vendedor?.Nombre ?? string.Empty
                },
                Fotos = l.Fotos
                    .OrderBy(f => f.Orden)
                    .Select(f => new FotoLibroDto
                    {
                        Id = f.Id,
                        Url = f.Url,
                        EsPrincipal = f.EsPrincipal,
                        Orden = f.Orden
                    }).ToList()
            };
        }

        // Actualización parcial (aplica DTO sobre entidad existente)
        public static void ApplyUpdate(this Libro libro, UpdateLibroDto dto)
        {
            if (dto.Titulo is not null) libro.Titulo = dto.Titulo;
            if (dto.Autor is not null) libro.Autor = dto.Autor;
            if (dto.Descripcion is not null) libro.Descripcion = dto.Descripcion;
            if (dto.Precio.HasValue) libro.Precio = dto.Precio.Value;
            if (dto.Condicion.HasValue) libro.Condicion = dto.Condicion.Value;
            if (dto.Estado.HasValue) libro.Estado = dto.Estado.Value;
        }
    }
}

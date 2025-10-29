
using ApiLibrosController.Contracts.Dto;
using ApiLibrosController.Contracts.shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrosController.Interfaces.Services
{
    public interface ILibroService
    {
        Task<LibroDetailDto> CreateAsync(CreateLibroDto dto, CancellationToken ct = default);
        Task<LibroDetailDto?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<PagedResult<LibroListItemDto>> ListAsync(ListLibrosQueryDto query, CancellationToken ct = default);
        Task<bool> PublishAsync(int id, CancellationToken ct = default);   // cambia a Publicado + FechaPublicacion
        Task<bool> UpdateAsync(int id, UpdateLibroDto dto, CancellationToken ct = default);

        // ... lo que ya tenés
        Task<IReadOnlyList<LibroListItemDto>> ListAllAsync(ListLibrosQueryDto? filter = null, CancellationToken ct = default);

    }
}

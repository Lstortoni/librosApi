using ApiLibrosController.Contracts.Dto;
using ApiLibrosController.Model.Enum;
using ApiLibrosController.Contracts.shared;
using ApiLibrosController.Interfaces.Repositories;
using ApiLibrosController.Interfaces.Services;
using ApiLibrosController.Services.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrosController.Services
{
    public class LibroService:ILibroService
    {
        private readonly ILibroRepository _repo;
        private readonly IVendedorRepository _vendedores;

        public LibroService(ILibroRepository repo, IVendedorRepository vendedores)
        {
            _repo = repo;
            _vendedores = vendedores;
        }

        public async Task<LibroDetailDto> CreateAsync(CreateLibroDto dto, CancellationToken ct = default)
        {
            if (!await _vendedores.ExistsAsync(dto.VendedorId, ct))
                throw new ArgumentException("Vendedor inexistente");

            var entity = dto.ToEntity();                 // <-- mapping
            var saved = await _repo.AddAsync(entity, ct);

            // Si querés devolver “completo”, recargás con includes:
            var full = await _repo.GetByIdAsync(saved.Id, includeVendedor: true, includeFotos: true, ct);
            return (full ?? saved).ToDetailDto();        // <-- mapping
        }

        public async Task<LibroDetailDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var e = await _repo.GetByIdAsync(id, includeVendedor: true, includeFotos: true, ct);
            return e?.ToDetailDto();
        }

        public async Task<PagedResult<LibroListItemDto>> ListAsync(ListLibrosQueryDto q, CancellationToken ct = default)
        {
            var (items, total) = await _repo.ListAsync(
                q.Term, q.VendedorId, q.Estado, q.Condicion,
                q.SortBy, q.PageNumber, q.PageSize, ct);

            var dtos = items.Select(x => x.ToListItemDto()).ToList();

            return new PagedResult<LibroListItemDto>
            {
                PageNumber = q.PageNumber,
                PageSize = q.PageSize,
                TotalCount = total,
                Items = dtos
            };
        }

        public async Task<bool> PublishAsync(int id, CancellationToken ct = default)
        {
            var e = await _repo.GetByIdAsync(id, includeVendedor: false, includeFotos: false, ct);
            if (e is null) return false;

            if (e.Estado == EstadoPublicacion.Publicado) return true;

            if (e.Precio <= 0) throw new InvalidOperationException("Precio inválido");

            e.Estado = EstadoPublicacion.Publicado;
            e.FechaPublicacion = DateTime.UtcNow;

            await _repo.UpdateAsync(e, ct);
            return true;
        }

        public async Task<bool> UpdateAsync(int id, UpdateLibroDto dto, CancellationToken ct = default)
        {
            var e = await _repo.GetByIdAsync(id, ct: ct);
            if (e is null) return false;

            e.ApplyUpdate(dto); // mapeo parcial en App
            await _repo.UpdateAsync(e, ct);
            return true;
        }

        public async Task<IReadOnlyList<LibroListItemDto>> ListAllAsync(ListLibrosQueryDto? q, CancellationToken ct = default)
        {
            var items = await _repo.ListAllAsync(
                q?.Term, q?.VendedorId, q?.Estado, q?.Condicion, q?.SortBy, ct);

            return items.Select(x => x.ToListItemDto()).ToList();
        }
    }
}

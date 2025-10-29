using ApiLibrosController.Contracts.Dto;
using ApiLibrosController.Contracts.shared;
using ApiLibrosController.Interfaces.Repositories;
using ApiLibrosController.Interfaces.Services;
using ApiLibrosController.Model;
using ApiLibrosController.Services.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrosController.Services
{
    public class VendedorService: IVendedorService
    {
        private readonly IVendedorRepository _repo;

        public VendedorService(IVendedorRepository repo) => _repo = repo;

        public async Task<VendedorBasicDto> CreateAsync(CreateVendedorDto dto, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre)) throw new ArgumentException("Nombre requerido");
            if (string.IsNullOrWhiteSpace(dto.Email)) throw new ArgumentException("Email requerido");

            var entity = dto.ToEntity();                  // <-- mapping
            var saved = await _repo.AddAsync(entity, ct);
            return saved.ToBasicDto();
        }

        public async Task<VendedorBasicDto?> GetBasicByIdAsync(int id, CancellationToken ct = default)
        {
            var v = await _repo.GetByIdAsync(id, ct);
            return v?.ToBasicDto();
        }

        public Task<bool> ExistsAsync(int id, CancellationToken ct = default) => _repo.ExistsAsync(id, ct);

        public async Task<PagedResult<VendedorBasicDto>> ListAsync(ListVendedorQueryDto query, CancellationToken ct = default)
        {
            var (items, total) = await _repo.ListAsync(query.Nombre, query.PageNumber, query.PageSize, ct);
         
            return new PagedResult<VendedorBasicDto>
            {
                PageNumber = query.PageNumber,
                PageSize = query.PageSize,
                TotalCount = total,
                Items = items.Select(v => v.ToBasicDto()).ToList()
            };
        }

        public async Task<IReadOnlyList<VendedorBasicDto>> ListAllAsync(string? term = null, CancellationToken ct = default)
        {
            var items = await _repo.ListAllAsync(term, ct);
            return items.Select(v => v.ToBasicDto()).ToList();
        }
    }

}

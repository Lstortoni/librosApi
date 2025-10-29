
using ApiLibrosController.Contracts.Dto;
using ApiLibrosController.Contracts.shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrosController.Interfaces.Services
{
    public interface IVendedorService
    {
        Task<VendedorBasicDto> CreateAsync(CreateVendedorDto dto, CancellationToken ct = default);
        Task<VendedorBasicDto?> GetBasicByIdAsync(int id, CancellationToken ct = default);
        Task<bool> ExistsAsync(int id, CancellationToken ct = default);

        Task<PagedResult<VendedorBasicDto>> ListAsync(ListVendedorQueryDto query, CancellationToken ct = default);

        Task<IReadOnlyList<VendedorBasicDto>> ListAllAsync(string? term = null, CancellationToken ct = default);
    }
}



using ApiLibrosController.Model;
using ApiLibrosController.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrosController.Interfaces.Repositories
{
    public interface IVendedorRepository
    {
        Task<Vendedor> AddAsync(Vendedor entity, CancellationToken ct = default);
        Task<Vendedor?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<bool> ExistsAsync(int id, CancellationToken ct = default);

        Task<(IReadOnlyList<Vendedor> Items, int Total)> ListAsync(
           string? nombre,
           int pageNumber,
           int pageSize,
           CancellationToken ct = default);

        Task<IReadOnlyList<Vendedor>> ListAllAsync(string? term, CancellationToken ct = default);

    }
}

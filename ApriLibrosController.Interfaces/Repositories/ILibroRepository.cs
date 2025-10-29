using ApiLibrosController.Model;
using ApiLibrosController.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrosController.Interfaces.Repositories
{
    public interface ILibroRepository
    {
        Task<Libro> AddAsync(Libro entity, CancellationToken ct = default);
        Task<Libro?> GetByIdAsync(int id, bool includeVendedor = false, bool includeFotos = false, CancellationToken ct = default);
        Task UpdateAsync(Libro entity, CancellationToken ct = default);

        // Filtro básico para listar
        Task<(IReadOnlyList<Libro> Items, int Total)> ListAsync(
            string? term,
            int? vendedorId,
            EstadoPublicacion? estado,
            CondicionLibro? condicion,
            string? sortBy,
            int pageNumber,
            int pageSize,
            CancellationToken ct = default);

        Task<IReadOnlyList<Libro>> ListAllAsync(
          string? term,
          int? vendedorId,
          EstadoPublicacion? estado,
          CondicionLibro? condicion,
          string? sortBy,
          CancellationToken ct = default);
    }
}

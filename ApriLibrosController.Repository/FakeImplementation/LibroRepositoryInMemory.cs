using ApiLibrosController.Model;
using ApiLibrosController.Model.Enum;
using ApiLibrosController.Interfaces.Repositories;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrosController.Repository.FakeImplementation
{
    public class LibroRepositoryInMemory : ILibroRepository
    {
        private static int _id = 0;
        private readonly ConcurrentDictionary<int, Libro> _db = new();

        public Task<Libro> AddAsync(Libro entity, CancellationToken ct = default)
        {
            entity.Id = Interlocked.Increment(ref _id);
            _db[entity.Id] = entity;
            return Task.FromResult(entity);
        }

        public Task<Libro?> GetByIdAsync(int id, bool includeVendedor = false, bool includeFotos = false, CancellationToken ct = default)
        {
            var ok = _db.TryGetValue(id, out var l);
            // InMemory: include* no aplica; en EF vas a usar Include().
            return Task.FromResult(ok ? l : null);
        }

        public Task UpdateAsync(Libro entity, CancellationToken ct = default)
        {
            _db[entity.Id] = entity;
            return Task.CompletedTask;
        }

        public Task<(IReadOnlyList<Libro> Items, int Total)> ListAsync(
            string? term,
            int? vendedorId,
            EstadoPublicacion? estado,
            CondicionLibro? condicion,
            string? sortBy,
            int pageNumber,
            int pageSize,
            CancellationToken ct = default)
        {
            IEnumerable<Libro> q = _db.Values;

            // Filtros
            if (!string.IsNullOrWhiteSpace(term))
            {
                var t = term.Trim().ToLowerInvariant();
                q = q.Where(l => (l.Titulo?.ToLowerInvariant().Contains(t) ?? false)
                              || (l.Autor?.ToLowerInvariant().Contains(t) ?? false));
            }
            if (vendedorId.HasValue) q = q.Where(l => l.VendedorId == vendedorId.Value);
            if (estado.HasValue) q = q.Where(l => l.Estado == estado.Value);
            if (condicion.HasValue) q = q.Where(l => l.Condicion == condicion.Value);

            var total = q.Count();

            // Orden
            q = sortBy switch
            {
                "Precio_ASC" => q.OrderBy(l => l.Precio),
                "Precio_DESC" => q.OrderByDescending(l => l.Precio),
                "FechaPublicacion_ASC" => q.OrderBy(l => l.FechaPublicacion ?? l.FechaCreacion),
                "FechaPublicacion_DESC" => q.OrderByDescending(l => l.FechaPublicacion ?? l.FechaCreacion),
                "Titulo_ASC" => q.OrderBy(l => l.Titulo),
                "Titulo_DESC" => q.OrderByDescending(l => l.Titulo),
                _ => q.OrderByDescending(l => l.FechaPublicacion ?? l.FechaCreacion),
            };

            // Paginación (con guardas mínimas)
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 20;

            var items = q.Skip((pageNumber - 1) * pageSize)
                         .Take(pageSize)
                         .ToList();

            return Task.FromResult(((IReadOnlyList<Libro>)items, total));
        }
        // ApiLibrosController.Repository/FakeImplementation/LibroRepositoryInMemory.cs
        public Task<IReadOnlyList<Libro>> ListAllAsync(
            string? term, int? vendedorId, EstadoPublicacion? estado, CondicionLibro? condicion,
            string? sortBy, CancellationToken ct = default)
        {
            IEnumerable<Libro> q = _db.Values;

            if (!string.IsNullOrWhiteSpace(term))
            {
                var t = term.Trim().ToLowerInvariant();
                q = q.Where(l => (l.Titulo?.ToLowerInvariant().Contains(t) ?? false)
                              || (l.Autor?.ToLowerInvariant().Contains(t) ?? false));
            }
            if (vendedorId.HasValue) q = q.Where(l => l.VendedorId == vendedorId.Value);
            if (estado.HasValue) q = q.Where(l => l.Estado == estado.Value);
            if (condicion.HasValue) q = q.Where(l => l.Condicion == condicion.Value);

            q = sortBy switch
            {
                "Precio_ASC" => q.OrderBy(l => l.Precio),
                "Precio_DESC" => q.OrderByDescending(l => l.Precio),
                "FechaPublicacion_ASC" => q.OrderBy(l => l.FechaPublicacion ?? l.FechaCreacion),
                "FechaPublicacion_DESC" => q.OrderByDescending(l => l.FechaPublicacion ?? l.FechaCreacion),
                "Titulo_ASC" => q.OrderBy(l => l.Titulo),
                "Titulo_DESC" => q.OrderByDescending(l => l.Titulo),
                _ => q.OrderByDescending(l => l.FechaPublicacion ?? l.FechaCreacion),
            };

            return Task.FromResult((IReadOnlyList<Libro>)q.ToList());
        }
    }
}

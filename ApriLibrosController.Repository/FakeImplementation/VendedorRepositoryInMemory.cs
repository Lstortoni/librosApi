using ApiLibrosController.Interfaces.Repositories;
using ApiLibrosController.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrosController.Repository.FakeImplementation
{
    public class VendedorRepositoryInMemory : IVendedorRepository
    {
        private static int _id = 0;
        private readonly ConcurrentDictionary<int, Vendedor> _db = new();

        public Task<Vendedor> AddAsync(Vendedor entity, CancellationToken ct = default)
        {
            entity.Id = Interlocked.Increment(ref _id);
            _db[entity.Id] = entity;
            return Task.FromResult(entity);
        }

        public Task<Vendedor?> GetByIdAsync(int id, CancellationToken ct = default)
            => Task.FromResult(_db.TryGetValue(id, out var v) ? v : null);

        public Task<bool> ExistsAsync(int id, CancellationToken ct = default)
            => Task.FromResult(_db.ContainsKey(id));

        // Opcional si definiste List en IVendedorRepository
        public Task<(IReadOnlyList<Vendedor> Items, int Total)> ListAsync(
            string? term, int pageNumber, int pageSize, CancellationToken ct = default)
        {
            IEnumerable<Vendedor> q = _db.Values;

            if (!string.IsNullOrWhiteSpace(term))
            {
                var t = term.Trim().ToLowerInvariant();
                q = q.Where(v => v.Nombre.ToLowerInvariant().Contains(t));
            }

            var total = q.Count();
            var items = q.OrderBy(v => v.Nombre)
                         .Skip((pageNumber - 1) * pageSize)
                         .Take(pageSize)
                         .ToList();

            return Task.FromResult(((IReadOnlyList<Vendedor>)items, total));
        }
        // ApiLibrosController.Repository/FakeImplementation/VendedorRepositoryInMemory.cs
        public Task<IReadOnlyList<Vendedor>> ListAllAsync(string? term, CancellationToken ct = default)
        {
            IEnumerable<Vendedor> q = _db.Values;

            if (!string.IsNullOrWhiteSpace(term))
            {
                var t = term.Trim().ToLowerInvariant();
                q = q.Where(v => v.Nombre.ToLowerInvariant().Contains(t));
            }

            var items = q.OrderBy(v => v.Nombre).ToList();
            return Task.FromResult((IReadOnlyList<Vendedor>)items);
        }
    }
}

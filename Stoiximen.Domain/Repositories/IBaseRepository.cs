using Stoiximen.Domain.Models;

namespace Stoiximen.Domain.Repositories
{
    public interface IBaseRepository<T, TKey> where T : class, IEntity<TKey>
    {
        // Read (use NoTracking)
        Task<T?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);

        // Write
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task<bool> SoftDeleteAsync(TKey id, CancellationToken cancellationToken = default);
        Task<bool> SoftDeleteAsync(T entity, CancellationToken cancellationToken = default);

        // Utility
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
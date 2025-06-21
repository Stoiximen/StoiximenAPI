using Microsoft.EntityFrameworkCore;
using Stoiximen.Domain.Models;
using Stoiximen.Domain.Repositories;
using Stoiximen.Infrastructure.EF.Context;

namespace Stoiximen.Infrastructure.Repositories
{
    public class BaseRepository<T, TKey> : IBaseRepository<T, TKey> where T : class, IEntity<TKey>
    {
        protected readonly StoiximenDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(StoiximenDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
        }

        // Read (respect NoTracking default)
        public virtual async Task<T?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.Id.Equals(id), cancellationToken);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet.ToListAsync(cancellationToken);
        }

        // Write
        public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            var addedEntity = await _dbSet.AddAsync(entity, cancellationToken);
            return addedEntity.Entity;
        }

        public virtual Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Update(entity);
            return Task.FromResult(entity);
        }

        public virtual async Task<bool> DeleteAsync(TKey id, CancellationToken cancellationToken = default)
        {
            var entity = await GetByIdAsync(id, cancellationToken);
            if (entity == null)
                return false;

            if (entity is SoftDeletableEntity softDeletable)
            {
                // Soft delete - just set DeletedAt, SaveChanges will handle DeletedBy
                softDeletable.DeletedAt = DateTime.UtcNow;
                _dbSet.Update(entity);
            }
            else
            {
                // Hard delete
                _dbSet.Remove(entity);
            }
            return true;
        }

        public virtual Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            if (entity == null)
                return Task.FromResult(false);

            if (entity is SoftDeletableEntity softDeletable)
            {
                // Soft delete - just set DeletedAt, SaveChanges will handle DeletedBy
                softDeletable.DeletedAt = DateTime.UtcNow;
                _dbSet.Update(entity);
            }
            else
            {
                // Hard delete
                _dbSet.Remove(entity);
            }
            return Task.FromResult(true);
        }

        // Utility
        public virtual async Task<bool> ExistsAsync(TKey id, CancellationToken cancellationToken = default)
        {
            return await _dbSet.AnyAsync(e => e.Id.Equals(id), cancellationToken);
        }

        public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
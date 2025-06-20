using Stoiximen.Domain.Repositories;
using Stoiximen.Infrastructure.EF.Context;

namespace Stoiximen.Infrastructure.Repositories
{
    // Check methods later
    public class AsyncRepository<T> : IAsyncRepository<T> where T : class
    {
        protected readonly StoiximenDbContext _context;
        //protected readonly DbSet<T> _dbSet;

        public AsyncRepository(StoiximenDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            //_dbSet = _context.Set<T>();
        }

        //public virtual async Task<IEnumerable<T>> GetAllAsync()
        //{
        //    return await _dbSet.ToListAsync();
        //}

        //public virtual async Task<T?> GetByIdAsync(object id)
        //{
        //    return await _dbSet.FindAsync(id);
        //}

        //public virtual async Task<T> AddAsync(T entity)
        //{
        //    var addedEntity = await _dbSet.AddAsync(entity);
        //    return addedEntity.Entity;
        //}

        //public virtual async Task<T> UpdateAsync(T entity)
        //{
        //    _dbSet.Update(entity);
        //    return entity;
        //}

        //public virtual async Task<bool> DeleteAsync(object id)
        //{
        //    var entity = await GetByIdAsync(id);
        //    if (entity == null)
        //        return false;

        //    _dbSet.Remove(entity);
        //    return true;
        //}

        //public virtual async Task<bool> ExistsAsync(object id)
        //{
        //    var entity = await GetByIdAsync(id);
        //    return entity != null;
        //}

        //public virtual async Task<int> SaveChangesAsync()
        //{
        //    return await _context.SaveChangesAsync();
        //}
    }
}